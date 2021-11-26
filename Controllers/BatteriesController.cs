using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RocketElevatorsRESTAPI.Models;
using Microsoft.Data.SqlClient;


namespace RocketElevatorsRESTAPI.Controllers
{
    [Route("api/batteries")]
    [ApiController]
    public class BatteriesController : ControllerBase
    {
        private readonly TodoContext _context;

        public BatteriesController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Batteries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Battery>>> GetBatteries()
        {
            
            return await _context.batteries.ToListAsync();
        }

        // GET: api/Batteries/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Battery>> GetBatteryID(int id)
        // public async Task<ActionResult<Battery>> GetBattery(string id)
        {
            
            var battery = await _context.batteries.FindAsync(id);

            if (battery == null)
            {
                return NotFound();
            }

            return battery;

        }

        // GET: api/Batteries/requestedInfo
        //
        [HttpGet("{requestedInfo}")]
        public async Task<ActionResult<IEnumerable<Battery>>> GetBatteriesInfos(string requestedInfo)
        {
            List<Battery> batteriesList = await _context.batteries.ToListAsync();
            List<Battery> filteredList = new List<Battery>();
            filteredList = batteriesList.Where(battery => battery.status == requestedInfo).ToList();

            if (filteredList == null)
            {
                return NotFound();
            }
            else
            {
                return filteredList;
            }
        }

        // PUT: api/Batteries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBattery(int id, Battery battery)
        {
            if (id != battery.id)
            {
                return BadRequest();
            }

            _context.Entry(battery).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BatteryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Batteries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Battery>> PostBattery(Battery battery)
        {
            _context.batteries.Add(battery);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBattery", new { id = battery.id }, battery);
        }

        // DELETE: api/Batteries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBattery(int id)
        {
            var battery = await _context.batteries.FindAsync(id);
            if (battery == null)
            {
                return NotFound();
            }

            _context.batteries.Remove(battery);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BatteryExists(int id)
        {
            return _context.batteries.Any(e => e.id == id);
        }
    }
}
