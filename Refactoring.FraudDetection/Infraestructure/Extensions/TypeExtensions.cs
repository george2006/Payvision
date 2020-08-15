using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Refactoring.FraudDetection.Infraestructure.Extensions
{
    public static class TypeExtensions
    {
        public static bool HasDefaultConstructor(this Type type) 
        {
            return (type.GetConstructor(Type.EmptyTypes) != null);
        }
    }
}
