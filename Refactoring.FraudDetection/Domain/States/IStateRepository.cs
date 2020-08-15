using Refactoring.FraudDetection.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring.FraudDetection.Domain.States
{
    public interface IStateRepository
    {
        void Add(State state);
        State StateByAbbreviature(string abbreviature);
    }
}
