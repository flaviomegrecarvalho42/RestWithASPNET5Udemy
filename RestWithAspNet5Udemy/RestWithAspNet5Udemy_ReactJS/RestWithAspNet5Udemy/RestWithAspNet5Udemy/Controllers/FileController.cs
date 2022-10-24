using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNet5Udemy.BLL.Interfaces;
using RestWithAspNet5Udemy.Data.DTO;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RestWithAspNet5Udemy.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class FileController : ControllerBase
    {
        private readonly IFileBLL _fileBll;

        public FileController(IFileBLL fileBll)
        {
            _fileBll = fileBll;
        }
               
        /// <summary>
        /// Maps POST requests to https://localhost:{port}/api/book/
        /// [FromBody] consumes the JSON object sent in the request body
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost("uploadFile")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FileDetailDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        public async Task<IActionResult> PostUploadFile([FromForm] IFormFile file)
        {
            FileDetailDto fileDetail = await _fileBll.SaveFile(file);

            return Created("file", fileDetail);
        }

        /// <summary>
        /// Maps POST requests to https://localhost:{port}/api/book/
        /// [FromBody] consumes the JSON object sent in the request body
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost("uploadMultipleFile")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(List<FileDetailDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        public async Task<IActionResult> PostUploadMultipleFile([FromForm] List<IFormFile> files)
        {
            List<FileDetailDto> filesDetail = await _fileBll.SaveFiles(files);

            return Created("file", filesDetail);
        }

        /// <summary>
        /// Maps POST requests to https://localhost:{port}/api/book/
        /// [FromBody] consumes the JSON object sent in the request body
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpGet("downloadFile/{fileName}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(byte[]))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/octet-stream")]
        public async Task<IActionResult> GetDownloadFile(string fileName)
        {
            byte[] buffer = _fileBll.GetFile(fileName);

            if (buffer != null)
            {
                HttpContext.Response.ContentType = $"application/{Path.GetExtension(fileName).Replace(".", "")}";
                HttpContext.Response.Headers.Add("content-length", buffer.Length.ToString());

                await HttpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length);
            }

            return new ContentResult();
        }

        /// <summary>
        /// Maps POST requests to https://localhost:{port}/api/book/
        /// [FromBody] consumes the JSON object sent in the request body
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpGet("downloadFileStreamResult/{fileName}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FileStreamResult))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/octetstream")]
        public async Task<IActionResult> GetDownloadFileStreamResult(string fileName)
        {
            var response = await _fileBll.GetFileStreamResult(fileName);

            return File(response.Memory, response.ExtensionType, response.FileName);
        }
    }
}
