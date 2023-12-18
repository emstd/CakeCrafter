﻿using AutoMapper;
using CakeCrafter.API.Contracts;
using CakeCrafter.Core.Interfaces.Services;
using CakeCrafter.Core.Models;
using CakeCrafter.Core.Pages;
using Microsoft.AspNetCore.Mvc;

namespace CakeCrafter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CakesController : ControllerBase
    {
        private readonly ICakeService _service;
        private readonly IMapper _mapper;

        public CakesController(ICakeService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ItemsPage<CakeGet>>> GetCakes([FromQuery] int categoryId, [FromQuery] int skip, [FromQuery] int take)
        {
            var cakesPage = await _service.Get(categoryId, skip, take);

            var PageResponse = new ItemsPage<CakeGet>
            {
                Items = cakesPage.Items.Select(cake => _mapper.Map<CakeGet>(cake)).ToArray(),
                TotalItems = cakesPage.TotalItems
            };

            return Ok(PageResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CakeGet>> GetCakeById(int id)
        {
            var cake = await _service.GetById(id);
            if (cake == null)
            {
                return NotFound();
            }
            var CakeResponse = _mapper.Map<CakeGet>(cake);
            return Ok(CakeResponse);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateCake(CakeCreate cakeCreate)
        {
            var cake = _mapper.Map<Cake>(cakeCreate);
            return Ok(await _service.Create(cake));
        }

        [HttpPut]
        public async Task<ActionResult<CakeCreate>> UpdateCake(CakeUpdate cakeUpdate)
        {
            var cake = _mapper.Map<Cake>(cakeUpdate);
            var updatedCake = await _service.Update(cake);
            if (updatedCake == null)
            {
                return NotFound();
            }
            return Ok(updatedCake);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Cake>>> DeleteCake(int id)
        {
            var result = await _service.Delete(id);
            return Ok(result);
        }
    }
}
