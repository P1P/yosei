using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using Teacup.Helpers;

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
        private Random m_static_random = new Random();

        /// <summary>
        /// Delegate of the fitness function
        /// Attributes a score (non-negative) to the genome
        /// From this usually depends its odds to be selected for mating
        /// </summary>
        /// <param name="p_genome">The genome to score</param>
        /// <returns>The fitness score of the genome</returns>
        public delegate decimal FitnessDelegate(Genome<T> p_genome);

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
        /// Returns the genome at the given index
        /// </summary>
        /// <param name="p_index">Index of the genome</param>
        /// <returns>The genome at p_index</returns>
        public Genome<T> GetGenome(int p_index)
        {
            return m_lst_genomes[p_index];
        }

        /// <summary>
        /// Adds a genome to the population
        /// </summary>
        /// <param name="p_genome">The genome to add</param>
        public void AddGenome(Genome<T> p_genome)
        {
            m_lst_genomes.Add(p_genome);
        }

        /// <summary>
        /// Returns the number of genomes in the population
        /// </summary>
        /// <returns>The number of genomes</returns>
        public int GetGenomeCount()
        {
            return m_lst_genomes.Count;
        }

        /// <summary>
        /// Returns a list of offsprings from the genetic pools
        /// </summary>
        /// <param name="p_genetic_pool">The genetic pool</param>
        /// <returns>The population of offsprings</returns>
        public Population<T> GetChildren(List<Genome<T>> p_genetic_pool)
        {
            List<Genome<T>> lst_children = new List<Genome<T>>();

            lst_children.Shuffle();

            for (int i = 0; i < p_genetic_pool.Count - 1; i += 2)
            {
                Genome<T>.Mate(p_genetic_pool[i], p_genetic_pool[i + 1]);

                lst_children.Add(p_genetic_pool[i]);
                lst_children.Add(p_genetic_pool[i + 1]);
            }

            return new Population<T>(lst_children.ToArray());
        }

        /// <summary>
        /// Selects the parents of the next generation from a roulette wheel algorithm
        /// A certain amount of draws are made, and the better the fitness of a genome, higher are its chance to be selected
        /// Every time a genome  is selected, a copy of it is added to the genetic pool for the next generation
        /// </summary>
        /// <param name="p_delegate_fitness">The fitness function to score each genome</param>
        /// <returns>The genetic pool for the next generation</returns>
        public List<Genome<T>> SelectRoulette(FitnessDelegate p_delegate_fitness)
        {
            List<Tuple<decimal, decimal>> lst_tickets = GetTickets(p_delegate_fitness);
			
            int resulting_population = m_lst_genomes.Count;
            List<Genome<T>> lst_parents = new List<Genome<T>>();

            for (int i = 0; i < resulting_population; ++i)
            {
                decimal draw = (decimal)m_static_random.NextDouble();

                for (int j = 0; j < lst_tickets.Count; ++j)
                {
                    // Find our winner for this draw
                    if (draw >= lst_tickets[j].First && draw <= lst_tickets[j].Second)
                    {
                        lst_parents.Add(new Genome<T>(m_lst_genomes[j]));
                        break;
                    }
                }
            }

            return lst_parents;
        }

        /// <summary>
        /// Returns a multi-line display of this population's genomes
        /// </summary>
        /// <returns>A string representation of the population</returns>
        public override string ToString()
        {
            string str = "";

            bool first = true;
            foreach (Genome<T> genome in m_lst_genomes)
            {
                if (first) { first = false; }
                else { str += "\n"; }

                str += "||" + genome.ToString() + "||";
            }

            return str;
        }

        /// <summary>
        /// Returns the tickets that every genome get in the grand roulette selection
        /// e.g. a valid ticket would be "from 0.1 to 0.3" for a genome with a fitness of 0.2
        /// </summary>
        /// <param name="p_delegate_fitness">The fitness function to score each genome</param>
        /// <returns>The list of tickets/intervals for each genome</returns>
        private List<Tuple<decimal, decimal>> GetTickets(FitnessDelegate p_delegate_fitness)
        {
            List<Tuple<decimal, decimal>> lst_tickets = new List<Tuple<decimal, decimal>>();

            decimal total_fitness = Decimal.Zero;

            // Computing how many tickets we can give
            for (int i = 0; i < m_lst_genomes.Count; ++i)
            {
                total_fitness += p_delegate_fitness(m_lst_genomes[i]);
            }

            decimal current_position_in_fitness = Decimal.Zero;

            // Attributing tickets/intervals
            for (int i = 0; i < m_lst_genomes.Count; ++i)
            {
                decimal fitness_proportion = p_delegate_fitness(m_lst_genomes[i]) / total_fitness;
                lst_tickets.Add(new Tuple<decimal, decimal>(current_position_in_fitness, current_position_in_fitness + fitness_proportion));
                current_position_in_fitness += fitness_proportion;
            }

            return lst_tickets;
        }
    }
}
