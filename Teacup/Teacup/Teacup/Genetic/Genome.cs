using System;
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
        private List<Chromosome<T>> m_lst_chromosomes;

        /// <summary>
        /// Initializes the genome with a list of chromosomes
        /// </summary>
        /// <param name="p_lst_chromosomes">The list of chromosomes</param>
        public Genome(params Chromosome<T>[] p_array_chromosomes)
        {
            m_lst_chromosomes = new List<Chromosome<T>>(p_array_chromosomes);
            
        }

        /// <summary>
        /// Returns the chromosome at the given index
        /// </summary>
        /// <param name="p_index">Index of the chromosome. Must be lower than chromosomes count</param>
        /// <returns>The chromosome at p_index</returns>
        public Chromosome<T> GetChromosome(int p_index)
        {
            return m_lst_chromosomes[p_index];
        }
	}
}
