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
        private String _CCNumber, _Number, _Height, _Weight, _Position, _TeamID, _Name, _Age, _ContractID;
        public String CCNumber 
        { get { return _CCNumber; } set { _CCNumber = value; } }
        public String Number
        { get { return _Number; } set { _Number = value;  } }
        public String Height
        { get { return _Height; } set { _Height = value; } }
        public String Weight
        { get { return _Weight; } set { _Weight = value; } }
        public String Position
        { get { return _Position; } set { _Position = value; } }
        public String TeamID
        { get { return _TeamID; } set { _TeamID = value; } }
        public String Name
        { get { return _Name; } set { _Name  = value; } }
        public String Age
        { get { return _Age; } set { _Age = value; } }
        public String ContractID
        { get { return _ContractID; } set { _ContractID = value; } }

        public override string ToString()
        {
            return _CCNumber + ": " + _Name;
        }
    }
}
