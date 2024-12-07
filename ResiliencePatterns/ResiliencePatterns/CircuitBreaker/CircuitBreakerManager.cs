using Polly;
using Polly.CircuitBreaker;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ResiliencePatterns.CircuitBreaker
{
    public class CircuitBreakerManager
    {
        private readonly HttpClient _httpClient;
        private readonly CircuitBreakerPolicy _circuitBreakerPolicy;

        public CircuitBreakerManager(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _circuitBreakerPolicy = Policy
                 .Handle<Exception>() // Handle exceptions (you can specify specific exceptions, e.g., HttpRequestException)
                 .CircuitBreaker(
                     3, TimeSpan.FromSeconds(30), // Break after 3 failures, hold the circuit open for 30 seconds
                     onBreak: (exception, timespan) => { /* Handle circuit broken logic */ },
                     onReset: () => { /* Handle circuit reset logic */ }
                 );
        }

        public async Task<string> CallApiWithFallbackAsync()
        {
            try
            {
                // Use Polly's Circuit Breaker for API 1 call
                return await _circuitBreakerPolicy.Execute(async () =>
                {
                    // Call API 1
                    var api1Response = await _httpClient.GetStringAsync("http://localhost:5276/api/CustomerDetails/1");
                    return api1Response;
                });
            }
            catch (BrokenCircuitException)
            {
                // If the circuit is broken, call API 2 as a fallback
                Console.WriteLine("Circuit breaker is open, calling API 2 instead.");
                return await CallApi2Async();
            }
            catch (Exception ex)
            {
                // Handle any other exceptions (network issues, etc.)
                Console.WriteLine($"Error calling API 1: {ex.Message}. Fallback to API 2.");
                return await CallApi2Async();
            }
        }

        private async Task<string> CallApi2Async()
        {
            // Fallback API 2
            try
            {
                var api2Response = await _httpClient.GetStringAsync("http://localhost:5255/api/CgCustomerDetails/1");
                return api2Response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calling API 2: {ex.Message}");
                return "Both API 1 and API 2 failed.";
            }
        }
    }
}
