using Microsoft.AspNetCore.Mvc;
using Uption.Helpers;
using Uption.Models.DTO;
using TeleSharp.TL;
using TLSharp.Core;
using System.Linq;
using TeleSharp.TL.Messages;
using System;
using TeleSharp.TL.Contacts;
using Uption.Services;

namespace Uption.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private EmailManager emailManager { get; set; }

        public EmailController(AppDbContext dbContext)
        {
            emailManager = new EmailManager(dbContext);
        }

        [HttpPut]
        public IActionResult Put([FromBody] AddMessageDTO addMessageDTO)
        {
            if(emailManager.AddMessage(addMessageDTO))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
