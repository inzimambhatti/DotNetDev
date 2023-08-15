using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using first_project.Models;
using first_project.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace first_project.Controllers
{
    public class PersonsControllers : Controller
    {
        public readonly IPersonsRepo personsRepo;
        public PersonsControllers( IPersonsRepo personsRepo)
        {
            this.personsRepo = personsRepo;
        }

        // GET: /<controller>/
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAllPersons ()
        {
            var _list = await this.personsRepo.GetAllPersons();
            if (_list != null)
            {
                return Ok(_list);
            }
            else {

                return NotFound();
            }

        }
        //Get Person by Id
        [HttpGet("GetAll/{id}")]
        public async Task<ActionResult> GetPersonbyId(int id)
        {
            var _list = await this.personsRepo.GetPersonsbyID(id);
            if (_list != null)
            {
                return Ok(_list);
            }
            else
            {

                return NotFound();
            }

        }
        [HttpPost("CreatePerson")]
        public async Task<IActionResult> CreatePerson([FromBody] PersonsModel personsModel)
        {
            var _result = await this.personsRepo.CreatePerson(personsModel);
            return Ok(_result);
        }

        [HttpPut("UpdatePerson")]
        public async Task<IActionResult> UpdatePerson([FromBody] PersonsModel personsModel, int id)
        {
            var _result = await this.personsRepo.UpdatePerson(personsModel, id);
            return Ok(_result);
        }

        [HttpDelete("DeletePerson")]
        public async Task<IActionResult> Remove(int id)
        {
            var _result = await this.personsRepo.DeletePerson(id);
            return Ok(_result);
        }

    }
}

