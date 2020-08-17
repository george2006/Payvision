﻿using Refactoring.FraudDetection.Abstractions.Fraud;
using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring.FraudDetection.Domain.Fraud
{
    public interface IFraudRuleRepository
    {
        IEnumerable<IFraudRule> Rules();
    }
}