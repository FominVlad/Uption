using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Uption.Helpers;
using Uption.Models;

namespace Uption.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionController : ControllerBase
    {
        private ActionManager actionManager { get; set; }
        public ActionController(AppDbContext dbContext)
        {
            actionManager = new ActionManager(dbContext);
        }

        [HttpGet]
        public List<ViewActionDTO> GetActions()
        {
            return actionManager.GetActions();
        }

        [HttpPut]
        public IActionResult AddAction([FromBody] AddActionDTO addActionDTO)
        {
            if (actionManager.AddAction(addActionDTO))
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
