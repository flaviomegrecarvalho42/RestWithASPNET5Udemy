using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace RestWithAspNet5Udemy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger)
        {
            _logger = logger;
        }

        [HttpGet("sum/{firstNumber}/{secondNumber}")]
        public IActionResult GetSum(string firstNumber, string secondNumber)
        {
            if (!IsNumeric(firstNumber) || !IsNumeric(secondNumber))
                return BadRequest("Invalid Input");

            var sumNumbers = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber);

            return Ok(sumNumbers.ToString());
        }

        private static bool IsNumeric(string strNumber)
        {
            return double.TryParse(strNumber, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out _);
        }

        private static decimal ConvertToDecimal(string strNumber)
        {
            if (!decimal.TryParse(strNumber.Replace(",", "."), out decimal decimalValue))
                return 0;

            return decimalValue;
        }
    }
}
