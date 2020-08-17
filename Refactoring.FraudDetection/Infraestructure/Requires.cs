using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Runtime.Serialization;
using System.Text;

namespace Refactoring.FraudDetection.Infraestructure
{
    public static class Requires
    {
        public static void NotNull(object obj, string parameterName) 
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(parameterName));
        }
        public static void NotNullOrEmpty(string value, string parameterName) 
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(parameterName));
        }

        public static void EmailIsValid(string email)
        {
            // This could be done writting a regular expression.
            try
            {
                MailAddress _ = new MailAddress(email);
            }
            catch (FormatException)
            {
                throw new InvalidOperationException($"Email with value {email} is not valid");
            }
           
        }

        public static void Required(bool condition, string message)
        {
            if (!condition)
                throw new InvalidOperationException(message);
        }
    }
}
