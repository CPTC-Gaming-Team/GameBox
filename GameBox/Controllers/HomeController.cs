using System.Diagnostics;
using GameBox.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameBox.Controllers
{
    /// <summary>
    /// Controller responsible for rendering the application's home pages such as the index,
    /// privacy page, and the global error page.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Renders the application home page (index).
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> that renders the Index view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Renders the privacy information page.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> that renders the Privacy view.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Displays the error view with request tracing information.
        /// The response is not cached to ensure fresh error details are shown.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> that renders the Error view with an <see cref="ErrorViewModel"/>.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
