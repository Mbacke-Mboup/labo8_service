using bruh.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bruh.Data
{

    public class AnimalService
    {
        private readonly bruhContext _context;

        public AnimalService(bruhContext context)
        {
            _context = context;
        }

        public bool ContextNull()
        {
            return _context == null || _context.Animal == null;
        }

        public async Task<IEnumerable<Animal>?> GetAll()
        {
            if (ContextNull())
            {
                return null;
            }
            return await _context.Animal.ToListAsync();
        }

        public async Task<Animal> GetAnimal(int id)
        {
            if (_context.Animal == null)
            {
                return null;
            }
            var animal = await _context.Animal.FindAsync(id);

            if (animal == null)
            {
                return null;
            }

            return animal;
        }

        public async Task<IActionResult> PutAnimal(int id, Animal animal)
        {
            if (id != animal.Id)
            {
                return null;
            }

            _context.Entry(animal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return null;
        }

        public async Task<ActionResult<Animal>> PostAnimal(Animal animal)
        {
            if (_context.Animal == null)
            {
                return null;
            }
            _context.Animal.Add(animal);
            await _context.SaveChangesAsync();

            return animal;
        }

        // DELETE: api/Animals/5
        public async Task<IActionResult> destroy(int id)
        {
            if (_context.Animal == null)
            {
                return null;
            }
            var animal = await _context.Animal.FindAsync(id);
            if (animal == null)
            {
                return null;
            }

            _context.Animal.Remove(animal);
            await _context.SaveChangesAsync();

            return null;
        }

        public async Task<ActionResult<bool>> destroy()
        {
            if (_context.Animal == null)
            {
                return false;
            }
            _context.Animal.RemoveRange(_context.Animal);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool AnimalExists(int id)
        {
            return (_context.Animal?.Any(e => e.Id == id)).GetValueOrDefault();
        }


    }
}
