using System.Collections.Generic;
using FinalExam.Services;
using FinalExam.Models;
using System.Net;
using FinalExam.Filters;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using System.IO;
using System.Diagnostics;
using Polly;
using Polly.Retry;

namespace FinalExam.Controllers.V1U1
{
    [Route("api/[controller]")]
    [TypeFilter(typeof(AuthorizationFilter))]
    [TypeFilter(typeof(RequestIdFilter))]
    [ApiVersion("1.1")]
    [ApiVersion("2022-12-10")]
    [ApiController]
    public class BoardGamesController : Controller
    {
        private readonly IBoardGameDatabase boardGames;
        private readonly ImageProcessor imageProcessor;
        private readonly AsyncRetryPolicy retryPolicy;

        public BoardGamesController(IBoardGameDatabase boardGames, ImageProcessor imageProcessor, IExceptionDetectionStrategy exceptionDetectionStrategy)
        {
            this.boardGames = boardGames;
            this.imageProcessor = imageProcessor;
            retryPolicy = Policy.HandleInner<Exception>((ex) => exceptionDetectionStrategy.IsTransient(ex))
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromMilliseconds(100 * Math.Pow(2, retryAttempt)),
                (exception, timeSpan, context) =>
                {
                    Debug.WriteLine("Attempting again...");
                });
        }

        [HttpGet]
        public IEnumerable<BoardGameModel> Get()
        {
            return this.boardGames.Values;
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var boardGame = this.boardGames.Get(name);
            if (boardGame != null)
            {
                return Json(boardGame);
            }

            return StatusCode((int)HttpStatusCode.NotFound);
        }

        [HttpPost]
        public IActionResult Post([FromBody] BoardGameModel model)
        {
            if (this.boardGames.ContainsKey(model.Name))
            {
                return StatusCode((int)HttpStatusCode.BadRequest);
            }

            this.boardGames.Add(model);
            return Json(model);
        }

        [HttpPut("{name}")]
        public IActionResult Put(string name, [FromBody] BoardGameModel model)
        {
            if (this.boardGames.ContainsKey(model.Name))
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
            var existingModel = this.boardGames.Get(name);

            if (Request.Headers.TryGetValue("If-Match", out StringValues ifmatchHeader) && ifmatchHeader == existingModel.ETag)
            {
                this.boardGames.Add(model);
                return Json(model);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete("{name}")]
        public IActionResult Delete(string name)
        {
            if (!this.boardGames.ContainsKey(name))
            {
                return new StatusCodeResult((int)HttpStatusCode.NotFound);
            }

            this.boardGames.Remove(name);

            return new ContentResult() { Content = "The image was deleted." };
        }

        // AddImage is new in version 1.1
        [HttpGet("{name}/addImage")]
        public IActionResult AddImage(string name, [FromBody] string imageUrl)
        {
            this.imageProcessor.QueueProcessImage(name, imageUrl);
            return new StatusCodeResult((int)HttpStatusCode.Created);
        }

        // ProcessingFinished is new in version 1.1
        [HttpGet("{name}/processingFinished")]
        public async Task<IActionResult> ProcessingFinished(string name, string genre, int numberOfPlayers)
        {
            string result = null;
            try
            {
                var request = WebRequest.Create("https://notarealwebsite.com/makerequest");

                result = await this.retryPolicy.ExecuteAsync(async () =>
                {
                    var response = await request.GetResponseAsync();
                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    return responseString;
                });

            }
            catch (Exception)
            {
                result = "false";
            }

            if (result == "true")
            {
                return new ContentResult() { Content = "Processing is finished" };
            }

            return new ContentResult() { Content = "Processing is still in process..." };
        }
    }
}
