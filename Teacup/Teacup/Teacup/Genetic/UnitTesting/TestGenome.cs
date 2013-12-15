using System;
using System.Collections.Generic;
using Teacup.Genetic;
using NUnit.Framework;

namespace Teacup.Genetic.UnitTesting
{
    [TestFixture]
    public class TestGenome
    {
        [Test]
        public void InitGetSet()
        {
            Chromosome<int> chr_1 = new Chromosome<int>("MyFirstChromosome");
            Chromosome<int> chr_2 = new Chromosome<int>("MySecondChromosome");

            chr_1.AddGene(new Gene<int>(1));
            chr_1.AddGene(new Gene<int>(2));

            chr_2.AddGene(new Gene<int>(11));
            chr_2.AddGene(new Gene<int>(12));
            chr_2.AddGene(new Gene<int>(13));

            Genome<int> genome = new Genome<int>(new Chromosome<int>[] { chr_1, chr_2} );

            Assert.AreEqual(genome.GetChromosomeCount(), 2);
            Assert.AreEqual(genome.GetChromosome("MyFirstChromosome"), chr_1);
            Assert.AreEqual(genome.GetChromosome("MySecondChromosome").GetGenesCount(), 3);
            Assert.AreEqual(genome.GetChromosome("MySecondChromosome").GetGene(2).GetData(), 13);
        }

        [Test]
        public void Mate()
        {
            Chromosome<decimal> chr_1A = new Chromosome<decimal>("ChromosomeA", 1, 2, 3, 4);
            Chromosome<decimal> chr_1B = new Chromosome<decimal>("ChromosomeB", 11, 12, 13, 14, 15, 16);

            Chromosome<decimal> chr_2A = new Chromosome<decimal>("ChromosomeA", 2.1m, 2.1m, 3.1m, 4.1m);
            Chromosome<decimal> chr_2B = new Chromosome<decimal>("ChromosomeB", 11.1m, 12.1m, 13.1m, 14.1m, 15.1m, 16.1m);

            Genome<decimal> genome_1 = new Genome<decimal>(new Chromosome<decimal>[] { chr_1A, chr_1B });
            Genome<decimal> genome_2 = new Genome<decimal>(new Chromosome<decimal>[] { chr_2A, chr_2B });

            Genome<decimal> offspring_1 = new Genome<decimal>(genome_1);
            Genome<decimal> offspring_2 = new Genome<decimal>(genome_2);

            Genome<decimal>.Mate(offspring_1, offspring_2, 0.7f, 0.05f, Chromosome<decimal>.MUTATION_TYPE.DELTA, 1m, 100m);

            // Make sure constructor by copy is functioning properly
            Assert.AreEqual(genome_1.GetChromosome(0).GetName(), "ChromosomeA");
            Assert.AreEqual(genome_1.GetChromosome(1).GetName(), "ChromosomeB");
            Assert.AreEqual(genome_1.GetChromosome(0).GetGenesDataCopyArray(), new decimal[] { 1, 2, 3, 4 });
            Assert.AreEqual(genome_1.GetChromosome(1).GetGenesDataCopyArray(), new decimal[] { 11, 12, 13, 14, 15, 16 });

            Assert.AreEqual(genome_2.GetChromosome(0).GetName(), "ChromosomeA");
            Assert.AreEqual(genome_2.GetChromosome(1).GetName(), "ChromosomeB");
            Assert.AreEqual(genome_2.GetChromosome(0).GetGenesDataCopyArray(), new decimal[] { 2.1m, 2.1m, 3.1m, 4.1m });
            Assert.AreEqual(genome_2.GetChromosome(1).GetGenesDataCopyArray(), new decimal[] { 11.1m, 12.1m, 13.1m, 14.1m, 15.1m, 16.1m });

            // Offspring should have the same number of chromosomes and genes, in the same order
            Assert.AreEqual(offspring_1.GetChromosomeCount(), genome_1.GetChromosomeCount());
            Assert.AreEqual(offspring_1.GetChromosomeCount(), genome_2.GetChromosomeCount());
            Assert.AreEqual(offspring_1.GetChromosome("ChromosomeA").GetGenesCount(), genome_1.GetChromosome(0).GetGenesCount());
            Assert.AreEqual(offspring_1.GetChromosome(1).GetGenesCount(), genome_2.GetChromosome("ChromosomeB").GetGenesCount());

            //Assert.AreNotEqual(true, true, offspring_1.ToString());
            //Assert.AreNotEqual(true, true, offspring_1.ToString() + " " + offspring_2.ToString());
        }
    }
}
