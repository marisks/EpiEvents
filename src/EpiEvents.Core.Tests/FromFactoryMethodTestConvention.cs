using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Fixie;

namespace EpiEvents.Core.Tests
{
    public class FromFactoryMethodTestConvention : Convention
    {
        public FromFactoryMethodTestConvention()
        {
            Classes
                .NameEndsWith("Tests");

            Methods
                .Where(method => method.IsVoid());

            Parameters
                .Add<FromFactoryMethod>();
        }

        public class FromFactoryMethod : ParameterSource
        {
            public IEnumerable<object[]> GetParameters(MethodInfo method)
            {
                var attribute = method.GetCustomAttributes<FactoryMethodDataAttribute>(true).FirstOrDefault();
                if (attribute != null)
                {
                    return GetParameters(attribute);
                }
                return Enumerable.Empty<object[]>();
            }

            private IEnumerable<object[]> GetParameters(FactoryMethodDataAttribute attribute)
            {
                return
                    (IEnumerable<object[]>)
                        attribute.Type.GetMethod(attribute.MethodName, BindingFlags.Public | BindingFlags.Static)
                            .Invoke(null, null);
            }
        }
    }
}