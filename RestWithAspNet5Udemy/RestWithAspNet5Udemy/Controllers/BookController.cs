using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithAspNet5Udemy.Models;
using RestWithAspNet5Udemy.BLL.Interfaces;

namespace RestWithAspNet5Udemy.Controllers
{
    [ApiVersion("1")]
    [ApiController]
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
        public IActionResult Post([FromBody] Book book)
        {
            if (book == null)
                return BadRequest();

            return Ok(_bookBll.Create(book));
        }

        /// <summary>
        /// Maps PUT requests to https://localhost:{port}/api/book/
        /// [FromBody] consumes the JSON object sent in the request body
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromBody] Book book)
        {
            if (book == null)
                return BadRequest();

            return Ok(_bookBll.Update(book));
        }

        /// <summary>
        /// Maps DELETE requests to https://localhost:{port}/api/book/{id}
        /// receiving an ID as in the Request Path
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete ("{id}")]
        public IActionResult Delete(long id)
        {
            _bookBll.Delete(id);

            return NoContent();
        }
    }
}
