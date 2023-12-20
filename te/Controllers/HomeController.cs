using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using te.Models;

namespace te.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            TicTacToeModel model = new TicTacToeModel
            {
                Board = new char[9] { '1', '2', '3', '4', '5', '6', '7', '8', '9' },
                CurrentPlayer = 1
            };

            if (HttpContext.Session.TryGetValue("LastButtonClicked", out byte[] lastButtonClickedBytes))
            {
                int lastButtonClicked = BitConverter.ToInt32(lastButtonClickedBytes, 0);
                if (lastButtonClicked >= 1 && lastButtonClicked <= model.Board.Length)
                {
                    model.Board[lastButtonClicked - 1] = ' '; // Make the clicked button empty
                }
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult MakeMove(int move, TicTacToeModel model)
        {
            if (model == null)
            {
                model = new TicTacToeModel();
                model.Board = new char[9] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            }

            if (model.Board != null && model.Board[move - 1] != 'X' && model.Board[move - 1] != 'O')
            {
                model.Board[move - 1] = (model.CurrentPlayer == 1) ? 'X' : 'O';
                model.CurrentPlayer = (model.CurrentPlayer == 1) ? 2 : 1;
            }

            HttpContext.Session.SetInt32("LastButtonClicked", move); // Сохраняем номер нажатой кнопки в сессии

            return RedirectToAction("Index", model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}