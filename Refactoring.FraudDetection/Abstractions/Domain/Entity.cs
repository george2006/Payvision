using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring.FraudDetection.Abstractions.Domain
{
    public abstract class Entity
    {
        public int Id { get; private set; }
        public Entity(int id) 
        {
            this.Id = id;
        }
    }
}
