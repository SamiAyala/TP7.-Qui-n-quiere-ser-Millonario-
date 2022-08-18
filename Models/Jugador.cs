namespace QuienQuiereSerMillonario.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data.SqlClient;
using Dapper;

public class Jugador{
    private int _idJugador, _pozoGanado;
    private string _nombre;
    private dateTime _fechaHora;
    private bool _comodinDobleChance, _comodin50, _comodinSaltear;

    public Jugador ()
    {}
    
    public Jugador (int idJugador,int pozoGanado,string nombre, datetime fechaHora, bool comodinDobleChance,bool comodin50,bool comodinSaltear)
}