using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bruh.Data;
using bruh.Models;

namespace bruh.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly AnimalService _service;

        public AnimalsController(AnimalService animalService)
        {
            _service = animalService;
        }

        // GET: api/Animals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimal()
        {
            IEnumerable<Animal>? animals = await _service.GetAll();
            if (animals == null) return StatusCode(StatusCodes.Status500InternalServerError);
            return Ok(await _service.GetAll());
        }

        // GET: api/Animals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Animal>> GetAnimal(int id)
        {
            Animal an = await _service.GetAnimal(id);

            return an;
        }

        // PUT: api/Animals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimal(int id, Animal animal)
        {
            await _service.PutAnimal(id, animal);

            return NoContent();
        }

        // POST: api/Animals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Animal>> PostAnimal(Animal animal)
        {
            await _service.PostAnimal(animal);

            return CreatedAtAction("GetAnimal", new { id = animal.Id }, animal);
        }

        // DELETE: api/Animals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> destroy(int id)
        {

            await _service.destroy(id);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> destroy()
        {

            await _service.destroy();

            return NoContent();
        }


    }
}
