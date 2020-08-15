// <copyright file="FraudRadarTests.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactoring.FraudDetection.Abstractions.Fraud;
using Refactoring.FraudDetection.Abstractions.OrderSource;
using Refactoring.FraudDetection.Domain.Orders;
using Refactoring.FraudDetection.Domain.States;
using Refactoring.FraudDetection.Fraud;
using Refactoring.FraudDetection.Fraud.Infraestructure.OrderSource;
using Refactoring.FraudDetection.Fraud.PredefinedFraudRules;
using Refactoring.FraudDetection.Infraestructure.Repositories.States;
using Refactoring.FraudDetection.Infraestructure.Factories.Orders;

namespace Refactoring.FraudDetection.Tests
{
    [TestClass]
    public class FraudRadarTests
    {
        [TestMethod]
        [DeploymentItem("./Files/OneLineFile.txt", "Files")]
        public void CheckFraud_OneLineFile_NoFraudExpected()
        {
            var result = ExecuteTest(Path.Combine(Environment.CurrentDirectory, "Files", "OneLineFile.txt"));

            result.Should().NotBeNull("The result should not be null.");
            result.Should().HaveCount(0, "The result should not contains fraudulent lines");
        }

        [TestMethod]
        [DeploymentItem("./Files/TwoLines_FraudulentSecond.txt", "Files")]
        public void CheckFraud_TwoLines_SecondLineFraudulent()
        {
            var result = ExecuteTest(Path.Combine(Environment.CurrentDirectory, "Files", "TwoLines_FraudulentSecond.txt"));

            result.Should().NotBeNull("The result should not be null.");
            result.Should().HaveCount(1, "The result should contains the number of lines of the file");
            result.First().IsFraudulent.Should().BeTrue("The first line is not fraudulent");
            result.First().OrderId.Should().Be(2, "The first line is not fraudulent");
        }

        [TestMethod]
        [DeploymentItem("./Files/ThreeLines_FraudulentSecond.txt", "Files")]
        public void CheckFraud_ThreeLines_SecondLineFraudulent()
        {
            var result = ExecuteTest(Path.Combine(Environment.CurrentDirectory, "Files", "ThreeLines_FraudulentSecond.txt"));

            result.Should().NotBeNull("The result should not be null.");
            result.Should().HaveCount(1, "The result should contains the number of lines of the file");
            result.First().IsFraudulent.Should().BeTrue("The first line is not fraudulent");
            result.First().OrderId.Should().Be(2, "The first line is not fraudulent");
        }

        [TestMethod]
        [DeploymentItem("./Files/FourLines_MoreThanOneFraudulent.txt", "Files")]
        public void CheckFraud_FourLines_MoreThanOneFraudulent()
        {
            var result = ExecuteTest(Path.Combine(Environment.CurrentDirectory, "Files", "FourLines_MoreThanOneFraudulent.txt"));

            result.Should().NotBeNull("The result should not be null.");
            result.Should().HaveCount(2, "The result should contains the number of lines of the file");
        }

        private static List<FraudResult> ExecuteTest(string filePath)
        {
            IEnumerable<IFraudRule> fraudRules = FraudRules();
            IFraudRadar fraudRadar = new FraudRadar(fraudRules);
            IStateRepository stateRepository = new MemoryStateRepository();
            IOrderFactory orderFactory = new StringArrayOrderFactory(stateRepository);
            IOrderSource orderSource = new FileOrderSource(filePath, orderFactory);
            return fraudRadar.Check(orderSource).ToList();
        }
        private static IEnumerable<IFraudRule> FraudRules() 
        {
            yield return new SameDealAndEmailButDifferentCreditCardFraudRule();
            yield return new SameDealAndAddressButDifferentCreditCardFraudRule();
        }

      
    }
}