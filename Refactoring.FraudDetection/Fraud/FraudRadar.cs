using Refactoring.FraudDetection.Abstractions.Fraud;
using Refactoring.FraudDetection.Abstractions.OrderSource;
using Refactoring.FraudDetection.Domain.Orders;
using Refactoring.FraudDetection.Fraud;
using Refactoring.FraudDetection.Infraestructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring.FraudDetection.Fraud
{
    public class FraudRadar : IFraudRadar
    {
        private readonly IEnumerable<IFraudRule> fraudRules;
        public FraudRadar(IEnumerable<IFraudRule> fraudRules)
        {
            Requires.NotNull(fraudRules, nameof(fraudRules));
            this.fraudRules = fraudRules;
        }
        public IEnumerable<FraudResult> Check(IOrderSource orderSource)
        {
            IList<FraudResult> fraudResults = new List<FraudResult>();
            IList<Order> orders = orderSource.Load();
            for (int i = 0; i < orders.Count; i++)
            {
                var currentOrder = orders[i];
                int j = (i + 1);
                bool isFraudulent = false;
                while(j < orders.Count && isFraudulent == false)
                {
                    // Please take a look at this method signature, you could add new rules.
                    // I Will model this better in a future if its needed building a rule engine.
                    // Using double disptach technique here to allow order changes its own state
                    // if needed
                    if (orders[j].CheckIsFraudulent(currentOrder, fraudRules))
                    {
                        fraudResults.Add(FraudResult.Fraudulent(orders[j].Id));
                        // IMPORTANT: improve perfomance when its fraudulent we dont need to go to the end.
                        isFraudulent = true;
                    }
                    j += 1;
                }
            }
            return fraudResults;
        }
    }
}
