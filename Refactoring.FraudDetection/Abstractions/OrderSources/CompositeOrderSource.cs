using Refactoring.FraudDetection.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using Refactoring.FraudDetection.Infraestructure;
using System.Linq;

namespace Refactoring.FraudDetection.Abstractions.OrderSource
{
	public sealed class CompositeOrderSource : IOrderSource
	{
		private readonly List<IOrderSource> orderSources;
		public CompositeOrderSource(IOrderSource[] sources)
		{
			this.orderSources = new List<IOrderSource>();
			Requires.NotNull(sources, nameof(sources));
			Requires.Required(sources.Count() > 0, $"{nameof(sources)} is empty");
			this.orderSources.AddRange(sources);
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