using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto
{
    internal class Contract
    {
        private String _ID, _Description, _Salary, _StartDate, _EndDate;
        public String ID { get => _ID; set => _ID = value; }
        public String Description { get => _Description; set => _Description = value; }
        public String Salary { get => _Salary; set => _Salary = value; }
        public String StartDate { get => _StartDate; set => _StartDate = value; }
        public String EndDate { get => _EndDate; set => _EndDate = value; }

        public override string ToString()
        {
            return "Descrição: " + _Description + "\n" +
                "Salário: " + _Salary + "$\n" +
                "Data Início: " + _StartDate + "\n" +
                "Data Fim: " + _EndDate;
        }
    }
}
