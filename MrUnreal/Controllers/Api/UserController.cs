using MrUnrealBusiness.DataRepository;
using MrUnrealData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MrUnreal.Controllers.Api
{
    public class UserController : ApiController
    {
        DataRepository<User> repo = new DataRepository<User>();

        [HttpGet]
        public IEnumerable<User> GetAllUsers()
        {
            var user = repo.GetAll();
            return user;
        }
    }
}
