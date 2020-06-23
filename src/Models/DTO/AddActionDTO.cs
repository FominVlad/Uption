namespace Uption.Models
{
    public class AddActionDTO
    {
        /// <summary>
        /// Action IP adress
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// Action type unique id
        /// </summary>
        public int ActionTypeId { get; set; }
    }
}
