using System;

namespace Uption.Models
{
    public class Action
    {
        /// <summary>
        /// Action unique id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Action type unique id (FK)
        /// </summary>
        public int ActionTypeId { get; set; }

        /// <summary>
        /// Action IP
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// Action date
        /// </summary>
        public DateTime Date { get; set; }

        public int DeviceTypeId { get; set; }

        public ActionType ActionType { get; set; }

        public DeviceType DeviceType { get; set; }
    }
}
