using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uption.Models
{
    public class Action
    {
        public int Id { get; set; }

        public int ActionTypeId { get; set; }

        public string Ip { get; set; }

        public DateTime Date { get; set; }

        public ActionType ActionType { get; set; }
    }
}
