using Refactoring.FraudDetection.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Refactoring.FraudDetection.Abstractions.Domain;
using Refactoring.FraudDetection.Abstractions.Fraud;
using System.ComponentModel.DataAnnotations;
using Refactoring.FraudDetection.Infraestructure;

namespace Refactoring.FraudDetection.Domain.Orders
{
    public class Order : Entity
    {
        public Order(int orderId, int dealId, Email email, string creditCard, Address address) : base(orderId)
        {
            Requires.Required(orderId > 0, $"{nameof(Order)} must be greather than zero, value: {orderId}");
            Requires.Required(dealId > 0, $"OrderId must be greather than zero, value: {orderId}");
            Requires.NotNull(email, nameof(email));
            Requires.NotNullOrEmpty(creditCard, nameof(creditCard));
            Requires.NotNull(address, nameof(address));

            this.DealId = dealId;
            this.CreditCard = creditCard;
            this.Address = address;
            this.Email = email;
        }
        public int DealId { get; private set; }
        public Email Email { get; private set; }
        public string CreditCard { get; private set; }
        public Address Address { get; private set; }

        public bool IsFraudulent { get; private set; } = false;

        /// <summary>
        /// Here a little extension allowing you to add fraud detection rules dynamically, 
        /// becouse Fraud radar is a new component, we let it 
        /// ready to easily add extensions if required.
        /// As its the first version we could live with out it.
        /// The first time we need to change the fraudulent conditions, we could 
        /// implement a simple Rule engine.
        /// Becouse this is a toy, by now i would let it like it is.
        /// </summary>
        /// <param name="other"></param>
        /// <param name="fraudulentRules"></param>
        /// <returns></returns>
        public bool CheckIsFraudulent(Order other, IEnumerable<IFraudRule> fraudulentRules)
        {
            Requires.NotNull(other, nameof(other));
            Requires.NotNull(fraudulentRules, nameof(fraudulentRules));
            bool isFraudulent = false;
            foreach (IFraudRule rule in fraudulentRules)
            {
                isFraudulent = rule.IsFraudulent(this, other);
                if (isFraudulent) 
                {
                    this.IsFraudulent = true;
                    return true;
                }
                   
            }
            // Mark the order as fraudulent.
           
            return false;
        }
     
    }
}
