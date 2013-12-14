using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Teacup.Genetic
{
    /// <summary>
    /// A population that contains several genomes
    /// Selectionsof the fittest happen here
    /// </summary>
    /// <typeparam name="T">The type of genetic information (struct)</typeparam>
    public class Population<T> where T : struct
    {
        private List<Genome<T>> m_lst_genomes;

        /// <summary>
        /// Initializes the population with a list of genomes
        /// </summary>
        /// <param name="p_array_genomes">The array of genomes</param>
        public Population(params Genome<T>[] p_array_genomes)
        {
            m_lst_genomes = new List<Genome<T>>(p_array_genomes);
        }

        /// <summary>
        /// Copy constructor, clones the list of genomes of the given population
        /// </summary>
        /// <param name="p_other">The population to copy</param>
        public Population(Population<T> p_other)
        {
            m_lst_genomes = new List<Genome<T>>();

            foreach (Genome<T> genome in p_other.m_lst_genomes)
            {
                m_lst_genomes.Add(new Genome<T>(genome));
            }
        }

        /// <summary>
        /// Returns the number of tickets every genome can get in the grand selection
        /// </summary>
        /// <returns>The list of number of tickets for every genome</returns>
        public List<decimal> GetTickets()
        {
            List<decimal> lst_tickets = new List<decimal>();

            decimal total_fitness = Decimal.Zero;

            // Computing how many tickets we can give
            for (int i = 0; i < m_lst_genomes.Count; ++i)
            {
                total_fitness += GetFitness(m_lst_genomes[i]);
            }

            // Attributing tickets
            for (int i = 0; i < m_lst_genomes.Count; ++i)
            {
                lst_tickets.Add(GetFitness(m_lst_genomes[i]) / total_fitness);
            }

            return lst_tickets;
        }

        public List<Genome<T>> SelectRoulette(List<decimal> p_lst_tickets)
        {
            List<Genome<T>> lst_parents = new List<Genome<T>>();

            return lst_parents;
        }

        public static decimal GetFitness(Genome<T> p_genome)
        {
            decimal fitness = Decimal.Zero;

            if (p_genome is Genome<decimal>)
            {
                for (int i = 0; i < p_genome.GetChromosomeCount(); ++i)
                {
                    for (int j = 0; j < p_genome.GetChromosome(i).GetGenesCount(); ++j)
                    {
                        fitness += (p_genome.GetChromosome(i).GetGene(j) as Gene<decimal>).GetData();
                    }
                }
            }

            return fitness;
        }
    }
}
