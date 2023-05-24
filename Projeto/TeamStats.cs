using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto
{
    internal class TeamStats
    {
        private String _Points, _Assists, _Rebounds, _Blocks, _Steals, _FG, _PT3;
        public String Points { get => _Points; set => _Points = value; }
        public String Assists { get => _Assists; set => _Assists = value; }
        public String Rebounds { get => _Rebounds; set => _Rebounds = value; }
        public String Blocks { get => _Blocks; set => _Blocks = value; }
        public String Steals { get => _Steals; set => _Steals = value; }
        public String FG { get => _FG; set => _FG = value; }
        public String PT3 { get => _PT3; set => _PT3 = value; }

        public override string ToString()
        {
            return "Points: " + _Points + "\n" +
                "Assists: " + _Assists + "\n" +
                "Rebounds: " + _Rebounds + "\n" +
                "Blocks: " + _Blocks + "\n" +
                "Steals: " + _Steals + "\n" +
                "FG%: " + _FG + "\n" +
                "3PT%: " + _PT3;
        }
    }
}
