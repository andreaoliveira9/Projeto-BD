using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Projeto
{
    class Player
    {
        private String _CCNumber, _Number, _Height, _Weight, _Position, _TeamID, _Name, _Age, _ContractID, _TeamName;
        public String CCNumber { get => _CCNumber; set => _CCNumber = value; }
        public String Number { get => _Number; set => _Number = value; }
        public String Height { get => _Height; set => _Height = value; }
        public String Weight { get => _Weight; set => _Weight = value; }
        public String Position { get => _Position; set => _Position = value; }
        public String TeamID { get => _TeamID; set => _TeamID = value; }
        public String Name { get => _Name; set => _Name = value; }
        public String Age { get => _Age; set => _Age = value; }
        public String ContractID { get => _ContractID; set => _ContractID = value; }
        public String TeamName { get => _TeamName; set => _TeamName = value; }

        public override string ToString()
        {
            return _CCNumber + ": " + _Name;
        }
    }
}
