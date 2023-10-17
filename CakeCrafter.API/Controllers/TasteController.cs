using CakeCrafter.API.Data;
using CakeCrafter.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CakeCrafter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasteController(AppDbContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //public async Task<ActionResult<List<Taste>>> GetTastes()
        //{
        //    return Ok(await _context.Tastes.ToListAsync());
        //}

        [HttpGet]
        public ActionResult<List<Taste>> GetTastes()
        {
            List<Taste> RequestTastes = new List<Taste>();
            Task GetTastesTask = Task.Run(() =>
            {
                var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer("Server=(localdb)\\mssqllocaldb;database=CakeCrafterdb;trusted_connection=true;TrustServerCertificate=True").Options;
                using (var context = new AppDbContext(options))
                {
                    RequestTastes = context.Tastes.ToList();
                }
            }
            );
            GetTastesTask.Wait();
            return Ok(RequestTastes);
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Taste>> GetTaste(int id)
        //{
        //    var taste = await _context.Tastes.FindAsync(id);
        //    if (taste == null)
        //    {
        //        return BadRequest("Taste not found!");
        //    }
        //    return Ok(taste);
        //}
        [HttpGet("{id}")]
        public ActionResult<Taste> GetTaste(int id)
        {
            Taste? RequestTastes = new Taste();
            Task GetTasteByIDTask = Task.Run(() =>
            {
                var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer("Server=(localdb)\\mssqllocaldb;database=CakeCrafterdb;trusted_connection=true;TrustServerCertificate=True").Options;
                using (var context = new AppDbContext(options))
                {
                    RequestTastes = context.Tastes.Find(id);
                }
            }
            );
            GetTasteByIDTask.Wait();
            if (RequestTastes == null)
            {
                return BadRequest("Taste not found!");
            }
            return Ok(RequestTastes);
        }


        //[HttpPost]
        //public async Task<ActionResult<List<Taste>>> AddTaste(Taste taste)
        //{
        //    _context.Add(taste);
        //    await _context.SaveChangesAsync();
        //    return Ok(await _context.Tastes.ToListAsync());
        //}
        [HttpPost]
        public ActionResult<List<Taste>> AddTaste(Taste taste)
        {
            List<Taste> RequestTastes = new List<Taste>();
            Task PostTastesTask = Task.Run(() =>
            {
                var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer("Server=(localdb)\\mssqllocaldb;database=CakeCrafterdb;trusted_connection=true;TrustServerCertificate=True").Options;
                using (var context = new AppDbContext(options))
                {
                    context.Add(taste);
                    context.SaveChanges();
                    RequestTastes = context.Tastes.ToList();
                }
            }
            );
            PostTastesTask.Wait();
            return Ok(RequestTastes);
        }


        //[HttpPut]
        //public async Task<ActionResult<List<Taste>>> UpdateTaste(Taste inputTaste)
        //{
        //    var dbTaste = await _context.Tastes.FindAsync(inputTaste.Id);
        //    if (dbTaste == null)
        //    {
        //        return BadRequest("Taste not found");
        //    }
        //    dbTaste.Name = inputTaste.Name;
        //    await _context.SaveChangesAsync();
        //    return Ok(await _context.Tastes.ToListAsync());
        //}
        [HttpPut]
        public ActionResult<List<Taste>> UpdateTaste(Taste inputTaste)
        {
            List<Taste> RequestTastes = new List<Taste>();
            Taste? dbTaste = new Taste();
            Task PutTasteTask = Task.Run(() =>
            {
                var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer("Server=(localdb)\\mssqllocaldb;database=CakeCrafterdb;trusted_connection=true;TrustServerCertificate=True").Options;
                using (var context = new AppDbContext(options))
                {
                    dbTaste = context.Tastes.Find(inputTaste.Id);
                    if (dbTaste != null)
                    {
                        dbTaste.Name = inputTaste.Name;
                    }
                    context.SaveChanges();
                    RequestTastes = context.Tastes.ToList();
                }
            }
            );
            PutTasteTask.Wait();
            if (dbTaste == null)
            {
                return BadRequest("Taste not found!");
            }
            return Ok(RequestTastes);
        }

        //[HttpDelete]
        //public async Task<ActionResult<List<Taste>>> DeleteTaste(int id)
        //{
        //    var dbTaste = await _context.Tastes.FindAsync(id);
        //    if (dbTaste == null)
        //    {
        //        return BadRequest("Taste not found!");
        //    }
        //    _context.Tastes.Remove(dbTaste);
        //    await _context.SaveChangesAsync();
        //    return Ok(await _context.Tastes.ToListAsync());
        //}
        [HttpDelete]
        public ActionResult<List<Taste>> DeleteTaste(int id)
        {
            List<Taste> RequestTastes = new List<Taste>();
            Taste? dbTaste = new Taste();
            Task FindTasteTask = Task.Run(() =>
            {
                var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer("Server=(localdb)\\mssqllocaldb;database=CakeCrafterdb;trusted_connection=true;TrustServerCertificate=True").Options;
                using (var context = new AppDbContext(options))
                {
                    dbTaste = context.Tastes.Find(id);
                    if (dbTaste != null)
                    {
                        context.Remove(dbTaste);
                    }
                    context.SaveChanges();
                    RequestTastes = context.Tastes.ToList();
                }
            }
            );
            FindTasteTask.Wait();
            if (dbTaste == null)
            {
                return BadRequest("Taste not found!");
            }

            return Ok(RequestTastes);
        }

    }
}
