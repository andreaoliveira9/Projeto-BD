using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto
{
    class Game1
    {
        private String _ID, _Time, _Date, _HomeScore, _AwayScore, _HomeID, _AwayID, _HomeTeamName, _AwayTeamName, _StadiumName;
        public String ID { get => _ID; set => _ID = value; }
        public String Time { get => _Time; set => _Time = value; }
        public String Date { get => _Date; set => _Date = value; }
        public String HomeScore { get => _HomeScore; set => _HomeScore = value; }
        public String AwayScore { get => _AwayScore; set => _AwayScore = value; }
        public String HomeID { get => _HomeID; set => _HomeID = value; }
        public String AwayID { get => _AwayID; set => _AwayID = value; }
        public String HomeTeamName { get => _HomeTeamName; set => _HomeTeamName = value; }
        public String AwayTeamName { get => _AwayTeamName; set => _AwayTeamName = value; }
        public String StadiumName { get => _StadiumName; set => _StadiumName = value; }

        public override string ToString()
        {
            return string.Format("ID: {0,-4} {1}    VS    {2}", _ID, _HomeTeamName, _AwayTeamName);
        }
    }
}
