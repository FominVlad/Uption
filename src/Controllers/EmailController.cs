﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Uption.Helpers;
using Uption.Models;
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

        //[HttpGet]
        //public List<Message> Get()
        //{
            
        //}

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
