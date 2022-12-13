using System.Collections.Generic;
using FinalExam.Services;
using FinalExam.Models;
using System.Net;
using FinalExam.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace FinalExam.Controllers.V1U0
{
    [Route("api/[controller]")]
    [TypeFilter(typeof(AuthorizationFilter))]
    [TypeFilter(typeof(RequestIdFilter))]
    [ApiVersion("1.0")]
    [ApiVersion("2022-03-02")]
    [ApiController]
    public class BoardGamesController : Controller
    {
        private readonly IBoardGameDatabase boardGames;

        public BoardGamesController(IBoardGameDatabase boardGames)
        {
            this.boardGames = boardGames;
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
        public BoardGameModel Post([FromBody] BoardGameModel model)
        {
            if (this.boardGames.ContainsKey(model.Name))
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return new BoardGameModel();
            }
            this.boardGames.Add(model);
            return model;
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
            return new StatusCodeResult((int)HttpStatusCode.Accepted);
        }
    }
}
