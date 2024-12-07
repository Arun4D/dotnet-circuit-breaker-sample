using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResiliencePatterns.CircuitBreaker
{
    internal interface ICircuitBreaker
    {
        bool IsCircuitOpen { get; }
        void ExecuteWithCircuitBreaker(Action action);
        void ResetCircuit();
    }
}
