using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;

namespace RestWithAspNet5Udemy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ILogger<CalculatorController> _logger;
        private bool isNumeber;

        public CalculatorController(ILogger<CalculatorController> logger)
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

        [HttpGet("subtraction/{firstNumber}/{secondNumber}")]
        public IActionResult GetSubtraction(string firstNumber, string secondNumber)
        {
            if (!IsNumeric(firstNumber) || !IsNumeric(secondNumber))
                return BadRequest("Invalid Input");

            var subtractNumbers = ConvertToDecimal(firstNumber) - ConvertToDecimal(secondNumber);

            return Ok(subtractNumbers.ToString());
        }

        [HttpGet("multiplication/{firstNumber}/{secondNumber}")]
        public IActionResult GetMultiplication(string firstNumber, string secondNumber)
        {
            if (!IsNumeric(firstNumber) || !IsNumeric(secondNumber))
                return BadRequest("Invalid Input");

            var multiplyNumbers = ConvertToDecimal(firstNumber) * ConvertToDecimal(secondNumber);

            return Ok(multiplyNumbers.ToString());
        }

        [HttpGet("division/{firstNumber}/{secondNumber}")]
        public IActionResult GetDivision(string firstNumber, string secondNumber)
        {
            if (!IsNumeric(firstNumber) || !IsNumeric(secondNumber))
                return BadRequest("Invalid Input");

            var divisionNumbers = ConvertToDecimal(firstNumber) / ConvertToDecimal(secondNumber);

            return Ok(divisionNumbers.ToString());
        }

        [HttpGet("average/{firstNumber}/{secondNumber}")]
        public IActionResult GetAverage(string firstNumber, string secondNumber)
        {
            if (!IsNumeric(firstNumber) || !IsNumeric(secondNumber))
                return BadRequest("Invalid Input");

            var averageNumbers = (ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber)) / 2;

            return Ok(averageNumbers.ToString());
        }

        [HttpGet("squareRoot/{firstNumber}/{secondNumber}")]
        public IActionResult GetSquareRoot(string number)
        {
            if (!IsNumeric(number))
                return BadRequest("Invalid Input");

            var squareRoot = Math.Sqrt((double)ConvertToDecimal(number));

            return Ok(squareRoot.ToString());
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
