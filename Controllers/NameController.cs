using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NameController(DataContext context) : ControllerBase
    {

        [HttpGet("names")]
        public async Task<ActionResult<IEnumerable<Name>>> GetAllNames() {

            return await context.Names.ToListAsync();

        }



        [HttpGet("names/name/{id}")]
        public async Task<Name> GetName(int id)
        {
            Name name = await context.Names.FindAsync(id) ?? throw new Exception("Name with given ID does not exist");

            return name;
        }



        [HttpPost("names/name")]
        public async Task<ActionResult<IEnumerable<Name>>> PostName(NameDto nameInput) {
    
            var newName = new Name
            {
                Person = nameInput.Person.ToLower()
            };

            bool nameExists = await context.Names.AnyAsync(e => e.Person == newName.Person);
            if  (nameExists) 
            {
                return BadRequest("Name already exists in database");
            }


            context.Names.Add(newName);
            await context.SaveChangesAsync();
            
            return await context.Names.ToListAsync();
        }

        [HttpDelete("names/name/{id}")]
        public async Task<ActionResult<IEnumerable<Name>>> DeleteName(int id) 
        {
            Name nameToBeDeleted = await context.Names.FindAsync(id) 
            ?? throw new Exception("Given ID does not exist");

            context.Names.Remove(nameToBeDeleted);
            await context.SaveChangesAsync();

            return await context.Names.ToListAsync();

        }

        // to update, give the Id and newusername
        // find the entity at the given Id
        // change its name to the newusername
        // save changes

        [HttpPut("names/name/{id}:{nameInput}")]
        
        public async Task<ActionResult<IEnumerable<Name>>> UpdateUser(int id, string nameInput){

            Name NameToBeUpdated = await context.Names.FindAsync(id) 
            ?? throw new Exception("Name at given Id does not exist.");

            string OldName = NameToBeUpdated.Person;
            NameToBeUpdated.Person = nameInput;

            await context.SaveChangesAsync();

            return await context.Names.ToListAsync();
        }

    }
}