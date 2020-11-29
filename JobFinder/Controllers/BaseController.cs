using System;
using System.Security.Claims;
using JobFinder.ActionFilters;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers
{
    [MakeActionResponse]
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