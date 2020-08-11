using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring.FraudDetection.Abstractions.Fraud
{
    public class FraudResult
    {
        protected FraudResult(int orderId, bool isFraudulent)
        {
            OrderId = orderId;
            IsFraudulent = isFraudulent;
        }

        public int OrderId { get; private set; }
        public bool IsFraudulent { get; private set; }
        public static FraudResult Fraudulent(int orderId) 
        {
            return new FraudResult(orderId, true);
        }
    }
}
