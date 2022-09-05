namespace QuienQuiereSerMillonario.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data.SqlClient;
using Dapper;

public class Respuesta{

    private int _idRespuesta, _idPregunta;
    private char _opcionRespuesta;
    private string _texto;
    private bool _correcta;

    public Respuesta()
    {}

    public Respuesta(int idRespuesta, int idPregunta, char OpcionRespuesta, string texto, bool correcta)
    {
        _idRespuesta=idRespuesta;
        _idPregunta=idPregunta;
        _opcionRespuesta=OpcionRespuesta;
        _texto=texto;
        _correcta=correcta;
    }

    public int idRespuesta
    {
        get{return _idRespuesta;}
    }
    public int idPregunta
    {
        get{return _idPregunta;}
    }
    public char opcionRespuesta
    {
        get{return _opcionRespuesta;}
    }
    public string texto
    {
        get{return _texto;}
    }
    public bool correcta
    {
        get{return _correcta;}
    }
}