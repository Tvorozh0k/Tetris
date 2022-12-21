using BL;
using Interfaces;
using Tetris.Models.Users;
using Tetris.Models;
using Tetris.Models.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Tetris.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUsersBL _userBl;
        private IGamesBL _gameBl;

        public HomeController(ILogger<HomeController> logger, IUsersBL userBl, IGamesBL gameBl)
        {
            _logger = logger;
            _userBl = userBl;
            _gameBl = gameBl;
        }

        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Game()
        {
            var newGame = new Entities.Game();

            GameModel gameModel = new GameModel()
            {
                Score = 0,
                GameDate = DateTime.Now,
                UserId = _userBl.GetByLogin(User.Identity.Name).Id
            };

            return View(gameModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Result(int score)
        {
            var game = new Entities.Game()
            {
                Score = score,
                GameDate = DateTime.Now,
                UserId = _userBl.GetByLogin(User.Identity.Name).Id
            };

            _gameBl.Add(game);

            return RedirectToAction("Game");
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetResults()
        {
            var query = _gameBl.GetByUserId(_userBl.GetByLogin(User.Identity.Name).Id);
            return View(query);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}