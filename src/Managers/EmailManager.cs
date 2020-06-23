using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uption.Models;
using Uption.Models.DTO;

namespace Uption.Helpers
{
    public class EmailManager
    {
        private AppDbContext dbContext { get; set; }
        private EmailSender emailSender { get; set; }
        private ActionManager actionManager { get; set; }
        public EmailManager(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.emailSender = new EmailSender();
            this.actionManager = new ActionManager(dbContext);
        }

        public bool AddMessage(AddMessageDTO addMessageDTO)
        {
            try
            {
                Message message = new Message()
                {
                    Email = addMessageDTO.Email,
                    Name = addMessageDTO.Name,
                    Text = addMessageDTO.Text,
                    Date = DateTime.Now
                };

                dbContext.Messages.Add(message);
                dbContext.SaveChanges();

                AddActionDTO action = new AddActionDTO()
                {
                    Ip = addMessageDTO.Ip,
                    ActionTypeId = 2
                };

                actionManager.AddAction(action);

                string messageStr = $"From: {addMessageDTO.Name} ({addMessageDTO.Email}).<br>Message:<br>{addMessageDTO.Text}";

                emailSender.SendEmail(new List<string>() { "mr.vladyslavfomin@gmail.com" }, $"New message by {addMessageDTO.Name}", messageStr);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
