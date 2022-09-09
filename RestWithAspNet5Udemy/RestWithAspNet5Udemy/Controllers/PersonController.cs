using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithAspNet5Udemy.BLL.Interfaces;
using RestWithAspNet5Udemy.Data.DTO;

namespace RestWithAspNet5Udemy.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonBLL _personBll;

        public PersonController(ILogger<PersonController> logger, IPersonBLL personBll)
        {
            _logger = logger;
            _personBll = personBll;
        }

        /// <summary>
        /// Maps GET requests to https://localhost:{port}/api/person
        /// Get no parameters for FindAll -> Search All
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personBll.FindAll());
        }

        /// <summary>
        /// Maps GET requests to https://localhost:{port}/api/person/{id}
        /// receiving an ID as in the Request Path
        /// Get with parameters for FindById -> Search by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var personDto = _personBll.FindById(id);
            
            if (personDto == null)
                return NotFound();
            
            return Ok(personDto);
        }

        /// <summary>
        /// Maps POST requests to https://localhost:{port}/api/person/
        /// [FromBody] consumes the JSON object sent in the request body
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] PersonDto personDto)
        {
            if (personDto == null)
                return BadRequest();

            return Ok(_personBll.Create(personDto));
        }

        /// <summary>
        /// Maps PUT requests to https://localhost:{port}/api/person/
        /// [FromBody] consumes the JSON object sent in the request body
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromBody] PersonDto personDto)
        {
            if (personDto == null)
                return BadRequest();

            return Ok(_personBll.Update(personDto));
        }

        /// <summary>
        /// Maps DELETE requests to https://localhost:{port}/api/person/{id}
        /// receiving an ID as in the Request Path
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete ("{id}")]
        public IActionResult Delete(long id)
        {
            _personBll.Delete(id);

            return NoContent();
        }
    }
}
