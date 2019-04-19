using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ObjectComparer
{
    public class RefTypeStrategy : ComparisonStrategy
    {
        private readonly Type _enumerableType = typeof(IEnumerable<object>);

        public RefTypeStrategy() : this(new HashSet<Type>()) { }

        public RefTypeStrategy(HashSet<Type> previousReferences)
        {
            PreviousReferences = previousReferences;
        }

        HashSet<Type> PreviousReferences { get; }

        List<ComparisonStrategy> Strategies { get; set; } = new List<ComparisonStrategy>();

        public override bool AreEqual()
        {
            var objectType = FirstObject.GetType();
            
            var equalsMethod = GetEqualsMethod(objectType);

            if (equalsMethod != null)
            {
                return (bool)equalsMethod.Invoke(FirstObject, new object[] { SecondObject });
            }

            var objectProperties = objectType.GetProperties();
            
            foreach (var property in objectProperties)
            {
                var comparisonStrategy = ComparisonStrategyFactory.GetStrategy(FirstObject, SecondObject, property, PreviousReferences);
                Strategies.Add(comparisonStrategy);
                
                RecordPreviousReference(property.PropertyType, objectType);
            }

            return Strategies.All(x => x.AreEqual());
        }

        private void RecordPreviousReference(Type propertyType, Type objectType)
        {
            if (_enumerableType.IsAssignableFrom(propertyType))
            {
                return;
            }

            if (propertyType.IsClass)
            {
                PreviousReferences.Add(objectType);
            }            
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
