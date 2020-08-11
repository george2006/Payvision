using Refactoring.FraudDetection.Abstractions.OrderSource;
using Refactoring.FraudDetection.Fraud;
using System.Collections.Generic;

namespace Refactoring.FraudDetection.Abstractions.Fraud
{
    public interface IFraudRadar
    {
        IEnumerable<FraudResult> Check(IOrderSource orderSource);
    }
}