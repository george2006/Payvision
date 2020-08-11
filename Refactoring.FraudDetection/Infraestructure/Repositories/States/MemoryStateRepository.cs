using Refactoring.FraudDetection.Domain.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring.FraudDetection.Intraestructure.Repositories.States
{
    public class MemoryStateRepository : IStateRepository
    {
        private Dictionary<string, string> states = new Dictionary<string, string>()
        {
            {"il", "Illinois"},
            {"cl", "Colorado"},
            {"ny", "New York"}
        };
        public void AddState(string abbreviature, string name)
        {
            states[abbreviature.ToLower()] = name;
        }

        public string StateNameByAbbreviature(string abbreviature)
        {
            string toLowerAbbreviature = abbreviature.ToLower();
            if (states.ContainsKey(toLowerAbbreviature)) 
            {
                return states[toLowerAbbreviature];
            }
            return null;
           
        }

        
    }
}
