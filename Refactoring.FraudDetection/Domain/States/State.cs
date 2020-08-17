using Refactoring.FraudDetection.Infraestructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring.FraudDetection.Domain.States
{
    public class State
    {
        public string Abbreviature { get; private set; }
        public string Name { get; private set; }
        public State(string abbreviature, string name) 
        {
            Requires.NotNullOrEmpty(abbreviature, nameof(abbreviature));
            Requires.NotNullOrEmpty(name, nameof(name));
            this.Abbreviature = abbreviature.ToLower();
            this.Name = name.ToLower();
        }
    }
}
