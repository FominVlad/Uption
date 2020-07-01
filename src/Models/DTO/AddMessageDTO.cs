namespace Uption.Models.DTO
{
    public class AddMessageDTO
    {
        /// <summary>
        /// Message unique id
        /// </summary>
        public string Ip { get; set; }

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
        /// Feedback text language
        /// </summary>
        public string Language { get; set; }
    }
}
