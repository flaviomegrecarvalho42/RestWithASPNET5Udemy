using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithAspNet5Udemy.BLL.Interfaces;
using RestWithAspNet5Udemy.Data.DTO;
using RestWithAspNet5Udemy.Hypermedia.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace RestWithAspNet5Udemy.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IBookBLL _bookBll;

        public BookController(ILogger<PersonController> logger, IBookBLL bookBll)
        {
            _logger = logger;
            _bookBll = bookBll;
        }

        /// <summary>
        /// Maps GET requests to https://localhost:{port}/api/book
        /// Get no parameters for FindAll -> Search All
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<BookDto>))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_bookBll.FindAll());
        }

        /// <summary>
        /// Maps GET requests to https://localhost:{port}/api/book/{id}
        /// receiving an ID as in the Request Path
        /// Get with parameters for FindById -> Search by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(BookDto))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult GetById(long id)
        {
            var person = _bookBll.FindById(id);
            
            if (person == null)
                return NotFound();
            
            return Ok(person);
        }

        /// <summary>
        /// Maps POST requests to https://localhost:{port}/api/book/
        /// [FromBody] consumes the JSON object sent in the request body
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(BookDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] BookDto bookDto)
        {
            if (bookDto == null)
                return BadRequest();

            return Created("book", _bookBll.Create(bookDto));
        }

        /// <summary>
        /// Maps PUT requests to https://localhost:{port}/api/book/
        /// [FromBody] consumes the JSON object sent in the request body
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPut]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(BookDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] BookDto bookDto)
        {
            if (bookDto == null)
                return BadRequest();

            return Ok(_bookBll.Update(bookDto));
        }

        /// <summary>
        /// Maps DELETE requests to https://localhost:{port}/api/book/{id}
        /// receiving an ID as in the Request Path
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete ("{id}")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        public IActionResult Delete(long id)
        {
            _bookBll.Delete(id);

            return NoContent();
        }
    }
}
