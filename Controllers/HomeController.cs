using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using QuienQuiereSerMillonario.Models;

namespace QuienQuiereSerMillonario.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
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
    public IActionResult Derrota()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Juego(string nombre)
    {
        JuegoQQSM.iniciarJuego(nombre);
        ViewBag.jug = JuegoQQSM.DevolverJugador();
        ViewBag.listPozo = JuegoQQSM.DevolverPozo();
        ViewBag.listPreg = JuegoQQSM.ListarPreguntas();
        ViewBag.listResp = JuegoQQSM.ObtenerRespuestas();
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
