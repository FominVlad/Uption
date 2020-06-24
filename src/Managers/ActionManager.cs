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
                Models.Action lastAction = GetLastAction(addActionDTO);

                if (lastAction != null && CompareDates(DateTime.Now, lastAction.Date, 900))
                {
                    return true;
                }

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
                    AddIpLocation(addActionDTO.Ip);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void AddIpLocation(string ip)
        {
            Task<IpLocation> parsedIp = ipParser.Parse(ip);
            dbContext.IpLocations.Add(parsedIp.Result);
            dbContext.SaveChanges();
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

        private Models.Action GetLastAction(AddActionDTO addActionDTO)
        {
            return dbContext.Actions.Where(a => a.Ip == addActionDTO.Ip && a.Date == dbContext.Actions.Max(d => d.Date) &&
                    a.ActionTypeId == addActionDTO.ActionTypeId).FirstOrDefault();
        }

        private bool CompareDates(DateTime firstDate, DateTime secondDate, int diffDate)
        {
            return (int)(firstDate - secondDate).TotalSeconds < diffDate;
        }
    }
}
