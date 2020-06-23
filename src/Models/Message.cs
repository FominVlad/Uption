using System;

namespace Uption.Models
{
    public class Message
    {
        /// <summary>
        /// Message unique id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Sender email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Sender name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Message text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Sending date
        /// </summary>
        public DateTime Date { get; set; }
    }
}
