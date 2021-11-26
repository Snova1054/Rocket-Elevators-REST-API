using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RocketElevatorsRESTAPI.Models;

namespace RocketElevatorsRESTAPI.Controllers
{
    [Route("api/buildings")]
    [ApiController]
    public class BuildingsController : ControllerBase
    {
        private readonly TodoContext _context;

        public BuildingsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Buildings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Building>>> Getbuildings()
        {
            return await _context.buildings.ToListAsync();
        }

        // GET: api/Buildings/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Building>> GetBuilding(int id)
        {
            var building = await _context.buildings.FindAsync(id);

            if (building == null)
            {
                return NotFound();
            }

            return building;
        }
        [HttpGet("{status}")]
        public async Task<ActionResult<IEnumerable<Building>>> GetLeadsInfos(string status)
        {
            List<Building> buildingsList = new List<Building>();
            List<int> listBuildingIDs = new List<int>();

            List<Battery> batteriesList = (await _context.batteries.ToListAsync()).Where(battery => battery.status == status).ToList();
            for (int b = 0; b < batteriesList.Count(); b++)
            {
                listBuildingIDs.Add(batteriesList[b].building_id);
            }

            List<Column> columnsList = (await _context.columns.ToListAsync()).Where(column => column.status == status).ToList();
            for (int c = 0; c < columnsList.Count(); c++)
            {
                if (!listBuildingIDs.Contains((await _context.batteries.FindAsync(columnsList[c].battery_id)).building_id))
                {
                    listBuildingIDs.Add((await _context.batteries.FindAsync(columnsList[c].battery_id)).building_id);
                }
            }

            List<Elevator> elevatorsList = (await _context.elevators.ToListAsync()).Where(elevator => elevator.status == status).ToList();
            for (int e = 0; e < elevatorsList.Count(); e++)
            {
                if (!listBuildingIDs.Contains((await _context.batteries.FindAsync((await _context.columns.FindAsync(elevatorsList[e].column_id)).battery_id)).building_id))
                {
                    listBuildingIDs.Add((await _context.batteries.FindAsync((await _context.columns.FindAsync(elevatorsList[e].column_id)).battery_id)).building_id);
                }
            }
            listBuildingIDs = listBuildingIDs.Distinct().ToList();
            listBuildingIDs.Sort();
            
            for (int i = 0; i < listBuildingIDs.Count(); i++)
            {
                buildingsList.Add(await _context.buildings.FindAsync(listBuildingIDs[i]));
                Console.WriteLine(buildingsList[i].id);
            }


            return buildingsList;
        }

        // PUT: api/Buildings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuilding(int id, Building building)
        {
            if (id != building.id)
            {
                return BadRequest();
            }

            _context.Entry(building).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildingExists(id))
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

        // POST: api/Buildings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Building>> PostBuilding(Building building)
        {
            _context.buildings.Add(building);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuilding", new { id = building.id }, building);
        }

        // DELETE: api/Buildings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuilding(int id)
        {
            var building = await _context.buildings.FindAsync(id);
            if (building == null)
            {
                return NotFound();
            }

            _context.buildings.Remove(building);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BuildingExists(int id)
        {
            return _context.buildings.Any(e => e.id == id);
        }
    }
}
