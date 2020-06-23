using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uption.Models
{
    public class ActionType
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public List<Action> Actions { get; set; }
    }
}
