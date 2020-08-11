using Refactoring.FraudDetection.Abstractions.Domain;
using Refactoring.FraudDetection.Domain;
using Refactoring.FraudDetection.Infraestructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Refactoring.FraudDetection.Domain.Orders
{
    public class Email : ValueObject<Email>
    {
        private string email;
        public Email(string email)
        {
            ValidEmailOrThrow(email);
            this.email = SanitizeEmail(email);
        }

        private string SanitizeEmail(string email)
        {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            aux[0] = aux[0]
                .Replace(".", "")
                .Replace("+", "");

           return string
                .Join("@", new string[] { aux[0], aux[1] })
                .ToLower();
        }

        private void ValidEmailOrThrow(string email)
        {
            Requires.NotNullOrEmpty(email, nameof(email));
            Requires.EmailIsValid(email);
        }

        protected override bool EqualsCore(Email other)
        {
            if (other == null)
                return false;
            return this.email == other.email;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = email.GetHashCode();
                return hashCode;
            }
        }

       
        
    }
}
