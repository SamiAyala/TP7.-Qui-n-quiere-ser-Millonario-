using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data.SqlClient;
using Dapper;

namespace QuienQuiereSerMillonario.Models
{
    public static class JuegoQQSM{
        private static int _preguntaActual,_posicionPozo,_pozoAcumuladoSeguro,_pozoAcumulado;
        private static char _respuestaCorrectaActual;
        private static bool _comodin5050,_comodinDobleChance,_comodinSaltear;
        private static List<Pozo> _listaPozo;
        private static Jugador _player;
        private static string _connectionString=@"Server=A-PHZ2-CIDI-026;DataBase=JuegoQQSM;Trusted_Connection=True;";

        public static void iniciarJuego(string pNombre){
            _preguntaActual=0;
            _respuestaCorrectaActual='\0';
            _posicionPozo=0;
            _pozoAcumuladoSeguro=0;
            _pozoAcumulado=0;
            _comodin5050=true; _comodinDobleChance=true;_comodinSaltear=true;
            _listaPozo=new List<Pozo>(){new Pozo(250, false), new Pozo(500, false), new Pozo(1000, false),new Pozo(2500, true),new Pozo(5000, false),new Pozo(7500, false),new Pozo(10000, false),new Pozo(20000, true),new Pozo(30000, false),new Pozo(50000, false),new Pozo(70000, false),new Pozo(100000, true),new Pozo(130000, false),new Pozo(180000, false),new Pozo(300000, false),new Pozo(50000, true),new Pozo(750000, false),new Pozo(1000000, false),new Pozo(1500000, false),new Pozo(2000000, true)};
            _listaPozo.Reverse();
            DateTime pFechaHora=DateTime.Now;
            string sql="INSERT INTO dbo.Jugadores(Nombre,FechaHora,PozoGanado, ComodinDobleChance, Comodin50, ComodinSaltear) VALUES (@Nombre,@FechaHora, 0, 1, 1, 1)";
            using(SqlConnection db=new SqlConnection(_connectionString)){
                db.Execute(sql,new {Nombre=pNombre,FechaHora=pFechaHora});
            }
            _player = new Jugador(0, _pozoAcumuladoSeguro,pNombre,pFechaHora,_comodinDobleChance,_comodin5050,_comodinSaltear);
        }
        public static List<Pregunta> ListarPreguntas(){
            using(SqlConnection db=new SqlConnection(_connectionString)){
                string sql="SELECT TOP 4 *,NEWID() FROM Preguntas WHERE NivelDificultad=1 UNION SELECT TOP 4 *,NEWID() FROM Preguntas WHERE NivelDificultad=2 UNION SELECT TOP 4 *,NEWID() FROM Preguntas WHERE NivelDificultad=3 UNION SELECT TOP 4 *,NEWID() FROM Preguntas WHERE NivelDificultad=4 ORDER BY NivelDificultad,NEWID()";
                List<Pregunta> listPreguntas = db.Query<Pregunta>(sql).ToList();
                return listPreguntas;
            }
        }
        public static List<Respuesta> ObtenerRespuestas()
        {
            List<Pregunta> listPreguntas = ListarPreguntas();
            using(SqlConnection db=new SqlConnection(_connectionString))
            {
                string sql="SELECT * FROM Respuestas R INNER JOIN Preguntas P ON P.idPregunta = R.fkPregunta WHERE idPregunta = @pIdPregunta";
                List<Respuesta> listRespuestas = db.Query<Respuesta>(sql, new{@pIdPregunta = listPreguntas[_preguntaActual].idPregunta}).ToList();
                sql = "SELECT OpcionRespuesta FROM Respuestas R INNER JOIN Preguntas P ON P.idPregunta = R.fkPregunta WHERE idPregunta = @pIdPregunta AND Correcta = 1";
                _respuestaCorrectaActual = db.QueryFirstOrDefault<char>(sql, new{@pIdPregunta = listPreguntas[_preguntaActual].idPregunta});
                return listRespuestas;
            }
        }

        public static bool ChequearRespuesta(char opcion, char opcionComodin){
            if (opcionComodin != null) _player.comodinDobleChance = false;
            if (opcion == _respuestaCorrectaActual || opcionComodin == _respuestaCorrectaActual)
            {
                if (_listaPozo[_posicionPozo].valorSeguro) _pozoAcumuladoSeguro = _listaPozo[_posicionPozo].importe;
                _posicionPozo++;
                return true;
            }else {
                return false;
            }
        }
        public static List<Pozo> DevolverPozo(){
            return _listaPozo;
        }
        public static int DevolverPosPozo(){
           return _posicionPozo; 
        }
        public static List<char> Comodin5050(){
            if(_player.comodin5050){
                _player.comodin5050 = false;
                int x = 0;
                List<char> ListChar = new List<char>();
                List<Respuesta> ListRespuesta = JuegoQQSM.ObtenerRespuestas();
                for(int i = 0; i<ListChar.Count() && x<2; i++){
                    if(ListRespuesta[i].correcta){
                        ListChar.Add(ListRespuesta[i].opcionRespuesta);
                        x++;
                    }
                }
                return ListChar;
            }
            return null;
        }
        public static void ComodinSaltear(){
            if (_player.comodinSaltear){
                _player.comodinSaltear = false;
                _preguntaActual++;
            }
        }
        public static Jugador DevolverJugador() {
            return _player;
        } 
    }
}