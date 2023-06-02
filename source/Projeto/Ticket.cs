using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto
{
    internal class Ticket
    {
        private String _Type, _Price, _Restantes;
        public String Type { get => _Type; set => _Type = value; }
        public String Price { get => _Price; set => _Price = value; }
        public String Restantes { get => _Restantes; set => _Restantes = value; }

        public override string ToString()
        {
            return string.Format("{0,-10} Preço: {1}$    Restantes: {2,-10}", _Type, _Price, _Restantes);
        }
    }
}
