
using MrUnrealBusiness.DataRepository;
using MrUnrealData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MrUnreal.Controllers
{
    public class CreationController : ApiController
    {
        DataRepository<Creation> repo;

        public CreationController()
        {
            repo = new DataRepository<Creation>();
        }

        [HttpGet]
        public IEnumerable<Creation> GetAllCreations()
        {
            var creation = repo.GetAll();
            return creation;
        }

        public IHttpActionResult GetCreation(int id)
        {
            var creation = repo.GetOne(id);
            if (creation == null)
            {
                return NotFound();
            }
            return Ok(creation);
        }

        public void PostCreation(Creation creation)
        {
            if (creation == null)
            {
                BadRequest("Invalid product data.");
                return;
            }

            repo.NewOne(creation);
        }

        [HttpPut]
        public IHttpActionResult PutCreation(Creation creation)
        {
            repo.Put(creation);
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteCreation(int id)
        {
            repo.Delete(id);
            return Ok();
        }
    }
}
