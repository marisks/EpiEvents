using System;

namespace EpiEvents.Core.Tests.Base
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class FactoryMethodDataAttribute : Attribute
    {
        public FactoryMethodDataAttribute(Type type, string methodName)
        {
            Type = type;
            MethodName = methodName;
        }

        public Type Type { get; }
        public string MethodName { get; }
    }
}