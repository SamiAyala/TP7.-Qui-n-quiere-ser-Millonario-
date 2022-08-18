namespace QuienQuiereSerMillonario.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data.SqlClient;
using Dapper;
public class Pregunta{
    private string _texto;
    private int _idPregunta, _nivelDificultad;

    public Pregunta()
    {}

    public Pregunta(int idPregunta, string texto, int nivelDificultad)
    {
        _idPregunta=idPregunta;
        _texto=texto;
        _nivelDificultad=nivelDificultad;
    }
}