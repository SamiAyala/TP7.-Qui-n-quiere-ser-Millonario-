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
    private bool _comodinDobleChance, _comodin5050, _comodinSaltear;

    public Jugador()
    {}
    
    public Jugador(int idJugador,int pozoGanado,string nombre, DateTime fechaHora, bool comodinDobleChance,bool comodin5050,bool comodinSaltear)
    {
        _idJugador=idJugador;
        _pozoGanado=pozoGanado;
        _nombre=nombre;
        _fechaHora=fechaHora;
        _comodinDobleChance=comodinDobleChance;
        _comodin5050=comodin5050;
        _comodinSaltear=comodinSaltear;
    }

    public int idJugador{
        get{return _idJugador;}
        set{_idJugador = value;}
    }
    public int pozoGanado{
        get{return _pozoGanado;}
        set{_pozoGanado = value;}
    }
    public string nombre{
        get{return _nombre;}
        set{_nombre = value;}
    }
    public DateTime fechaHora{
        get{return _fechaHora;}
        set{_fechaHora = value;}
    }
    public bool comodinDobleChance{
        get{return _comodinDobleChance;}
        set{_comodinDobleChance = value;}
    }
    public bool comodin5050{
        get{return _comodin5050;}
        set{_comodin5050 = value;}
    }
    public bool comodinSaltear{
        get{return _comodinSaltear;}
        set{_comodinSaltear = value;}
    }
}