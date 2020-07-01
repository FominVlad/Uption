using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeleSharp.TL;
using TeleSharp.TL.Contacts;
using TLSharp.Core;

namespace Uption.Services
{
    public class TelegramService
    {
        private IConfigurationSection telegramConfiguration { get; set; }
        private TelegramClient telegramClient { get; set; }
        private int ApiId { get; set; }
        private string ApiHash { get; set; }
        private string OwnerPhoneNumber { get; set; }
        private string Hash { get; set; }

        public TelegramService(IConfiguration configuration)
        {
            this.telegramConfiguration = configuration.GetSection("Telegram");

            if (!Int32.TryParse(telegramConfiguration?.GetSection("ApiId").Value, out int apiId))
                throw new Exception("Cannot parse ApiId section!");

            this.ApiId = apiId;

            this.ApiHash = telegramConfiguration?.GetSection("ApiHash").Value;

            if (string.IsNullOrEmpty(this.ApiHash))
                throw new Exception("ApiHash is null or empty!");

            this.OwnerPhoneNumber = telegramConfiguration?.GetSection("OwnerPhoneNumber").Value;

            if (string.IsNullOrEmpty(this.OwnerPhoneNumber))
                throw new Exception("Owner phone number is null or empty!");

            this.telegramClient = new TelegramClient(this.ApiId, this.ApiHash);
        }

        private async void SetHash()
        {
            var hash = await telegramClient?.SendCodeRequestAsync(this.OwnerPhoneNumber);
            this.Hash = hash;
        }

        public async void MakeAuth(string code)
        {
            try
            {
                if (this.Hash == null)
                    throw new Exception("Hash is null!");

                await telegramClient?.MakeAuthAsync(this.OwnerPhoneNumber, this.Hash, code);
            }
            catch (Exception ex)
            {

            }
        }

        private async Task<TLUser> GetUserByPhoneNumber(string phoneNumber)
        {
            TLContacts result = await telegramClient?.GetContactsAsync();

            return result.Users
                .Where(x => x.GetType() == typeof(TLUser))
                .Cast<TLUser>()
                .FirstOrDefault(x => x.Phone == phoneNumber.Trim().Replace("+", ""));
        }

        public async Task<TLImportedContacts> AddContact(string phoneNumber, string firstName, string lastName)
        {
            var contacts = new TLVector<TLInputPhoneContact>();
            contacts.Add(new TLInputPhoneContact 
            { 
                Phone = phoneNumber, 
                FirstName = firstName, 
                LastName = lastName 
            });

            var req = new TLRequestImportContacts()
            {
                Contacts = contacts
            };

            return await telegramClient?.SendRequestAsync<TLImportedContacts>(req);
        }

        public async Task<bool> SendMessage(string phoneNumber, string contactName, string messageText)
        {
            try
            {
                await telegramClient?.ConnectAsync();

                if (!telegramClient.IsUserAuthorized())
                {
                    SetHash();
                    throw new Exception("User is unauthorized!");
                }

                TLUser user = GetUserByPhoneNumber(phoneNumber).Result;

                if (user == null)
                {
                    await AddContact(phoneNumber, contactName, "");
                    user = await GetUserByPhoneNumber(phoneNumber);
                }

                await telegramClient?.SendMessageAsync(new TLInputPeerUser() { UserId = user.Id }, messageText);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
