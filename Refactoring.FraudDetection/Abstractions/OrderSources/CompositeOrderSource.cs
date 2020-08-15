using Refactoring.FraudDetection.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using Refactoring.FraudDetection.Infraestructure;
namespace Refactoring.FraudDetection.Abstractions.OrderSource
{
	public sealed class CompositeOrderSource : IOrderSource
	{
		private readonly IList<IOrderSource> orderSources;
		public CompositeOrderSource(IOrderSource[] sources)
		{
			this.orderSources = new List<IOrderSource>();
		}
		public void AddSource(IOrderSource orderSource) 
		{
			Requires.NotNull(orderSource, nameof(orderSource));
			this.orderSources.Add(orderSource);
		}
		public IList<Order> Load() 
		{
			List<Order> orders = new List<Order>();
			foreach (IOrderSource orderSource in this.orderSources) 
			{
				orders.AddRange(orderSource.Load());
			}
			return orders;
		}
	}
}