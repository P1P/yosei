using System;
using System.Collections.Generic;
using Teacup.Genetic;
using NUnit.Framework;

namespace Teacup.Genetic.UnitTesting
{
    [TestFixture]
    public class TestChromosome
    {
        [Test]
        public void InitGetSet()
        {
            // Creating a chromosome gene by gene
            Chromosome<int> chr_1 = new Chromosome<int>("MyChromosome");

            Assert.AreEqual(chr_1.GetGenesCount(), 0);

            int nb_genes = 10;

            for (int i = 0; i < nb_genes; ++i)
            {
                Gene<int> gene = new Gene<int>(i * 10);
                chr_1.AddGene(gene);
                Assert.AreEqual(chr_1.GetGene(i), gene);
            }

            Assert.AreEqual(chr_1.GetGenesCount(), nb_genes);

            // Creating a chromosome directly through data
            decimal[] data_array = new decimal[] { decimal.MinValue, decimal.MinusOne, decimal.Zero, decimal.One, decimal.MaxValue };

            Chromosome<decimal> chr_2 = new Chromosome<decimal>("MySecondChromosome", data_array);

            for (int i = 0; i < data_array.Length; ++i)
            {
                Assert.AreEqual(chr_2.GetGene(i).GetData(), data_array[i]);
            }
        }

        [Test]
        public void Crossover()
        {
            Chromosome<int> chr_base_1 = new Chromosome<int>("Chromosome1", new int[] { 1, 2, 3, 4, 5 });
            Chromosome<int> chr_base_2 = new Chromosome<int>("Chromosome2", new int[] { -1, -2, -3, -4, -5 });
            
            // Junction at end should mean no crossover or full swap
            Chromosome<int> chr_1 = new Chromosome<int>(chr_base_1);
            Chromosome<int> chr_2 = new Chromosome<int>(chr_base_2);

            Chromosome<int>.CrossOver(chr_1, chr_2, 0, 0);

            Assert.AreEqual(chr_1.GetGenesCopyArray(), chr_base_1.GetGenesCopyArray(), chr_1.ToString() + " vs " + chr_base_1.ToString());
            Assert.AreEqual(chr_2.GetGenesCopyArray(), chr_base_2.GetGenesCopyArray(), chr_2.ToString() + " vs " + chr_base_2.ToString());
            
            // Junction at beginning should mean full swap or no crossover
            chr_1 = new Chromosome<int>(chr_base_1);
            chr_2 = new Chromosome<int>(chr_base_2);

            Chromosome<int>.CrossOver(chr_1, chr_2, chr_1.GetGenesCount(), 0);

            Assert.AreEqual(chr_1.GetGenesCopyArray(), chr_base_2.GetGenesCopyArray());
            Assert.AreEqual(chr_2.GetGenesCopyArray(), chr_base_1.GetGenesCopyArray());
            Assert.AreNotEqual(chr_1.GetGenesCopyArray(), chr_base_1.GetGenesCopyArray());
            Assert.AreNotEqual(chr_2.GetGenesCopyArray(), chr_base_2.GetGenesCopyArray());

            // Testing more general cases
            chr_1 = new Chromosome<int>(chr_base_1);
            chr_2 = new Chromosome<int>(chr_base_2);

            Chromosome<int>.CrossOver(chr_1, chr_2, 1, 1);

            Assert.AreEqual(chr_1.GetGenesDataCopyArray(), new int[] { 1, -2, -3, -4, -5 }, chr_1.ToString());
            Assert.AreEqual(chr_2.GetGenesDataCopyArray(), new int[] { -1, 2, 3, 4, 5 }, chr_2.ToString());
            Assert.AreNotEqual(chr_1.GetGenesCopyArray(), chr_base_1.GetGenesCopyArray());
            Assert.AreNotEqual(chr_2.GetGenesCopyArray(), chr_base_2.GetGenesCopyArray());


            chr_1 = new Chromosome<int>(chr_base_1);
            chr_2 = new Chromosome<int>(chr_base_2);

            Chromosome<int>.CrossOver(chr_1, chr_2, 3, 1);

            Assert.AreEqual(chr_1.GetGenesDataCopyArray(), new int[] { 1, 2, 3, -4, -5 });
            Assert.AreEqual(chr_2.GetGenesDataCopyArray(), new int[] { -1, -2, -3, 4, 5 });
            Assert.AreNotEqual(chr_1.GetGenesCopyArray(), chr_base_1.GetGenesCopyArray());
            Assert.AreNotEqual(chr_2.GetGenesCopyArray(), chr_base_2.GetGenesCopyArray());


            chr_1 = new Chromosome<int>(chr_base_1);
            chr_2 = new Chromosome<int>(chr_base_2);

            Chromosome<int>.CrossOver(chr_1, chr_2, 4, 1);

            Assert.AreEqual(chr_1.GetGenesDataCopyArray(), new int[] { 1, 2, 3, 4, -5 });
            Assert.AreEqual(chr_2.GetGenesDataCopyArray(), new int[] { -1, -2, -3, -4, 5 });
            Assert.AreNotEqual(chr_1.GetGenesCopyArray(), chr_base_1.GetGenesCopyArray());
            Assert.AreNotEqual(chr_2.GetGenesCopyArray(), chr_base_2.GetGenesCopyArray());
        }

        [Test]
        public void Mutation()
        {
            Chromosome<decimal> chr_1 = new Chromosome<decimal>("MyChromosome");

            for (int i = 0; i < 10; ++i)
            {
                chr_1.AddGene(new Gene<decimal>(1m));
                Assert.AreEqual(chr_1.GetGene(i).GetData(), 1m);
            }

            chr_1.Mutate(Chromosome<decimal>.MUTATION_TYPE.FULL, 0.1m, 2m, 0);

            Assert.AreEqual(chr_1.GetGenesCount(), 10);
            Assert.AreNotEqual(chr_1.GetGene(0).GetData(), 1m);
        }
    }
}
