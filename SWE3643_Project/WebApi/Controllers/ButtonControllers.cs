using System.Net;
using Microsoft.AspNetCore.Mvc;
using Console;
//using System.Web.Http;
//using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("Test")]
    public class ClearButton : Controller
    {
        //[HttpPost]
        [Route("Clear")]
        public IActionResult ClearInput()
        {
            return Content("Cleared.");
        }

        //[HttpPost]
        [Route("Trim/{input}")]
        [HttpGet]
        public IActionResult TrimInput(string input)
        {
            System.Console.WriteLine(input);
            string output = new string(input.Where(c => !("aeiouyAEIOUY ").Contains(c)).ToArray());
            return Content(output);
        }
    }

    public static class InputFormatter
    {
        public static double[] ParseMultiLineInput(string input)
        {
            string output = new string(input.Where(c => ("-0123456789. \n").Contains(c)).ToArray());
            string[] values = output.Split('\n');
            List<double> doubleValues = new List<double>();
            for (int i = 0; i < values.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(values[i])) continue;
                values[i] = values[i].Trim().Split(' ')[0];
                if (values[i].Contains('.'))
                {
                    string firstHalf = values[i].Split('.')[0];
                    string secondHalf = values[i].Split('.')[1];
                    doubleValues.Add(double.Parse(firstHalf + "." + secondHalf));
                }
                else doubleValues.Add(double.Parse(values[i]));
            }
            return doubleValues.ToArray();
        }

        public static double[] ParseSingleLineInput(string input)
        {
            string output = new string(input.Where(c => ("-0123456789., \n").Contains(c)).ToArray()).Trim();
            output = output.Replace(',', ' ');
            output = output.Split('\n')[0].Replace(' ', '\n');
            return ParseMultiLineInput(output);
        }

        public static ValuePair[] ParsePairedLinesInput(string input)
        {
            string output = new string(input.Where(c => ("-0123456789.,\n").Contains(c)).ToArray()).Trim();
            string[] values = output.Split('\n');
            List<ValuePair> valuePairs = new List<ValuePair>();
            for (int i = 0; i < values.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(values[i])) continue;
                string[] splitPair = values[i].Split(',');
                if ((splitPair.Length != 2 && (splitPair.Length != 3 || string.IsNullOrWhiteSpace(splitPair[2]) == false)) || (string.IsNullOrWhiteSpace(splitPair[0]) || string.IsNullOrWhiteSpace(splitPair[1])))
                {
                    throw new ArgumentException("Each line must be in the format of 'x,y'!");
                }
                valuePairs.Add(new ValuePair(double.Parse(splitPair[0]), double.Parse(splitPair[1])));
            }

            return valuePairs.ToArray();
        }
    }

    [ApiController]
    [Route("DeviationCalc")]
    public class DeviationCalculator : Controller
    {
        [Route("SSD/{input}")]
        [HttpGet]
        public IActionResult SampleStandardDeviation(string input)
        {
            char[] validChars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.', '\n', '-' };
            if (input.Any(c => !validChars.Contains(c)))
            {
                bool isUsingComma = input.Contains(',') || input.Contains(' ');
                Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                return Content("Input contains non-numeric characters!" + (isUsingComma ? " Please create a new line for each value and don't use commas or spaces!" : ""));
            }

            try
            {
                return Content(Console.DeviationFunctions.SampleStandardDeviation(InputFormatter.ParseMultiLineInput(input))
                    .ToString());
            }
            catch (ArgumentException e)
            {
                Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                return Content(e.Message);
            }
        }

        [Route("PSD/{input}")]
        [HttpGet]
        public IActionResult PopulationStandardDeviation(string input)
        {
            char[] validChars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.', '\n', '-' };
            if (input.Any(c => !validChars.Contains(c)))
            {
                bool isUsingComma = input.Contains(',') || input.Contains(' ');
                Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                return Content("Input contains non-numeric characters!" + (isUsingComma ? " Please create a new line for each value and don't use commas or spaces!" : ""));
            }

            try
            {
                return Content(Console.DeviationFunctions.PopulationStandardDeviation(InputFormatter.ParseMultiLineInput(input))
                    .ToString());
            }
            catch (ArgumentException e)
            {
                Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                return Content(e.Message);
            }
        }
        
        [Route("MEAN/{input}")]
        [HttpGet]
        public IActionResult Mean(string input)
        {
            char[] validChars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.', '\n', '-' };
            if (input.Any(c => !validChars.Contains(c)))
            {
                bool isUsingComma = input.Contains(',') || input.Contains(' ');
                Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                return Content("Input contains non-numeric characters!" + (isUsingComma ? " Please create a new line for each value and don't use commas or spaces!" : ""));
            }

            try
            {
                return Content(Console.DeviationFunctions.Mean(InputFormatter.ParseMultiLineInput(input))
                    .ToString());
            }
            catch (ArgumentException e)
            {
                Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                return Content(e.Message);
            }
        }
        
        [Route("ZSCORE/{input}")]
        [HttpGet]
        public IActionResult ZScore(string input)
        {
            char[] validChars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.', ' ', ',', '-' };
            if (input.Any(c => !validChars.Contains(c)))
            {
                bool isUsingComma = input.Contains(',') || input.Contains(' ');
                Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                return Content("Input contains non-numeric characters!" + (isUsingComma ? " Please enter three values on a single line!" : ""));
            }

            double[] values = InputFormatter.ParseSingleLineInput(input);
            if (values.Length != 3)
            {
                Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                return Content("Please enter three values!");
            }

            try
            {
                return Content(Console.DeviationFunctions.ZScore(values[0], values[1], values[2])
                    .ToString());
            }
            catch (ArgumentException e)
            {
                Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                return Content(e.Message);
            }
        }
        
        [Route("SLRF/{input}")]
        [HttpGet]
        public IActionResult SingleLinearRegressionFormula(string input)
        {
            char[] validChars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.', ',', '\n', '-' };
            if (input.Any(c => !validChars.Contains(c)))
            {
                Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                return Content("Input contains non-numeric characters!");
            }

            try
            {
                double slope;
                double intercept;
                Console.RegressionFunctions.SingleLinearRegression(InputFormatter.ParsePairedLinesInput(input), out slope, out intercept);
                return Content("y = " + slope + "x + " + intercept);
            }
            catch (ArgumentException e)
            {
                Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                return Content(e.Message);
            }
        }
        
        [Route("PYLR/{input}")]
        [HttpGet]
        public IActionResult PredictYLinearRegression(string input)
        {
            char[] validChars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.', ' ', ',', '-' };
            if (input.Any(c => !validChars.Contains(c)))
            {
                bool isUsingComma = input.Contains(',') || input.Contains(' ');
                Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                return Content("Input contains non-numeric characters!" + (isUsingComma ? " Please enter three values on a single line!" : ""));
            }

            double[] values = InputFormatter.ParseSingleLineInput(input);
            if (values.Length != 3)
            {
                Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                return Content("Please enter three values!");
            }

            try
            {
                return Content("y = " + Console.RegressionFunctions.PredictYLinearRegression(values[0], values[1], values[2]));
            }
            catch (ArgumentException e)
            {
                Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                return Content(e.Message);
            }
        }
    }
}