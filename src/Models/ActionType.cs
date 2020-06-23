using System.Collections.Generic;

namespace Uption.Models
{
    public class ActionType
    {
        /// <summary>
        /// Action type unique id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Action type description
        /// </summary>
        public string Type { get; set; }

        public List<Action> Actions { get; set; }
    }
}
