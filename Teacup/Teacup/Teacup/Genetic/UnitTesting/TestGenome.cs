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

            Chromosome<decimal> chr_2A = new Chromosome<decimal>("ChromosomeA", -1, -2, -3, -4);
            Chromosome<decimal> chr_2B = new Chromosome<decimal>("ChromosomeB", -11, -12, -13, -14, -15, -16);

            Genome<decimal> genome_1 = new Genome<decimal>(new Chromosome<decimal>[] { chr_1A, chr_1B });
            Genome<decimal> genome_2 = new Genome<decimal>(new Chromosome<decimal>[] { chr_2A, chr_2B });

            Genome<decimal> genome_offspring = Genome<decimal>.Mate(new Genome<decimal>(genome_1), new Genome<decimal>(genome_2), 0.70f, 0.2f);

            // Make sure constructor by copy is functioning properly
            Assert.AreEqual(genome_1.GetChromosome(0).GetName(), "ChromosomeA");
            Assert.AreEqual(genome_1.GetChromosome(1).GetName(), "ChromosomeB");
            Assert.AreEqual(genome_1.GetChromosome(0).GetGenesDataCopyArray(), new decimal[] { 1, 2, 3, 4 });
            Assert.AreEqual(genome_1.GetChromosome(1).GetGenesDataCopyArray(), new decimal[] { 11, 12, 13, 14, 15, 16 });

            Assert.AreEqual(genome_2.GetChromosome(0).GetName(), "ChromosomeA");
            Assert.AreEqual(genome_2.GetChromosome(1).GetName(), "ChromosomeB");
            Assert.AreEqual(genome_2.GetChromosome(0).GetGenesDataCopyArray(), new decimal[] { -1, -2, -3, -4 });
            Assert.AreEqual(genome_2.GetChromosome(1).GetGenesDataCopyArray(), new decimal[] { -11, -12, -13, -14, -15, -16 });

            // Offspring should have the same number of chromosomes and genes, in the same order
            Assert.AreEqual(genome_offspring.GetChromosomeCount(), genome_1.GetChromosomeCount());
            Assert.AreEqual(genome_offspring.GetChromosomeCount(), genome_2.GetChromosomeCount());
            Assert.AreEqual(genome_offspring.GetChromosome("ChromosomeA").GetGenesCount(), genome_1.GetChromosome(0).GetGenesCount());
            Assert.AreEqual(genome_offspring.GetChromosome(1).GetGenesCount(), genome_2.GetChromosome("ChromosomeB").GetGenesCount());

            //Assert.AreNotEqual(true, true, genome_offspring.ToString());
        }
    }
}
