using DataAccessLayer.Implementation;
using DatabaseContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace MQTTConfigWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class MqttConfigController : ControllerBase
    {
        private readonly ILogger<MqttConfigController> _logger;
        private readonly MQTTConfigContext _context;

        public MqttConfigController(ILogger<MqttConfigController> logger, MQTTConfigContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        //[AllowAnonymous]
        public IEnumerable<MQTTConfig> GetAll()
        {
            using var _repository = new MQTTConfigRepository(_context);
            return _repository.GetAll();
        }
        [HttpGet("app/{guid}")]
        //[AllowAnonymous]
        public async Task<ActionResult<MQTTConfig>> GetMQTTConfigByGuid(Guid guid)
        {
            using var repository = new MQTTConfigRepository(_context);
            var config = await repository.GetValueByGuidAsync(guid);

            if (config == null)
            {
                return NotFound();
            }

            return config;
        }

        [HttpGet("{id}")]
        //[AllowAnonymous]
        public async Task<ActionResult<MQTTConfig>> GetMQTTConfigById(int id)
        {
            using var repository = new MQTTConfigRepository(_context);
            var config = await repository.GetValueAsync(id);

            if (config == null)
            {
                return NotFound();
            }

            return config;
        }
        [HttpPost]
        //[AllowAnonymous]
        public async Task<ActionResult<MQTTConfig>> PostMQTTConfig(MQTTConfig purchase)
        {
            using var repository = new MQTTConfigRepository(_context);
            purchase = await repository.SaveAsync(purchase);
            return purchase;
        }
        [HttpDelete("{id}")]
        //[AllowAnonymous]
        public async Task<IActionResult> DeleteMQTTConfig(int id)
        {
            using var repository = new MQTTConfigRepository(_context);
            await repository.DeleteAsync(id);

            return NoContent();
        }
        [HttpPut("{id}")]
        //[AllowAnonymous]
        public async Task<ActionResult<MQTTConfig>> PutMQTTConfig(int id, MQTTConfig model)
        {
            if (model.Id != id)
            {
                return BadRequest();
            }

            using var repository = new MQTTConfigRepository(_context);
            var config = await repository.GetValueAsync(id);

            if (config == null)
            {
                return NotFound();
            }

            config.Update(model);
            await repository.UpdateAsync(config);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await repository.UpdateAsync(config);

            return config;

        }
    }
}
