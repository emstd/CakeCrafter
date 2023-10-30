using CakeCrafter.API.Data;
using CakeCrafter.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CakeCrafter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasureUnitsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MeasureUnitsController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Taste>>> GetMeasureUnits()
        {
            return Ok(await _context.MeasureUnits.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ingredient>> GetMeasureUnit(int id)
        {
            var measureUnit = await _context.MeasureUnits.FindAsync(id);
            if (measureUnit == null)
            {
                return BadRequest("Measure Unit not found!");
            }
            return Ok(measureUnit);
        }

        [HttpPost]
        public async Task<ActionResult<List<Ingredient>>> AddMeasureUnit(MeasureUnit measureUnit)
        {
            _context.MeasureUnits.Add(measureUnit);
            await _context.SaveChangesAsync();
            return Ok(await _context.MeasureUnits.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Ingredient>>> UpdateMeasureUnit(MeasureUnit inputMeasureUnit)
        {
            var dbMeasureUnit = await _context.MeasureUnits.FindAsync(inputMeasureUnit.Id);
            if (dbMeasureUnit == null)
            {
                return BadRequest("Measure Unit not found!");
            }
            dbMeasureUnit.Name = inputMeasureUnit.Name;

            await _context.SaveChangesAsync();

            return Ok(await _context.MeasureUnits.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<MeasureUnit>>> DeleteMeasureUnit(int id)
        {
            var dbMeasureUnit = await _context.MeasureUnits.FindAsync(id);
            if (dbMeasureUnit == null)
            {
                return BadRequest("Measure Unit not found!");
            }
            _context.MeasureUnits.Remove(dbMeasureUnit);
            await _context.SaveChangesAsync();
            return Ok(await _context.MeasureUnits.ToListAsync());
        }
    }
}
