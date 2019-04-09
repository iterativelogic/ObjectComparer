using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ObjectComparer
{
    public class RefTypeState : PropertyState
    {
        public RefTypeState() : this(new HashSet<Type>()) { }

        public RefTypeState(HashSet<Type> previousReferences)
        {
            PreviousReferences = previousReferences;
        }

        HashSet<Type> PreviousReferences { get; }
        List<PropertyState> States { get; set; } = new List<PropertyState>();

        public override bool AreEqual()
        {
            var objectType = FirstObject.GetType();
            
            var equalsMethod = GetEqualsMethod(objectType);

            if (equalsMethod != null)
            {
                return (bool)equalsMethod.Invoke(FirstObject, new object[] { SecondObject });
            }

            var objectProperties = objectType.GetProperties();
            var enumerableType = typeof(IEnumerable<object>);

            foreach (var property in objectProperties)
            {
                var propertyType = property.PropertyType;

                var firstValue = property.GetValue(FirstObject);
                var secondValue = property.GetValue(SecondObject);

                if (firstValue == null || secondValue == null)
                {
                    States.Add(new NullObjectState
                    {
                        FirstObject = firstValue,
                        SecondObject = secondValue
                    });

                    continue;
                }

                if (IsPreviousReferenceProperty(propertyType))
                {
                    States.Add(new PreviousObjectState
                    {
                        FirstObject = firstValue,
                        SecondObject = secondValue
                    });

                    continue;
                }

                if (propertyType.IsValueType)
                {
                    States.Add(new ValueTypeState
                    {
                        FirstObject = firstValue,
                        SecondObject = secondValue
                    });

                    continue;
                }

                if (enumerableType.IsAssignableFrom(propertyType))
                {
                    States.Add(new CollectionState
                    {
                        FirstObject = firstValue,
                        SecondObject = secondValue
                    });

                    continue;
                }

                if (propertyType.IsClass)
                {
                    PreviousReferences.Add(objectType);

                    States.Add(new RefTypeState(PreviousReferences)
                    {
                        FirstObject = firstValue,
                        SecondObject = secondValue
                    });

                    continue;
                }
            }

            return States.All(x => x.AreEqual());
        }

        private bool IsPreviousReferenceProperty(Type propertyType)
        {
            return PreviousReferences.Any(type => type == propertyType);
        }

        private static MethodInfo GetEqualsMethod(Type objectType)
        {
            return objectType.GetMethods()
                .Where(x => x.Name == "Equals")
                .SingleOrDefault(x =>
                {
                    var parameters = x.GetParameters();
                    return parameters.Count() == 1 && parameters.First().ParameterType == objectType;
                });
        }
    }
}
