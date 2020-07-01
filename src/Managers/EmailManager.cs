using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
                    $"Новое сообщение от {addMessageDTO.Name}", FormMessageText(addMessageDTO));

                SendFeedback(addMessageDTO);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private string FormMessageText(AddMessageDTO addMessageDTO)
        {
            return $"<b>Отправитель:</b><br>{addMessageDTO.Name} ({addMessageDTO.Email}).<br><b>Текст:</b><br>{addMessageDTO.Text}<br><b>Язык общения:</b> <em>{addMessageDTO.Language}.</em>";
        }
        
        private void FeedbackMessageText(AddMessageDTO addMessageDTO, out string messageSubject, out string messageText)
        {
            switch (addMessageDTO?.Language)
            {
                case "Rus":
                    {
                        messageSubject = "Ваше сообщение было доставлено!";
                        messageText = $"Здравствуйте, <b>{addMessageDTO.Name}</b>!<br>Мы получили Ваше сообщение:<br><em>\"{addMessageDTO.Text}\"</em>.<br>С Вами свяжутся в ближайшее время.<br>С уважением, <b>команда Uption</b>!";
                        break;
                    };
                case "Ukr":
                    {
                        messageSubject = "Ваше повідомлення було доставлено!";
                        messageText = $"Привіт, <b>{addMessageDTO.Name}</b>!<br>Ми отримали Ваше повідомлення:<br><em>\"{addMessageDTO.Text}\"</em>.<br>З Вами зв'яжуться найближчим часом.<br>З повагою, <b>команда Uption</b>!";
                        break;
                    };
                case "Eng":
                    {
                        messageSubject = "Your message has been delivered!";
                        messageText = $"Hello, <b>{addMessageDTO.Name}</b>!<br>We have received your message:<br><em>\"{addMessageDTO.Text}\"</em>.<br>You will be contacted shortly.<br>Regards, <b>Uption team</b>!";
                        break;
                    }
                default:
                    {
                        throw new Exception("Language is undefined in switch.");
                    }
            }
        }

        private bool SendFeedback(AddMessageDTO addMessageDTO)
        {
            try
            {
                if (!Regex.IsMatch(addMessageDTO.Email, @"^\S+@\S+$"))
                {
                    return false;
                }

                FeedbackMessageText(addMessageDTO, out string messageSubject, out string messageText);

                emailSender.SendEmail(new List<string>() { addMessageDTO.Email }, messageSubject, messageText);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}


