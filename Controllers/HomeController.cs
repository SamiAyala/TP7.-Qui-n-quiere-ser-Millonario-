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
    public IActionResult Victoria()
    {
        Jugador jug = JuegoQQSM.DevolverJugador();
        int pozoAcumuladoSeguro = JuegoQQSM.DevolverPozoAsegurado();
        ViewBag.jug = jug;
        ViewBag.pozoAcumuladoSeguro = pozoAcumuladoSeguro;
        JuegoQQSM.GuardarJugador(jug, pozoAcumuladoSeguro);
        return View();
    }
    
    [HttpPost]
    public IActionResult Juego(string nombre)
    {
        JuegoQQSM.iniciarJuego(nombre);
        ViewBag.jug = JuegoQQSM.DevolverJugador();
        ViewBag.listPozo = JuegoQQSM.DevolverPozo();
        ViewBag.posicionPozo = JuegoQQSM.DevolverPosPozo();
        ViewBag.listPreg = JuegoQQSM.ListarPreguntas();
        ViewBag.pregActual = JuegoQQSM.DevolverPregActual();
        ViewBag.preg = JuegoQQSM.DevolverPregunta(ViewBag.listPreg);
        ViewBag.listResp = JuegoQQSM.ObtenerRespuestas(ViewBag.listPreg[ViewBag.pregActual].idPregunta);
        return View();
    }

    [HttpPost]
    public JsonResult ChequearRespuestaAjax(char opcion){
        return Json(JuegoQQSM.ChequearRespuesta(opcion, opcion));
    }

    public IActionResult SiguientePreg(){
        Jugador jug = JuegoQQSM.DevolverJugador();
        List<Pozo> listPozo = JuegoQQSM.DevolverPozo();
        int posicionPozo = JuegoQQSM.DevolverPosPozo();
        ViewBag.jug = jug;
        ViewBag.listPozo = listPozo;
        if(posicionPozo >= listPozo.Count){
            ViewBag.posicionPozo = posicionPozo-1;
            return View("Victoria");
        } else {
            ViewBag.posicionPozo = posicionPozo;
            ViewBag.listPreg = JuegoQQSM.DevolverListaPreguntas();
            ViewBag.pregActual = JuegoQQSM.DevolverPregActual();
            ViewBag.preg = JuegoQQSM.DevolverPregunta(ViewBag.listPreg);
            ViewBag.listResp = JuegoQQSM.ObtenerRespuestas(ViewBag.listPreg[ViewBag.pregActual].idPregunta);
            return View("Juego");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
