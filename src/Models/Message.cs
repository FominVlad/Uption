using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uption.Models
{
    public class Message
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }
    }
}
