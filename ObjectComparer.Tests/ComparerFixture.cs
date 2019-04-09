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
    }

    public class Address
    {
        public int FlatNo { get; set; }
        public string ApartmentName { get; set; }
        public bool IsOccupied { get; set; }
        public Person BelongsTo { get; set; }
    }

    [TestClass]
    public class ComparerFixture
    {
        [TestMethod]
        public void CompareValueTypePropery()
        {
            Assert.IsTrue(Comparer.AreSimilar(1234, 1234));
        }

        [TestMethod]
        public void CompareSimpleReferenceTypeObject()
        {
            Assert.IsTrue(Comparer.AreSimilar("Pranav", "Pranav"));
        }

        [TestMethod]
        public void ComparePrimitiveTypeObjects()
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

       
    }
}
