using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User_Management.Repositories.Interfaces;

namespace User_Management.Bases
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseIntController<TEntity, TRepository> : ControllerBase
        where TEntity : class, IEntityInt
        where TRepository : IRepositoryInt<TEntity>
    {
        private readonly TRepository _repository;

        public BaseIntController(TRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<TEntity>> Get()
        {
            var result = await _repository.Get();
            return Ok(new { data = result });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> Get(int id)
        {
            var get = await _repository.Get(id);
            if (get == null)
            {
                return NotFound();
            }
            return Ok(new { data = get });
        }

        [HttpPost]
        public async Task<ActionResult<TEntity>> Post(TEntity entity)
        {
            await _repository.Post(entity);
            return CreatedAtAction("Get", new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, TEntity entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }
            var update = await _repository.Put(entity);
            return Ok(update);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TEntity>> Delete(int id)
        {
            var delete = await _repository.Delete(id);
            if (delete == null)
            {
                return NotFound();
            }
            return delete;
        }
    }
}
