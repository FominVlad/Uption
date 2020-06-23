using System;

namespace Uption.Models
{
    public class ViewActionDTO
    {
        /// <summary>
        /// Action date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Action IP adress
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// Action type description
        /// </summary>
        public string ActionType { get; set; }

        /// <summary>
        /// Action IP continent
        /// </summary>
        public string Continent { get; set; }

        /// <summary>
        /// Action IP country
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Action IP region
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Action IP city
        /// </summary>
        public string City { get; set; }
    }
}
