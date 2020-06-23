using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uption.Models
{
    public class ViewActionDTO
    {
        public DateTime Date { get; set; }

        public string Ip { get; set; }

        public string ActionType { get; set; }

        public string Continent { get; set; }

        public string Country { get; set; }

        public string Region { get; set; }

        public string City { get; set; }
    }
}
