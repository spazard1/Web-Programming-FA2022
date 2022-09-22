using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Spells.Models;
using Spells.Services;
using System.Diagnostics;
using System.Net;

namespace Spells.Controllers
{
    public class SpellsController : Controller
    {

        private static SpellsDatabase spells = new SpellsDatabase();

        public IActionResult Index()
        {
            return View(spells);
        }

        public IActionResult ViewSpell(string id)
        {
            if (int.TryParse(id, out int result))
            {
                ViewData["id"] = result;
                return View(spells.Get(result));
            }

            return RedirectToAction("index");
        }

        [HttpPost]
        public IActionResult Add()
        {
            if (Request.HasFormContentType)
            {
                if (Request.Form.TryGetValue("spellName", out StringValues result))
                {
                    spells.Add(result.ToString());
                    return RedirectToAction("index");
                }
            }

            return StatusCode((int)HttpStatusCode.UnsupportedMediaType);
        }

        [HttpPost]
        public IActionResult Delete()
        {
            if (!Request.HasFormContentType)
            {
                return RedirectToAction("index");
            }

            if (!Request.Form.TryGetValue("spellIndex", out StringValues result))
            {
                return RedirectToAction("index");
            }

            if (!int.TryParse(result, out int spellIndex))
            {
                return RedirectToAction("index");
            }

            spells.Delete(spellIndex);

            return RedirectToAction("index");
        }
    }
}
