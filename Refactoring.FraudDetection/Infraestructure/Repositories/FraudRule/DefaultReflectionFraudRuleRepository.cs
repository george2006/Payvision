using Refactoring.FraudDetection.Abstractions.Fraud;
using Refactoring.FraudDetection.Fraud;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using Refactoring.FraudDetection.Infraestructure.Extensions;
namespace Refactoring.FraudDetection.Infraestructure.Repositories.FraudRule
{
    public class DefaultReflectionFraudRuleRepository : IFraudRuleRepository
    {
        public IEnumerable<IFraudRule> Rules()
        {
            Type fraudRuleType = typeof(IFraudRule);
            Type[] types = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsAssignableFrom(fraudRuleType))
                .Where(t => t.IsClass)
                .Where(t => t.HasDefaultConstructor())
                .ToArray();

            return types.Select(t => (IFraudRule)Activator.CreateInstance(t));
        }

    }
}
