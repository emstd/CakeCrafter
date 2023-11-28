﻿using CakeCrafter.API.Contracts;
using CakeCrafter.Core.Interfaces.Services;
using CakeCrafter.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CakeCrafter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CakesController : ControllerBase
    {
        private readonly ICakeService _service;

        public CakesController(ICakeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cake>>> GetCakes([FromRoute] string category, [FromQuery] int pageNumber = 1)
        {
            var cakes = await _service.Get(category, pageNumber);
            var result = cakes.Select(cake => new GetCakeResponse()
            {
                Id = cake.Id,
                Name = cake.Name,
                Description = cake.Description,
                TasteId = cake.TasteId,
                CategoryId = cake.CategoryId,
                CookTime = cake.CookTime,
                Level = cake.Level,
                Weight = cake.Weight,
            });
            return Ok(await _service.Get(category, pageNumber));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cake>> GetCakeById(int id)
        {
            var result = await _service.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            var cake = new GetCakeResponse
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                TasteId = result.TasteId,
                CategoryId = result.CategoryId,
                CookTime = result.CookTime,
                Level = result.Level,
                Weight = result.Weight,
            };
            return Ok(cake);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateCake(CreateCakeRequest cakeRequest)
        {
            var cake = new Cake{
                Name = cakeRequest.Name,
                Description = cakeRequest.Description,
                TasteId = cakeRequest.TasteId,
                CategoryId = cakeRequest.CategoryId,
                CookTime = cakeRequest.CookTime,
                Level = cakeRequest.Level,
                Weight = cakeRequest.Weight
            };
            return Ok(await _service.Create(cake));
        }

        [HttpPut]
        public async Task<ActionResult<List<Cake>>> UpdateCake(CreateCakeRequest cakeRequest)
        {
            var cake = new Cake
            {
                Name = cakeRequest.Name,
                Description = cakeRequest.Description,
                TasteId = cakeRequest.TasteId,
                CategoryId = cakeRequest.CategoryId,
                CookTime = cakeRequest.CookTime,
                Level = cakeRequest.Level,
                Weight = cakeRequest.Weight
            };

            var result = await _service.Update(cake);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Cake>>> DeleteCake(int id)
        {
            var result = await _service.Delete(id);
            return Ok(result);
        }
    }
}
