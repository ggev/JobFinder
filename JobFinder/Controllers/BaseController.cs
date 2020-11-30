using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace JobFinder.Controllers
{
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        protected int GetPersonId()
        {
            var personId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Convert.ToInt32(personId);
        }
    }
}