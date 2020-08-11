using Refactoring.FraudDetection.Abstractions.Domain;
using Refactoring.FraudDetection.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring.FraudDetection.Domain.Orders
{
    public class Street : ValueObject<Street>
    {
        private string street;
        public Street(string street) 
        {
            this.street = street.ToLower();
        }
        protected override bool EqualsCore(Street other)
        {
            if (other == null)
                return false;

            return this.street == other.street;
        }

        protected override int GetHashCodeCore()
        {
            throw new NotImplementedException();
        }
    }
}
