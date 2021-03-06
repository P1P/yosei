﻿using System;
using System.Collections.Generic;
using Teacup.Genetic;
using NUnit.Framework;

namespace Teacup.Genetic.UnitTesting
{
    [TestFixture]
    public class TestPopulation
    {
        [Test]
        public void BalancedSelection()
        {
            GeneticOperatorRules rules = new GeneticOperatorRules(0.7, MUTATION_TYPE.DELTA, 0.01, 0.1m, 0m, 1m);

            Chromosome<decimal> chr_1A = new Chromosome<decimal>("ChromosomeA", rules, new decimal[] { 2m, 2m, 0m });
            Chromosome<decimal> chr_1B = new Chromosome<decimal>("ChromosomeB", rules, new decimal[] { 2m, 2m, 0m });

            Chromosome<decimal> chr_2A = new Chromosome<decimal>("ChromosomeA", rules, new decimal[] { 0m, 2m, 2m });
            Chromosome<decimal> chr_2B = new Chromosome<decimal>("ChromosomeB", rules, new decimal[] { 0m, 2m, 2m });

            Genome<decimal> genome_1 = new Genome<decimal>(chr_1A, chr_1B);
            Genome<decimal> genome_2 = new Genome<decimal>(chr_2A, chr_2B);

            Population<decimal> pop_1 = new Population<decimal>(genome_1, genome_2);

            List<Genome<decimal>> genetic_pool_1 = pop_1.SelectRoulette(FitnessDelegate);

            // Check that we have a properly size genetic pool
            Assert.AreEqual(genetic_pool_1.Count, 2, genetic_pool_1[0].ToString() + " " + genetic_pool_1[1].ToString());
        }

        [Test]
        public void PredominantSelection()
        {
            GeneticOperatorRules rules = new GeneticOperatorRules(0.7, MUTATION_TYPE.DELTA, 0.1, 150m, 50m, 999m);

            Chromosome<decimal> chr_1A = new Chromosome<decimal>("ChromosomeA", 2, rules);
            Chromosome<decimal> chr_1B = new Chromosome<decimal>("ChromosomeB", 2, rules);

            List<Genome<decimal>> lst_base_genomes = new List<Genome<decimal>>();

            lst_base_genomes.Add(new Genome<decimal>(chr_1A, chr_1B));

            List<Genome<decimal>> lst_genomes = new List<Genome<decimal>>();
            for (int i = 0; i < 10; ++i)
            {
                foreach (Genome<decimal> base_genome in lst_base_genomes)
                {
                    lst_genomes.Add(new Genome<decimal>(base_genome));
                }
            }

            Population<decimal> pop_1 = new Population<decimal>(lst_genomes.ToArray());

            for (int i = 0; i < 1000; ++i)
            {
                List<Genome<decimal>> genetic_pool_1 = pop_1.SelectRoulette(FitnessDelegate);

                Assert.AreEqual(genetic_pool_1.Count, pop_1.GetGenomeCount());
                pop_1 = pop_1.GetChildren(genetic_pool_1);
            }

            Assert.AreEqual(pop_1.GetGenomeCount(), pop_1.GetGenomeCount());
            //Assert.AreNotEqual(true, true, pop_1.ToString());
        }

        private decimal FitnessDelegate(Genome<decimal> p_genome)
        {
            decimal fitness = 0m;

            for (int i = 0; i < p_genome.GetChromosomeCount(); ++i)
            {
                for (int j = 0; j < p_genome.GetChromosome(i).GetGenesCount(); ++j)
                {
                    fitness += p_genome.GetChromosome(i).GetGene(j);
                }
            }
            
            decimal difference = Math.Abs(250m - fitness);

            fitness = 1m / Math.Max(0.00001m, difference);

            return Math.Max(0.000001m, fitness);
        }
    }
}
