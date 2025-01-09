using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Models;
using MQTTConfigWebApi.DataContext;
using MQTTConfigWebApi.Repo;

namespace MQTTConfigWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MqttConfigController : ControllerBase
    {
        private readonly ILogger<MqttConfigController> _logger;

        public MqttConfigController(ILogger<MqttConfigController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<MQTTConfig> GetAll()
        {
            using var _repository = new MQTTConfigRepository(new MQTTConfigContext());
            return _repository.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<MQTTConfig>> GetMQTTConfigById(int id)
        {
            using var repository = new MQTTConfigRepository(new MQTTConfigContext());
            var config = await repository.GetValueAsync(id);

            if (config == null)
            {
                return NotFound();
            }

            return config;
        }
        [HttpPost]
        public async Task<ActionResult<MQTTConfig>> PostMQTTConfig(MQTTConfig purchase)
        {
            using var repository = new MQTTConfigRepository(new MQTTConfigContext());
            purchase = await repository.SaveAsync(purchase);
            return purchase;
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMQTTConfig(int id)
        {
            using var repository = new MQTTConfigRepository(new MQTTConfigContext());
            await repository.DeleteAsync(id);

            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchMQTTConfig(int id, [FromBody] JsonPatchDocument<MQTTConfig> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            using var repository = new MQTTConfigRepository(new MQTTConfigContext());
            var config = await repository.GetValueAsync(id);

            if (config == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(config);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await repository.UpdateAsync(config);

            return NoContent();

        }
    }
}
