using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring.FraudDetection.Domain.Orders
{
    public class State
    {
        public string Name { get; private set; }
        public State(string name) 
        {
            this.Name = name.ToLower();
        }
    }
}
