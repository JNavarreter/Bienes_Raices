using Microsoft.AspNetCore.Mvc;
using PL.Models;
using System.Diagnostics;

namespace PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IHttpContextAccessor httpContextAccessor, ILogger<HomeController> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Logout()
        {
            _httpContextAccessor.HttpContext.Session.Clear();

            return RedirectToAction("Login", "Usuario");
        }

        public IActionResult PDF()
        {
            var pdfPath = "wwwroot/pdf/Documentacion.pdf"; // Path to your PDF file
            var pdfBytes = System.IO.File.ReadAllBytes(pdfPath);

            var pdfStream = new System.IO.MemoryStream(pdfBytes);

            return new FileStreamResult(pdfStream, "application/pdf");
        }
        public IActionResult AvisodePrivacidad()
        {
            var pdfPath = "wwwroot/pdf/Documentacion.pdf"; // Path to your PDF file
            var pdfBytes = System.IO.File.ReadAllBytes(pdfPath);

            var pdfStream = new System.IO.MemoryStream(pdfBytes);

            return new FileStreamResult(pdfStream, "application/pdf");
        }
    }
}