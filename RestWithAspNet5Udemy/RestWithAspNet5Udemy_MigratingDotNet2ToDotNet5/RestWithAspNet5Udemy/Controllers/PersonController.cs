using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithAspNet5Udemy.BLL.Interfaces;
using RestWithAspNet5Udemy.Data.DTO;
using RestWithAspNet5Udemy.Hypermedia.Filters;
using System.Collections.Generic;

namespace RestWithAspNet5Udemy.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonBLL _personBll;

        public PersonController(IPersonBLL personBll)
        {
            _personBll = personBll;
        }

        /// <summary>
        /// Maps GET requests to https://localhost:{port}/api/person
        /// Get no parameters for FindAll -> Search All
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PersonDto>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [TypeFilter(typeof(HyperMediaFilter))]
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult GetById(long id)
        {
            var personDto = _personBll.FindById(id);
            
            if (personDto == null)
                return NotFound();
            
            return Ok(personDto);
        }

        /// <summary>
        /// Maps GET requests to https://localhost:{port}/api/person/{id}
        /// receiving an ID as in the Request Path
        /// Get with parameters for FindById -> Search by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("byName")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult GetByName([FromQuery] string firstName, string lastName)
        {
            var personDto = _personBll.FindByName(firstName, lastName);

            if (personDto == null)
                return NotFound();

            return Ok(personDto);
        }

        /// <summary>
        /// Maps GET requests to https://localhost:{port}/api/person
        /// Get no parameters for FindAll -> Search All
        /// </summary>
        /// <returns></returns>
        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PersonDto>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult GetWithPagedSearch([FromQuery] string name,
                                                            string sortDirection,
                                                            int pageSize,
                                                            int page)
        {
            return Ok(_personBll.FindWithPagedSearch(name, sortDirection, pageSize, page));
        }

        /// <summary>
        /// Maps POST requests to https://localhost:{port}/api/person/
        /// [FromBody] consumes the JSON object sent in the request body
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PersonDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] PersonDto personDto)
        {
            if (personDto == null)
                return BadRequest();

            return Created("person", _personBll.Create(personDto));
        }

        /// <summary>
        /// Maps PUT requests to https://localhost:{port}/api/person/
        /// [FromBody] consumes the JSON object sent in the request body
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] PersonDto personDto)
        {
            if (personDto == null)
                return BadRequest();

            return Ok(_personBll.Update(personDto));
        }

        /// <summary>
        /// 
        /// receiving an ID as in the Request Path
        /// Get with parameters for FindById -> Search by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Patch(PersonDto personDto)
        {
            if (personDto == null)
                return BadRequest();

            var updatedPerson = _personBll.Update(personDto);

            if (updatedPerson == null)
                return BadRequest();

            return Ok(updatedPerson);
        }

        /// <summary>
        /// Maps DELETE requests to https://localhost:{port}/api/person/{id}
        /// receiving an ID as in the Request Path
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete ("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Delete(long id)
        {
            _personBll.Delete(id);

            return NoContent();
        }
    }
}
