namespace QuienQuiereSerMillonario.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data.SqlClient;
using Dapper;

public class Pozo{
    private int _Importe;
    private bool _ValorSeguro;

    public Pozo(int Importe, bool ValorSeguro)
    {
        _Importe=Importe;
        _ValorSeguro=ValorSeguro;
    }
}