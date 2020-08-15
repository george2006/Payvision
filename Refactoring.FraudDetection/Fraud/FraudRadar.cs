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
                while(j < orders.Count)
                {
                    // if order is just marked as fraudulent, we dont compare it again
                    if (orders[j].IsFraudulent)
                        continue;

                    if (orders[j].CheckIsFraudulent(currentOrder, fraudRules))
                    {
                        fraudResults.Add(FraudResult.Fraudulent(orders[j].Id));
                    }
                    j += 1;
                }
            }
            return fraudResults;
        }
    }
}
