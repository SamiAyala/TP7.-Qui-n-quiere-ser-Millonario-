namespace QuienQuiereSerMillonario.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data.SqlClient;
using Dapper;

public class Jugador{
    private int _idJugador, _pozoGanado;
    private string _nombre;
    private DateTime _fechaHora;
    private bool _comodinDobleChance, _comodin50, _comodinSaltear;

    public Jugador()
    {}
    
    public Jugador(int idJugador,int pozoGanado,string nombre, DateTime fechaHora, bool comodinDobleChance,bool comodin50,bool comodinSaltear)
    {
        _idJugador=idJugador;
        _pozoGanado=pozoGanado;
        _nombre=nombre;
        _fechaHora=fechaHora;
        _comodinDobleChance=comodinDobleChance;
        _comodin50=comodin50;
        _comodinSaltear=comodinSaltear;
    }
}