using System;
using System.Collections.Generic;
using Teacup.Genetic;
using NUnit.Framework;

namespace Teacup.Genetic.UnitTesting
{
    [TestFixture]
	public class TestGene
	{
        [Test]
        public void InitGetSet()
        {
            // Data should be affected via constructor
            decimal decimal_value = 1m;
            int int_value = 50000;

            Gene<decimal> decimal_gene = new Gene<decimal>(decimal_value);
            Gene<int> int_gene = new Gene<int>(int_value);

            Assert.AreEqual(decimal_gene.GetData(), decimal_value);
            Assert.AreEqual(int_gene.GetData(), int_value);

            // Data should be affected via setter
            decimal another_decimal_value = -5m;
            int another_int_value = 10;

            decimal_gene.SetData(another_decimal_value);
            int_gene.SetData(another_int_value);

            Assert.AreEqual(decimal_gene.GetData(), another_decimal_value);
            Assert.AreEqual(int_gene.GetData(), another_int_value);

            Assert.AreNotEqual(decimal_gene.GetData(), decimal_value);
            Assert.AreNotEqual(int_gene.GetData(), int_value);
        }
	}
}
