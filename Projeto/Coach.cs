using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto
{
    internal class Coach
    {
        private String _CCNumber, _Name, _Age, _ContractID;
        public String CCNumber { get => _CCNumber; set => _CCNumber = value; }
        public String Name { get => _Name; set => _Name = value; }
        public String Age { get => _Age; set => _Age = value; }
        public String ContractID { get => _ContractID; set => _ContractID = value; }

        public override string ToString()
        {
            return _CCNumber + ": " + _Name;
        }
    }
}
