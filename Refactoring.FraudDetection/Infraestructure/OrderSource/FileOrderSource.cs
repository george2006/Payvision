using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Refactoring.FraudDetection.Abstractions.OrderSource;
using Refactoring.FraudDetection.Domain.Orders;
using Refactoring.FraudDetection.Infraestructure;

namespace Refactoring.FraudDetection.Fraud.Infraestructure.OrderSource
{
    public class FileOrderSource : IOrderSource
    {
        private readonly string filePath;
        private readonly IOrderFactory orderFactory;
        public FileOrderSource(string filePath, IOrderFactory orderFactory) 
        {
            Requires.NotNullOrEmpty(filePath, nameof(filePath));
            Requires.Required(File.Exists(filePath), $"File {filePath} doesnt exists");
            this.filePath = filePath;
            this.orderFactory = orderFactory;
        }
        public IList<Order> Load()
        {
            string [] fileLines = File.ReadAllLines(this.filePath);
            IList<Order> orders = new List<Order>();
            foreach (var fileLine in fileLines) 
            {
                string[] orderFields = fileLine.Split(',', StringSplitOptions.RemoveEmptyEntries);
                Order order = orderFactory.Create(orderFields);
                orders.Add(order);
            }
            return orders;
        }
    }
}
