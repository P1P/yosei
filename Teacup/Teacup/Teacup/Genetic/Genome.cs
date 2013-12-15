using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

namespace Teacup.Genetic
{
    /// <summary>
    /// A genome that contains several chromosomes
    /// Represents the phenotype of an entity
    /// </summary>
    /// <typeparam name="T">The type of genetic information (struct)</typeparam>
	public class Genome<T> where T : struct
	{
        private Dictionary<string, Chromosome<T>> m_dict_chromosomes;
        private static Random m_static_random = new Random();

        /// <summary>
        /// Initializes the genome with a list of chromosomes
        /// </summary>
        /// <param name="p_array_chromosomes">The array of chromosomes</param>
        public Genome(params Chromosome<T>[] p_array_chromosomes)
        {
            m_dict_chromosomes = new Dictionary<string, Chromosome<T>>();

            foreach (Chromosome<T> chr in p_array_chromosomes)
            {
                m_dict_chromosomes.Add(chr.GetName(), chr);
            }
        }

        /// <summary>
        /// Copy constructor, clones the dictionnary of chromosomes of the given genome
        /// </summary>
        /// <param name="p_other">The genome to copy</param>
        public Genome(Genome<T> p_other)
        {
            m_dict_chromosomes = new Dictionary<string, Chromosome<T>>();

            foreach (KeyValuePair<string, Chromosome<T>> pair in p_other.m_dict_chromosomes)
            {
                m_dict_chromosomes.Add(pair.Key, new Chromosome<T>(pair.Value));
            }
        }

        /// <summary>
        /// Returns the chromosome at the given key
        /// </summary>
        /// <param name="p_key">Key of the chromosome</param>
        /// <returns>The chromosome at p_key</returns>
        public Chromosome<T> GetChromosome(string p_key)
        {
            return m_dict_chromosomes[p_key];
        }

        /// <summary>
        /// Returns the chromosome at the given index
        /// </summary>
        /// <param name="p_index">Index of the chromosome</param>
        /// <returns>The chromosome at p_index</returns>
        public Chromosome<T> GetChromosome(int p_index)
        {
            return m_dict_chromosomes.ElementAt(p_index).Value;
        }

        /// <summary>
        /// Adds a chromosome to the genome
        /// </summary>
        /// <param name="p_chromosome">The chromosome to add</param>
        public void AddChromosome(Chromosome<T> p_chromosome)
        {
            m_dict_chromosomes.Add(p_chromosome.GetName(), p_chromosome);
        }

        /// <summary>
        /// Returns the number of chromosomes in the genome
        /// </summary>
        /// <returns>The number of chromosomes</returns>
        public int GetChromosomeCount()
        {
            return m_dict_chromosomes.Count;
        }

        /// <summary>
        /// Mate two parent genomes to become offsprings through possible crossovers and mutations on each chromosome
        /// </summary>
        /// <param name="p_genome_1">The fist parent genome, to become the first offspring</param>
        /// <param name="p_genome_2">The second parent genome, to become the second offspring</param>
        /// <param name="p_crossover_rate">The probability for a crossover to occur on each chromosome</param>
        /// <param name="p_mutation_rate">The probability for a mutation to occur on each chromosome (not on each gene)</param>
        /// <param name="p_mutation_type">
        /// The type of mutations to occur
        /// DELTA: new gene = old gene +- random(0, delta); guaranteed to stay within bounds
        /// FULL: new gene = random(0, bounds); guaranteed to stay within bounds</param>
        /// <param name="p_mutation_delta">
        /// The variation (in case of delta mutation)
        /// A negative or positive offset of absolute value up to it will be added to the gene
        /// </param>
        /// <param name="p_mutation_bounds">The upper limit or the gene's value. Lower is always zero</param>
        public static void Mate(Genome<T> p_genome_1, Genome<T> p_genome_2, double p_crossover_rate, double p_mutation_rate,
            Chromosome<T>.MUTATION_TYPE p_mutation_type, decimal p_mutation_delta, decimal p_mutation_bounds)
        {
            for (int i = 0; i < p_genome_1.GetChromosomeCount(); ++i)
            {
                Chromosome<T> chr_1 = p_genome_1.GetChromosome(i);
                Chromosome<T> chr_2 = p_genome_2.GetChromosome(chr_1.GetName());

                Debug.Assert(chr_1.GetName() == chr_2.GetName());

                // Crossover
                if (m_static_random.NextDouble() < p_crossover_rate)
                {
                    Chromosome<T>.CrossOver(chr_1, chr_2);
                }

                // Mutation
                if (m_static_random.NextDouble() < p_mutation_rate)
                {
                    chr_1.Mutate(p_mutation_type, p_mutation_delta, p_mutation_bounds);
                }

                if (m_static_random.NextDouble() < p_mutation_rate)
                {
                    chr_2.Mutate(p_mutation_type, p_mutation_delta, p_mutation_bounds);
                }
            }
        }

        /// <summary>
        /// Returns a one-line display of this genome's chromosomes
        /// </summary>
        /// <returns>A string representation of the genome</returns>
        public override string ToString()
        {
            string str = "";

            bool first = true;
            foreach (KeyValuePair<string, Chromosome<T>> pair in m_dict_chromosomes)
            {
                if (first) { first = false; }
                else { str += " - "; }

                str += pair.Key.ToString() + " " + pair.Value.ToString();
            }

            return str;
        }
	}
}
