using System;
using System.Collections.Generic;
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

                emailSender.SendEmail(new List<string>() { "mr.vladyslavfomin@gmail.com" }, 
                    $"New message by {addMessageDTO.Name}", FormMessageText(addMessageDTO));

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private string FormMessageText(AddMessageDTO addMessageDTO)
        {
            return $"From: {addMessageDTO.Name} ({addMessageDTO.Email}).<br>Message:<br>{addMessageDTO.Text}";
        }
    }
}
