using Refactoring.FraudDetection.Domain.States;
using Refactoring.FraudDetection.Infraestructure;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Refactoring.FraudDetection.Domain.Orders
{
    public class StringArrayOrderFactory : IOrderFactory 
    { 
        private readonly IStateRepository stateRepository;
        public StringArrayOrderFactory(IStateRepository stateRepository) 
        {
            Requires.NotNull(stateRepository, nameof(stateRepository));
            this.stateRepository = stateRepository;
        }

        public Order Create(string[] orderFields)
        {
            ThrowIfNotValidInput(orderFields);
            return MapOrder(orderFields);
        }

        private void ThrowIfNotValidInput(string[] orderFields)
        {
            Requires.NotNull(orderFields, nameof(orderFields));
            Requires.Required(orderFields.Length == 8,
                $"Each order has 8 fields current order has {orderFields.Length}");

            string stringOrderId = orderFields[OrderFieldsPosition.OrderId];
            string orderErrorMessage = $"OrderId must be a number and its value is {stringOrderId} ";
            Requires.Required(int.TryParse(stringOrderId, out int orderId), orderErrorMessage);

            string stringDealId = orderFields[OrderFieldsPosition.DealId];
            string dealIdErrorMessage = $"DealId must be a number and its value is {stringDealId} ";
            Requires.Required(int.TryParse(stringDealId, out int dealId), dealIdErrorMessage);
        }

        private Order MapOrder(string[] orderFields)
        {
            int orderId = int.Parse(orderFields[OrderFieldsPosition.OrderId]);
            int dealId = int.Parse(orderFields[OrderFieldsPosition.DealId]);
            var stringEmail = orderFields[OrderFieldsPosition.Email];
            var street = orderFields[OrderFieldsPosition.Street];
            var city = orderFields[OrderFieldsPosition.City];
            var zipCode = orderFields[OrderFieldsPosition.ZipCode];
            var creditCard = orderFields[OrderFieldsPosition.CreditCard];

            // We need to find state in a state repository by its abbreviature.
            State state = new State(
                stateRepository.StateNameByAbbreviature(orderFields[OrderFieldsPosition.State]));

            Address address = new Address(street, city, state, zipCode);
            Email email = new Email(stringEmail);
            return new Order(orderId, dealId, email, creditCard, address);
        }

        internal static class OrderFieldsPosition 
        {
            public static int OrderId = 0;
            public static int DealId = 1;
            public static int Email = 2;
            public static int Street = 3;
            public static int City = 4;
            public static int State = 5;
            public static int ZipCode = 6;
            public static int CreditCard = 7;
        }
    }
}
