using Refactoring.FraudDetection.Domain.States;
using Refactoring.FraudDetection.Infraestructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring.FraudDetection.Infraestructure.Repositories.States
{
    public sealed class MemoryStateRepository : IStateRepository
    {
        private readonly Dictionary<string, State> states = new Dictionary<string, State>()
        {
            {"il", new State("il", "Illinois")},
            {"cl", new State("cl", "Colorado")},
            {"ny", new State("ny", "New York")}
        };

        public void Add(State state)
        {
            Requires.NotNull(state, nameof(state));

            if (!states.ContainsKey(state.Abbreviature))
            {
                states.Add(state.Abbreviature, state);
            }

            states[state.Abbreviature] = state;
        }

        State IStateRepository.StateByAbbreviature(string abbreviature)
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
