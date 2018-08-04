using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PeopleController : ApiController
    {
        private dxcchallengeEntities1 db = new dxcchallengeEntities1();

        /// <summary>
        /// User Story 1
        /// Example:
        /// GET: api/People/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        [ResponseType(typeof(Person))]
        public IHttpActionResult GetPerson(int id)
        {
            Person person = db.People.Find(id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }


        /// <summary>
        /// User Story 2
        /// Example:
        /// GET: api/People
        /// </summary>
        /// <returns></returns>
        public IQueryable<Person> GetPeople()
        {
            return db.People;
        }

        /// <summary>
        /// User Story 3
        /// Example:
        /// GET: api/People/getbylastname/Ferna
        /// </summary>
        /// <param name="lastname"></param>
        /// <returns></returns>
        [Route ("api/People/getbylastname/{lastname}")]
        [HttpGet]
        [ResponseType(typeof(Person))]
        public IHttpActionResult GetPersonByLastName(string lastname)
        {
            var people = db.People;
            var query = from p in people where p.lastname.StartsWith(lastname) select p;
            if (query.Any())
            {
                return Ok(query);
            }
            return NotFound();
        }

        /// <summary>
        /// User Story 4
        /// Example:
        /// DELETE: api/People/deletebyssn/506-15-5976
        /// </summary>
        /// <returns></returns>
        [Route("api/People/deletebyssn/{ssn}")]
        [ResponseType(typeof(Person))]
        public IHttpActionResult DeletePerson(string ssn)
        {
            var people = db.People;
            var query = from p in people where p.ssn.Equals(ssn) select p;
            if (query.Any())
            {
                var person = query.First();
                db.People.Remove(person);
                db.SaveChanges();

                return Ok(person);
            }
            return NotFound();
        }

        /// <summary>
        /// User Story 5
        /// Example:
        /// GET: api/Peoplebylastname
        /// </summary>
        /// <returns></returns>
        /// [Route ("api/People/getbylastname/{lastname}")]
        [Route("api/Peoplebylastname")]
        [HttpGet]
        public IQueryable<Person> GetPeopleByLastname()
        {
            return db.People.OrderBy(p => p.lastname);
        }

        /// <summary>
        /// User Story 6
        /// Example:
        /// GET: api/People/getbyssn/680-40-7931
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        [Route("api/People/getbyssn/{ssn}")]
        [HttpGet]
        [ResponseType(typeof(Person))]
        public IHttpActionResult GetPersonBySsn(string ssn)
        {
            var people = db.People;
            var query = from p in people where p.ssn.Equals(ssn) select p;
            if (query.Any())
            {
                return Ok(query);
            }

            return NotFound();
            
        }

        /// <summary>
        /// User Story 7
        /// Example:
        /// POST: api/People/UpdatePerson/4
        /// And the body of the request should be in Json format with the data that you are going to update
        /// {"id":4,"name":"Cristian","lastname":"Gamboa","ssn":"445-04-5676","birthdate":"1985-09-08T23:14:00"}
        /// </summary>
        /// <returns></returns>
        [Route("api/People/UpdatePerson/{id}")]
        [HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdatePersonLastname(int id, Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != person.id)
            {
                return BadRequest();
            }

            db.Entry(person).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonExists(int id)
        {
            return db.People.Count(e => e.id == id) > 0;
        }
    }
}