using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto
{
    internal class Game
    {
        private String _ID, _Time, _Date, _HomeScore, _AwayScore, _HomeID, _AwayID;
        public String ID { get => _ID; set => _ID = value; }
        public String Time { get => _Time; set => _Time = value; }
        public String Date { get => _Date; set => _Date = value; }
        public String HomeScore { get => _HomeScore; set => _HomeScore = value; }
        public String AwayScore { get => _AwayScore; set => _AwayScore = value; }
        public String HomeID { get => _HomeID; set => _HomeID = value; }
        public String AwayID { get => _AwayID; set => _AwayID = value; }

        public override string ToString()
        {
            if (_HomeScore == "") 
            {
                return "ID: " + _ID + " Data: " + _Date + " Hora: " + _Time;
            } 
            else
            {
                return "ID: " + _ID + " Data: " + _Date + " Hora: " + _Time + " Resultado: " + _HomeScore + "-" + _AwayScore;
            }
        }
    }
}
