using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResiliencePatterns.CircuitBreaker;
using System.Text.Json;

namespace MyApiGateway.Controllers
{
 
    [Route("api/[controller]")]
    [ApiController]
    public class ApiGatewayController : ControllerBase
    {

        private readonly HttpClient _httpClient;
        private readonly CircuitBreakerManager _circuitBreakerManager;

        public ApiGatewayController(HttpClient httpClient, CircuitBreakerManager circuitBreakerManager)
        {
            _httpClient = httpClient;
            _circuitBreakerManager = circuitBreakerManager;

        }

        [HttpGet("invoke")]
        public async Task<IActionResult> InvokeApi()
        {
            //var url = "http://localhost:5001/api/target/hello";

            try
            {
                //var response = await _httpClient.GetAsync(url);
                //response.EnsureSuccessStatusCode();

                //var content = await response.Content.ReadAsStringAsync();
                //return Ok($"Response from API 2: {content}");

                var content = await _circuitBreakerManager.CallApiWithFallbackAsync();
                return Ok($"Response from API 2: {content}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }


        [HttpGet("invoke-multiple-apis")]
        public async Task<IActionResult> InvokeMultipleApis()
        {
            var api1Url = "http://localhost:5255/api/CgCustomerDetails/1";
            var api2Url = "http://localhost:5276/api/CustomerDetails/1";

            try
            {
                // Invoke both APIs simultaneously
                var api1Response = _httpClient.GetAsync(api1Url);
                var api2Response = _httpClient.GetAsync(api2Url);

                await Task.WhenAll(api1Response, api2Response);

                if (!api1Response.Result.IsSuccessStatusCode || !api2Response.Result.IsSuccessStatusCode)
                {
                    return StatusCode(500, "One or both APIs failed.");
                }

                // Deserialize responses
                var api1Data = await api1Response.Result.Content.ReadAsStringAsync();
                var api2Data = await api2Response.Result.Content.ReadAsStringAsync();

                var result = new
                {
                    Api1 = JsonSerializer.Deserialize<object>(api1Data),
                    Api2 = JsonSerializer.Deserialize<object>(api2Data)
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }
    }
}
