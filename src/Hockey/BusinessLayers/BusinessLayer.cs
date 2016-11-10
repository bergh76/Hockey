using Hockey.Data;
using Hockey.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hockey.BusinessLayers
{
    public class BusinessLayer
    {

        public static string teamMessage;
        public static string productMessage;
        public static string categoryMessage;
        public static string subcatMessage;

        private readonly ApplicationDbContext _context;
        public BusinessLayer(ApplicationDbContext context)
        {
            _context = context;
        }

        internal async Task AddTeam(ApplicationDbContext context, Team team)
        {
            var nameInput = team.TeamName;
            var exists = context.Team.Where(x => x.TeamName == nameInput).Select(x => x.TeamName).FirstOrDefault();
            do while (nameInput == exists)
                {
                    teamMessage = "Laget finns redan";
                    return;
                }
            while (false);
            var v = context.Team.ToList().Select(x => x.TeamId).Count();
            if (v == 0)
            {
                int tempV = 9001;
                team.TeamId = tempV;
            }
            else
            {
                var getLastID = context.Team.ToList().OrderBy(x => x.TeamId).Select(x => x.TeamId).Last();
                team.TeamId = getLastID + 1;
            }
            context.Add(team);
            await context.SaveChangesAsync();
        }
    }
}
