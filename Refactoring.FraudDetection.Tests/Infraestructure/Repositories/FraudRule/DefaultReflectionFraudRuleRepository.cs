using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactoring.FraudDetection.Infraestructure.Repositories.FraudRule;
using System.Collections.Generic;
using System.Linq;

namespace Refactoring.FraudDetection.Tests.Infraestructure.Repositories.FraudRule 
{
	[TestClass]
	public class DefaultReflectionFraudRuleRepositoryTests 
	{
		[TestMethod()]
		public void WhenLoadIsCalledSomeFraudRulesAreFound() 
		{
			DefaultReflectionFraudRuleRepository sut = CreateSut();
			IEnumerable<Abstractions.Fraud.IFraudRule> rules = sut.Rules();
			Assert.IsNotNull(rules);
			// This assert will fail if we decide to move our fraudrules implementations
			// to another assembly so its in good place.
			Assert.IsTrue(rules.Count() > 0);
		}
		private DefaultReflectionFraudRuleRepository CreateSut() 
		{
			return new DefaultReflectionFraudRuleRepository();
		}
	}
}