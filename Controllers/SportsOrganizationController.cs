using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace JWTAuthentication.Controllers
{
    [Route("api/organizations")]
    public class SportsOrganizationController : Controller
    {
        [HttpGet, Authorize]
        public IEnumerable<Organization> Get()
        {
            string currName = "";
            var currentUser = HttpContext.User;
            var organizations = new Organization[] {
                new Organization
                    { TeamName = "Cincinnati Bengals",League = "National Football League",
                      Founded = DateTime.Parse("1967-05-24"), City = "Cincinnati", State = "Ohio"},
                new Organization
                    { TeamName = "Cincinnati Reds",League = "Major League Baseball",
                      Founded = DateTime.Parse("1880-10-06"), City = "Cincinnati", State = "Ohio"}
            };
            if (currentUser.HasClaim(c => c.Type == ClaimTypes.Name))
            {
                var name = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
                currName = name;
            }

            if (currName.Contains("Gage"))
            {
                organizations = organizations.Where(o => o.League.Contains("Football")).ToArray();
            }

            return organizations;

        }

        public class Organization
        {
            public string TeamName { get; set; }
            public string League { get; set; }
            public DateTime Founded { get; set; }
            public string City { get; set; }
            public string State { get; set; }
        }
    }
}
