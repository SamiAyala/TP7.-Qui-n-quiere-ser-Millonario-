namespace QuienQuiereSerMillonario.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data.SqlClient;
using Dapper;

public class Pozo{
    private int _importe;
    private bool _valorSeguro;

    public Pozo(int Importe, bool ValorSeguro)
    {
        _importe=Importe;
        _valorSeguro=ValorSeguro;
    }
}