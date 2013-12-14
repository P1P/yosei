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
            
            // Junction at end should mean no crossover
            Chromosome<int> chr_1 = new Chromosome<int>(chr_base_1);
            Chromosome<int> chr_2 = new Chromosome<int>(chr_base_2);

            Chromosome<int>.CrossOver(chr_1, chr_2, chr_1.GetGenesCount());

            Assert.AreEqual(chr_1.GetGenesCopyArray(), chr_base_1.GetGenesCopyArray(), chr_1.ToString() + " vs " + chr_base_1.ToString());
            Assert.AreEqual(chr_2.GetGenesCopyArray(), chr_base_2.GetGenesCopyArray(), chr_2.ToString() + " vs " + chr_base_2.ToString());
            
            // Junction at beginning should mean full swap
            chr_1 = new Chromosome<int>(chr_base_1);
            chr_2 = new Chromosome<int>(chr_base_2);

            Chromosome<int>.CrossOver(chr_1, chr_2, 0);

            Assert.AreEqual(chr_1.GetGenesCopyArray(), chr_base_2.GetGenesCopyArray());
            Assert.AreEqual(chr_2.GetGenesCopyArray(), chr_base_1.GetGenesCopyArray());
            Assert.AreNotEqual(chr_1.GetGenesCopyArray(), chr_base_1.GetGenesCopyArray());
            Assert.AreNotEqual(chr_2.GetGenesCopyArray(), chr_base_2.GetGenesCopyArray());

            // Testing more general cases
            chr_1 = new Chromosome<int>(chr_base_1);
            chr_2 = new Chromosome<int>(chr_base_2);

            Chromosome<int>.CrossOver(chr_1, chr_2, 1);

            Assert.AreEqual(chr_1.GetGenesDataCopyArray(), new int[] { 1, -2, -3, -4, -5 }, chr_1.ToString());
            Assert.AreEqual(chr_2.GetGenesDataCopyArray(), new int[] { -1, 2, 3, 4, 5 }, chr_2.ToString());
            Assert.AreNotEqual(chr_1.GetGenesCopyArray(), chr_base_1.GetGenesCopyArray());
            Assert.AreNotEqual(chr_2.GetGenesCopyArray(), chr_base_2.GetGenesCopyArray());


            chr_1 = new Chromosome<int>(chr_base_1);
            chr_2 = new Chromosome<int>(chr_base_2);

            Chromosome<int>.CrossOver(chr_1, chr_2, 3);

            Assert.AreEqual(chr_1.GetGenesDataCopyArray(), new int[] { 1, 2, 3, -4, -5 });
            Assert.AreEqual(chr_2.GetGenesDataCopyArray(), new int[] { -1, -2, -3, 4, 5 });
            Assert.AreNotEqual(chr_1.GetGenesCopyArray(), chr_base_1.GetGenesCopyArray());
            Assert.AreNotEqual(chr_2.GetGenesCopyArray(), chr_base_2.GetGenesCopyArray());


            chr_1 = new Chromosome<int>(chr_base_1);
            chr_2 = new Chromosome<int>(chr_base_2);

            Chromosome<int>.CrossOver(chr_1, chr_2, 4);

            Assert.AreEqual(chr_1.GetGenesDataCopyArray(), new int[] { 1, 2, 3, 4, -5 });
            Assert.AreEqual(chr_2.GetGenesDataCopyArray(), new int[] { -1, -2, -3, -4, 5 });
            Assert.AreNotEqual(chr_1.GetGenesCopyArray(), chr_base_1.GetGenesCopyArray());
            Assert.AreNotEqual(chr_2.GetGenesCopyArray(), chr_base_2.GetGenesCopyArray());
        }

        [Test]
        public void Mutation_decimal()
        {
            int random_seed = (int)(DateTime.Now.Ticks % int.MaxValue);
            int nb_mutations_per_gene = 10;
            int nb_genes = 100;

            // Retrieving RNG values
            Random random = new Random(random_seed);
            decimal[] rng = new decimal[nb_mutations_per_gene * nb_genes];

            for (int i = 0; i < rng.Length; ++i)
            {
                rng[i] = (decimal) random.NextDouble();
            }

            Chromosome<decimal> chr_1 = new Chromosome<decimal>("MyChromosome");

            for (int i = 0; i < nb_genes; ++i)
            {
                chr_1.AddGene(new Gene<decimal>(1m));
                Assert.AreEqual(chr_1.GetGene(i).GetData(), 1f);
            }

            // Confronting predicted RNG values with effective mutations
            // This test case relies on the Chromosome using its random for mutations only
            Chromosome<decimal>.SetRandomSeed(random_seed);
            for (int i = 0; i < nb_mutations_per_gene; ++i)
            {
                for (int j = 0; j < nb_genes; ++j)
                {
                    chr_1.Mutate(j); // RNG prediction would be off mark if we let the gene index be randomly decided
                    Assert.AreEqual(chr_1.GetGene(j).GetData(), rng[j + i * nb_genes]);
                }
            }
        }
        
        [Test]
        public void Mutation_int()
        {
            int random_seed = (int)(DateTime.Now.Ticks % int.MaxValue);
            int nb_mutations_per_gene = 10;
            int nb_genes = 100;

            // Retrieving RNG values
            Random random = new Random(random_seed);
            int[] rng = new int[nb_mutations_per_gene * nb_genes];

            for (int i = 0; i < rng.Length; ++i)
            {
                rng[i] = random.Next();
            }

            Chromosome<int> chr_1 = new Chromosome<int>("MyChromosome");

            for (int i = 0; i < nb_genes; ++i)
            {
                chr_1.AddGene(new Gene<int>(1));
                Assert.AreEqual(chr_1.GetGene(i).GetData(), 1f);
            }

            // Confronting predicted RNG values with effective mutations
            // This test case relies on the Chromosome using its random for mutations only
            Chromosome<int>.SetRandomSeed(random_seed);
            for (int i = 0; i < nb_mutations_per_gene; ++i)
            {
                for (int j = 0; j < nb_genes; ++j)
                {
                    chr_1.Mutate(j); // RNG prediction would be off mark if we let the gene index be randomly decided
                    Assert.AreEqual(chr_1.GetGene(j).GetData(), rng[j + i * nb_genes]);
                }
            }
        }
    }
}
