﻿using Refactoring.FraudDetection.Abstractions.Fraud;
using Refactoring.FraudDetection.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring.FraudDetection.Fraud.PredefinedFraudRules
{
    public class SameDealAndEmailButDifferentCreditCardFraudRule : IFraudRule
    {
        public bool IsFraudulent(Order thisOne, Order other)
        {
            if (thisOne.DealId == other.DealId &&
                thisOne.Email == other.Email &&
                thisOne.CreditCard != other.CreditCard)
            {
                return true;
            }
            return false;
        }
    }
}
