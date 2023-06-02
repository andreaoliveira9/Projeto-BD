using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Projeto
{
    internal class Standing
    {
        private String _TeamName, _GamesPlayed, _Wins, _Losses, _WinP;
        public String TeamName { get => _TeamName; set => _TeamName = value; }
        public String GamesPlayed { get => _GamesPlayed; set => _GamesPlayed = value; }
        public String Wins { get => _Wins; set => _Wins = value; }
        public String Losses { get => _Losses; set => _Losses = value; }
        public String WinP { get => _WinP; set => _WinP = value; }

        public override string ToString()
        {
            return string.Format("{0,-30}\n GP: {1,-5} W: {2,-5} L: {3,-5} Win%: {4,-5}", _TeamName, _GamesPlayed, _Wins, _Losses, _WinP);


        }
    }
}
