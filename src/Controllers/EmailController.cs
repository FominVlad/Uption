using Microsoft.AspNetCore.Mvc;
using Uption.Helpers;
using Uption.Models.DTO;

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
