using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto
{
    internal class Team
    {
        private String _ID, _TeamName, _City, _Conference, _FoundYear, _CoachName, _OwnerName;
        public String ID { get => _ID; set => _ID = value; }
        public String TeamName { get => _TeamName; set => _TeamName = value; }
        public String City { get => _City; set => _City = value; }
        public String Conference { get => _Conference; set => _Conference = value; }
        public String FoundYear { get => _FoundYear; set => _FoundYear = value; }
        public String CoachName { get => _CoachName; set => _CoachName = value; }
        public String OwnerName { get => _OwnerName; set => _OwnerName = value; }

        public override string ToString()
        {
            return _ID + ": " + _TeamName;
        }
    }
}
