using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uption.Models.DTO
{
    public class AddMessageDTO
    {
        public string Ip { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }
    }
}
