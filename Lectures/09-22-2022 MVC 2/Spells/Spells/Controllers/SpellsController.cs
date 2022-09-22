using Microsoft.AspNetCore.Mvc;
using Spells.Models;
using System.Diagnostics;

namespace Spells.Controllers
{
    public class SpellsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}