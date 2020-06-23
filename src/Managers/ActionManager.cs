using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uption.Models;

namespace Uption.Helpers
{
    public class ActionManager
    {
        private AppDbContext dbContext { get; set; }
        private IpParser ipParser { get; set; }

        public ActionManager(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.ipParser = new IpParser();
        }

        public bool AddAction(AddActionDTO addActionDTO)
        {
            try
            {
                Models.Action action = new Models.Action()
                {
                    ActionTypeId = addActionDTO.ActionTypeId,
                    Ip = addActionDTO.Ip,
                    Date = DateTime.Now
                };

                dbContext.Actions.Add(action);
                dbContext.SaveChanges();

                if (dbContext.IpLocations.FirstOrDefault(x => x.Ip == addActionDTO.Ip) == null)
                {
                    Task<IpLocation> parsedIp = ipParser.Parse(addActionDTO.Ip);
                    dbContext.IpLocations.Add(parsedIp.Result);
                    dbContext.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<ViewActionDTO> GetActions()
        {
            var result = from action in dbContext.Actions
                   join actionType in dbContext.ActionTypes on action.ActionTypeId equals actionType.Id
                   join ipLoc in dbContext.IpLocations on action.Ip equals ipLoc.Ip into ipLocationsJoin
                   from ipLocation in ipLocationsJoin.DefaultIfEmpty()
                    select new ViewActionDTO
                    {
                        Date = action.Date,
                        Ip = action.Ip,
                        ActionType = actionType.Type,
                        Continent = ipLocation.Continent,
                        Country = ipLocation.Country,
                        Region = ipLocation.Region,
                        City = ipLocation.City
                    };

            return result.ToList();
        }
    }
}
