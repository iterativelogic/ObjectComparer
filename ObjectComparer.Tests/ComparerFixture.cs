using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ObjectComparer.Tests
{
    public class Person
    {
        public List<string> Childrens { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasIdentity { get; set; }
        public Address Address { get; set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class Address
    {
        public int FlatNo { get; set; }
        public string ApartmentName { get; set; }
        public bool IsOccupied { get; set; }
        public Person BelongsTo { get; set; }
    }

    public class PersonIdentity
    {
        public int Id { get; set; }
        public string SocialSecurity { get; set; }
    }

    [TestClass]
    public class ComparerFixture
    {
        [TestMethod]
        public void CompareValueTypePropery_ForSameObjects()
        {
            Assert.IsTrue(Comparer.AreSimilar(1234, 1234));
        }

        [TestMethod]
        public void CompareValueTypeProperty_ForNotEqualObject()
        {
            Assert.IsFalse(Comparer.AreSimilar(1234, 2345));
        }

        [TestMethod]
        public void CompareSimpleReferenceTypeObject()
        {
            Assert.IsTrue(Comparer.AreSimilar("Pranav", "Pranav"));
        }

        [TestMethod]
        public void CompareSimpleValueTypeListThatAreEqual_Unordered()
        {
            var arr1 = new List<int> { 2, 5, 8 };
            var arr2 = new List<int> { 5, 8, 2 };

            Assert.IsTrue(Comparer.AreSimilar(arr1, arr2));
        }

        [TestMethod]
        public void CompareSimpleRefTypeListThatAreEqual_Unordered()
        {
            var arr1 = new List<string> {"Pune", "Delhi", "Mumbai" };
            var arr2 = new List<string> { "Delhi", "Mumbai", "Pune" };

            Assert.IsTrue(Comparer.AreSimilar(arr1, arr2));
        }

        [TestMethod]
        public void CompareSimpleValueTypeArrayThatAreEqual_Unordered()
        {
            var arr1 = new int[] { 2, 5, 8 };
            var arr2 = new int[] { 5, 8, 2 };

            Assert.IsTrue(Comparer.AreSimilar(arr1, arr2));
        }

        [TestMethod]
        public void CompareSimpleRefTypeArrayThatAreEqual_Unordered()
        {
            var arr1 = new string[] { "Pune", "Delhi", "Mumbai" };
            var arr2 = new string[] { "Delhi", "Mumbai", "Pune" };

            Assert.IsTrue(Comparer.AreSimilar(arr1, arr2));
        }

        [TestMethod]
        public void CompareCustomObject_SimilarCollection()
        {
            var personIdentity1 = new PersonIdentity
            {
                Id = 1,
                SocialSecurity = "12345"
            };

            var personIdentity2 = new PersonIdentity
            {
                Id = 2,
                SocialSecurity = "12345"
            };


            var col1 = new List<PersonIdentity> { personIdentity1, personIdentity2 };
            var col2 = new List<PersonIdentity> { personIdentity2, personIdentity1 };

            Assert.IsTrue(Comparer.AreSimilar(col1, col2));
        }

        [TestMethod]
        public void CompareCustomObject_UnEqualCollection()
        {
            var personIdentity1 = new PersonIdentity
            {
                Id = 1,
                SocialSecurity = "12345"
            };

            var personIdentity2 = new PersonIdentity
            {
                Id = 2,
                SocialSecurity = "12345"
            };

            var personIdentity3 = new PersonIdentity
            {
                Id = 1,
                SocialSecurity = "12345"
            };

            var personIdentity4 = new PersonIdentity
            {
                Id = 2,
                SocialSecurity = "12345"
            };
            
            var col1 = new List<PersonIdentity> { personIdentity1, personIdentity2, personIdentity3 };
            var col2 = new List<PersonIdentity> { personIdentity2, personIdentity1, personIdentity4 };

            //var col1 = new List<PersonIdentity> {
            //    personIdentity1,
            //    new PersonIdentity
            //    {
            //        Id = 1,
            //        SocialSecurity = "1234"
            //    }
            //};

            //var col2 = new List<PersonIdentity> {                
            //    new PersonIdentity
            //    {
            //        Id = 1,
            //        SocialSecurity = "1234"
            //    },
            //    personIdentity3
            //};

            Assert.IsFalse(Comparer.AreSimilar(col1, col2));
        }

        [TestMethod]
        public void CompareComplexReferenceTypeObjects()
        {
            var circuit = new Person
            {
                Childrens = new List<string> { "Short", "Circuit" },
                Id = 1,
                Name = "Circuit",
                HasIdentity = true,
                Address = new Address
                {
                    ApartmentName = "Shanti Kunj",
                    FlatNo = 123,
                    IsOccupied = true
                }
            };

            var munna = new Person
            {
                Childrens = new List<string> { "Circuit", "Short" },
                Id = 1,
                Name = "Circuit",
                HasIdentity = true,
                Address = new Address
                {
                    ApartmentName = "Shanti Kunj",
                    FlatNo = 123,
                    IsOccupied = true
                }
            };

            circuit.Address.BelongsTo = munna;
            munna.Address.BelongsTo = munna;

            Assert.IsTrue(Comparer.AreSimilar(circuit, munna));
        }

        [TestMethod]
        public void CompareSameObjects()
        {
            var circuit = new Person
            {
                Childrens = new List<string> { "Short", "Circuit" },
                Id = 1,
                Name = "Circuit",
                HasIdentity = true,
                Address = new Address
                {
                    ApartmentName = "Shanti Kunj",
                    FlatNo = 123,
                    IsOccupied = true
                }
            };

            var munna = new Person
            {
                Childrens = new List<string> { "Circuit", "Short" },
                Id = 1,
                Name = "Circuit",
                HasIdentity = true,
                Address = new Address
                {
                    ApartmentName = "Shanti Kunj",
                    FlatNo = 123,
                    IsOccupied = true
                }
            };

            var rustam1 = new Person
            {
                Id = 123,
                Name = "Rustam",
                HasIdentity = true
            };

            var rustam2 = new Person
            {
                Id = 123,
                Name = "Rustam",
                HasIdentity = true
            };

            circuit.Address.BelongsTo = rustam1;
            munna.Address.BelongsTo = rustam2;

            Assert.IsTrue(Comparer.AreSimilar(circuit, munna));
        }

        [TestMethod]
        public void CompareComplexReferenceObjectCollection()
        {
            var personCollection1 = new List<Person>
            {
                new Person
                {
                    Id = 1,
                    Name = "Pranav"
                },
                new Person
                {
                    Id = 1,
                    Name = "Pranav"
                }
            };

            var personCollection2 = new List<Person>
            {
                new Person
                {
                    Id = 1,
                    Name = "Pranav"
                },
                new Person
                {
                    Id = 2,
                    Name = "Pranav"
                }
            };

            Assert.IsTrue(Comparer.AreSimilar(personCollection1, personCollection2));
        }
    }
}
