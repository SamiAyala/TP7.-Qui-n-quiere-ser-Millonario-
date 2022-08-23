using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data.SqlClient;
using Dapper;

namespace QuienQuiereSerMillonario.Models
{
    public class JuegoQQSM{
        private int _PreguntaActual,_PosicionPozo,_PozoAcumuladoSeguro,_pozoAcumulado;
        private char _RespuestaCorrectaActual;
        private bool _Comodin5050,_ComodinDobleChance,_ComodinSaltear;
        private List<Pozo> _ListaPozo= new List<Pozo>();
        private Jugador _Player;
        private static string _connectionString=@"Server=A-PHZ2-CIDI-053\SQLEXPRESS;DataBase=JuegoQQSM;Trusted_Connection=True;";

        public void iniciarJuego(string pNombre){
            _PreguntaActual=1;
            _RespuestaCorrectaActual='\0';
            _PosicionPozo=0;
            _PozoAcumuladoSeguro=0;
            _pozoAcumulado=0;
            _Comodin5050=true; _ComodinDobleChance=true;_ComodinSaltear=true;
            _ListaPozo=new List<Pozo>(){new Pozo(250, false), new Pozo(500, false), new Pozo(1000, false),new Pozo(5000, false),new Pozo(10000, false),new Pozo(25000, false),new Pozo(50000, false),new Pozo(100000, false),new Pozo(250000, false),new Pozo(500000, false),new Pozo(1000000, false)};
            DateTime pFechaHora=DateTime.Now;
            string sql="INSERT INTO dbo.Jugadores(Nombre,FechaHora) VALUES (@Nombre,@FechaHora)";
            using(SqlConnection db=new SqlConnection(_connectionString)){
                db.Execute(sql,new {Nombre=pNombre,FechaHora=pFechaHora});
            }
            _Player = new Jugador(0,_PozoAcumuladoSeguro,pNombre,pFechaHora,_ComodinDobleChance,_Comodin5050,_ComodinSaltear);
        }
        public void ListarPreguntas(){
            using(SqlConnection db=new SqlConnection(_connectionString)){
                string sql="SELECT TOP 4 * FROM Preguntas WHERE NivelDificultad=1 ORDER BY NEWID() SELECT TOP 4 * FROM Preguntas WHERE NivelDificultad=2 ORDER BY NEWID() SELECT TOP 4 * FROM Preguntas WHERE NivelDificultad=3 ORDER BY NEWID() SELECT TOP 4 * FROM Preguntas WHERE NivelDificultad=4 ORDER BY NEWID()";
                List<Pregunta> ListPreguntas = db.Query<Pregunta>(sql).ToList();
            }
        }
        public void ObtenerRespuestas()
        {
            using(SqlConnection db=new SqlConnection(_connectionString))
            {
                string sql="SELECT * FROM Respuestas R INNER JOIN Preguntas P ON P.idPregunta = R.fkPregunta WHERE idPregunta=pIdPregunta";
                List<Respuesta> ListRespuestas = db.Query<Respuesta>(sql).ToList();
            }
        }
    }
}