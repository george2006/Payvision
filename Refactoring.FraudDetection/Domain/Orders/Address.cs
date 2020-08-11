using Refactoring.FraudDetection.Abstractions.Domain;
using Refactoring.FraudDetection.Domain;
using Refactoring.FraudDetection.Domain.States;
using Refactoring.FraudDetection.Infraestructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring.FraudDetection.Domain.Orders
{
    public class Address : ValueObject<Address>
    {
        public Address(string street, string city, State state,
                       string zipCode)
        {
            Requires.NotNullOrEmpty(street, nameof(street));
            Requires.NotNullOrEmpty(city,nameof(city));
            Requires.NotNull(state, nameof(state));
            Requires.NotNullOrEmpty(zipCode, nameof(zipCode));
            Street = street.ToLower();
            City = city.ToLower();
            State = state;
            ZipCode = zipCode.ToLower();
        }

        public string Street { get; private set; }

        public string City { get; private set; }

        public State State { get; private set; }

        public string ZipCode { get; private set; }

        protected override bool EqualsCore(Address other)
        {
            if (other == null)
                return false;

            return (this.Street == other.Street) &&
                (this.City == other.City) &&
                (this.State == other.State) &&
                (this.ZipCode == other.ZipCode);

        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = Street.GetHashCode();
                hashCode = (hashCode * 397) ^ Street.GetHashCode();
                hashCode = (hashCode * 397) ^ City.GetHashCode();
                hashCode = (hashCode * 397) ^ ZipCode.GetHashCode();
                hashCode = (hashCode * 397) ^ State.GetHashCode();
                return hashCode;
            }
        }
    }
}
