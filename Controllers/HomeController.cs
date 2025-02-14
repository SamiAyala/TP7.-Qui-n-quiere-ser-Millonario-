﻿using System.Diagnostics;
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
        Jugador jug = JuegoQQSM.DevolverJugador();
        int pozoAcumuladoSeguro = JuegoQQSM.DevolverPozoAsegurado();
        ViewBag.jug = jug;
        ViewBag.pozoAcumuladoSeguro = pozoAcumuladoSeguro;
        JuegoQQSM.GuardarJugador(jug, pozoAcumuladoSeguro);
        return View();
    }

    public IActionResult Victoria()
    {
        Jugador jug = JuegoQQSM.DevolverJugador();
        int pozoGanado = JuegoQQSM.DevolverPozoAsegurado();
        ViewBag.jug = jug;
        ViewBag.pozoGanado = pozoGanado;
        JuegoQQSM.GuardarJugador(jug, pozoGanado);
        return View();
    }

    public IActionResult Retirarse()
    {
        Jugador jug = JuegoQQSM.DevolverJugador();
        int pozoGanado = JuegoQQSM.DevolverPozo()[JuegoQQSM.DevolverPosPozo()].importe;
        ViewBag.jug = jug;
        ViewBag.pozoGanado = pozoGanado;
        JuegoQQSM.GuardarJugador(jug, pozoGanado);
        return View("Victoria");
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

    public IActionResult SiguientePreg(){
        JuegoQQSM.IncrementarPregYPozo();
        Jugador jug = JuegoQQSM.DevolverJugador();
        List<Pozo> listPozo = JuegoQQSM.DevolverPozo();
        int posicionPozo = JuegoQQSM.DevolverPosPozo();
        ViewBag.jug = jug;
        ViewBag.listPozo = listPozo;
        if(posicionPozo >= listPozo.Count){
            return RedirectToAction("Victoria");
        } else {
            ViewBag.posicionPozo = posicionPozo;
            ViewBag.listPreg = JuegoQQSM.DevolverListaPreguntas();
            ViewBag.pregActual = JuegoQQSM.DevolverPregActual();
            ViewBag.preg = JuegoQQSM.DevolverPregunta(ViewBag.listPreg);
            ViewBag.listResp = JuegoQQSM.ObtenerRespuestas(ViewBag.listPreg[ViewBag.pregActual].idPregunta);
            return View("Juego");
        }
    }

    public IActionResult ComodinSaltear(){
        JuegoQQSM.ComodinSaltear();
        return RedirectToAction("SiguientePreg","Home");
    }

    [HttpPost]
    public List<char> Comodin5050(){
        return JuegoQQSM.Comodin5050();
    }

    [HttpPost]
    public int ComodinDobleChance(){
        int i = 0;
        JuegoQQSM.ComodinDobleChance();
        return i;
    }

    [HttpPost]
    public JsonResult ChequearRespuestaAjax(char opcion){
        return Json(JuegoQQSM.ChequearRespuesta(opcion, opcion));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
