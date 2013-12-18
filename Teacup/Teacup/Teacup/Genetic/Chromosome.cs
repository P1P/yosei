using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Teacup.Genetic
{
    public enum MUTATION_TYPE { DELTA, FULL };

    /// <summary>
    /// A chromosome contains several genes
    /// Can undergo several transformations such as crossovers and gene mutations
    /// </summary>
    /// <typeparam name="T">The type of genetic information (struct)</typeparam>
    public class Chromosome<T> where T : struct
    {
        private LinkedList<T> m_lst_genes;
        private string m_name;
        private GeneticOperatorRules m_genetic_operator_rules;

        private static Random m_random = new Random();

        /// <summary>
        /// Initializes the chromosome with a list of genes
        /// </summary>
        /// <param name="p_name">The name of the chromosome</param>
        /// <param name="p_genetic_operator_rules">The crossover and mutation parameters of the chromosome</param>
        /// <param name="p_array_genes">The array of genes</param>
        public Chromosome(string p_name, GeneticOperatorRules p_genetic_operator_rules, params T[] p_array_genes)
        {
            m_name = p_name;
            m_genetic_operator_rules = p_genetic_operator_rules;

            m_lst_genes = new LinkedList<T>(p_array_genes);
        }

        /// <summary>
        /// Initializes the chromosome with a random list of genes
        /// </summary>
        /// <param name="p_name">The name of the chromosome</param>
        /// <param name="p_nb_genes">The number of random genes to initialize the chromosome with</param>
        /// <param name="p_genetic_operator_rules">The crossover and mutation parameters of the chromosome</param>
        public Chromosome(string p_name, int p_nb_genes, GeneticOperatorRules p_genetic_operator_rules)
        {
            m_name = p_name;
            m_genetic_operator_rules = p_genetic_operator_rules;

            m_lst_genes = new LinkedList<T>();

            MUTATION_TYPE mutation_type = m_genetic_operator_rules.m_mutation_type;

            m_genetic_operator_rules.m_mutation_type = MUTATION_TYPE.FULL;
            for (int i = 0; i < p_nb_genes; ++i)
            {
                m_lst_genes.AddLast(MutateGene(default(T)));
            }
            m_genetic_operator_rules.m_mutation_type = mutation_type;
        }

        /// <summary>
        /// Copy constructor, deep-copies the list of genes of the given chromosome
        /// </summary>
        /// <param name="p_other">The chromosome to copy</param>
        public Chromosome(Chromosome<T> p_other)
        {
            m_name = p_other.m_name;
            m_genetic_operator_rules = p_other.m_genetic_operator_rules;

            m_lst_genes = new LinkedList<T>(p_other.m_lst_genes);
        }

        /// <summary>
        /// Performs a chromosomal crossover
        /// Swaps will happen after the junction point
        /// Requires both chromosomes to have the same length
        /// </summary>
        /// <param name="p_chr_1">The first chromosome</param>
        /// <param name="p_chr_2">The second chromosome</param>
        /// <param name="p_junction_index">The point of junction. No parameter or -1 for random index</param>
        /// <param name="p_order">The order from which the cross over will happen. 1 forward, 0 backward. No parameter or -1 for random order</param>
        public static void CrossOver(Chromosome<T> p_chr_1, Chromosome<T> p_chr_2, int p_junction_index = -1, int p_order = -1)
        {
            int chr_1_length = p_chr_1.m_lst_genes.Count;

            // Requires same length on both chromosomes
            Debug.Assert(chr_1_length == p_chr_2.m_lst_genes.Count);

            // Random junction
            if (p_junction_index < 0)
            {
                p_junction_index = m_random.Next(0, chr_1_length);
            }

            // Randomly decide from what side of the junction the crossover should happen
            bool order = (p_order == 0 ? false : p_order == 1 ? true : m_random.Next(2) == 0);

            LinkedListNode<T> node_chr_1, node_chr_2;
            T gene_tmp;

            node_chr_1 = p_chr_1.m_lst_genes.First;
            node_chr_2 = p_chr_2.m_lst_genes.First;

            // Cross genes over after (or before) the junction point
            for (int i = 0; i < chr_1_length; ++i)
            {
                if ((order && i >= p_junction_index) || (!order && i < p_junction_index))
                {
                    gene_tmp = node_chr_1.Value;

                    node_chr_1.Value = node_chr_2.Value;
                    node_chr_2.Value = gene_tmp;
                }

                node_chr_1 = node_chr_1.Next;
                node_chr_2 = node_chr_2.Next;
            }
        }

        /// <summary>
        /// Mutates a chromosome with an offset, or without regards to the genes' current values
        /// </summary>
        /// <param name="p_index">The index of the gene the mutation should occur on. No value or -1 for random index</param>
        public void Mutate(int p_index = -1)
        {
            // Random mutation location
            if (p_index < 0)
            {
                p_index = m_random.Next(0, this.m_lst_genes.Count);
            }

            LinkedListNode<T> node = this.m_lst_genes.First;
            for (int i = 0; i < this.m_lst_genes.Count; ++i)
            {
                if (i == p_index)
                {
                    node.Value = MutateGene(node.Value);
                    break;
                }
                node = node.Next;
            }
        }

        /// <summary>
        /// Returns the name of the chromosome
        /// </summary>
        /// <returns>The name of the chromosome</returns>
        public string GetName()
        {
            return m_name;
        }

        /// <summary>
        /// Returns the genetic operator rules of the chromosome
        /// </summary>
        /// <returns>The genetic operator rules of the chromosome</returns>
        public GeneticOperatorRules GetGeneticOperatorRules()
        {
            return m_genetic_operator_rules;
        }

        /// <summary>
        /// Returns the gene at the given index
        /// </summary>
        /// <param name="p_index">Index of the gene. Must be lower than gene count</param>
        /// <returns>The gene at p_index</returns>
        public T GetGene(int p_index)
        {
            Debug.Assert(p_index < m_lst_genes.Count);

            int i = 0;
            foreach (T gene in m_lst_genes)
            {
                if (i == p_index)
                {
                    return gene;
                }
                ++i;
            }

            return default(T);
        }

        /// <summary>
        /// Adds a gene at the end of the chromosome
        /// </summary>
        /// <param name="p_gene">The new gene</param>
        public void AddGene(T p_gene)
        {
            m_lst_genes.AddLast(p_gene);
        }

        /// <summary>
        /// Returns the number of genes in this chromosome
        /// </summary>
        /// <returns>The number of genes in this chromosome</returns>
        public int GetGenesCount()
        {
            return m_lst_genes.Count;
        }

        /// <summary>
        /// Returns an array containg a copy of the genes of the chromosomes
        /// </summary>
        /// <returns>An array of copies of genes of the chromosome</returns>
        public T[] GetGenesCopyArray()
        {
            T[] array_genes = new T[m_lst_genes.Count];

            int i = 0;
            foreach (T gene in m_lst_genes)
            {
                array_genes[i] = gene;
                i++;
            }

            return array_genes;
        }

        /// <summary>
        /// Returns an array containg a copy of the pieces of data of the genes of the chromosomes
        /// </summary>
        /// <returns>An array of copies of the pieces of data of the genes of the chromosome</returns>
        public T[] GetGenesDataCopyArray()
        {
            T[] array_data = new T[m_lst_genes.Count];

            int i = 0;
            foreach (T gene in m_lst_genes)
            {
                array_data[i] = gene;
                i++;
            }

            return array_data;
        }

        /// <summary>
        /// Returns a one-line concatenation of the chromosome's genes' ToString() with a '|' separator
        /// </summary>
        /// <returns>A string representation of the chromosome</returns>
        public override string ToString()
        {
            string str = "{";

            int i = 0;
            foreach (T gene in m_lst_genes)
            {
                str += String.Format("{0:000.00}", gene);

                if (i < m_lst_genes.Count - 1)
                {
                    str += "; ";
                }
                i++;
            }

            str += "}";

            return str;
        }

        /// <summary>
        /// Equals override. Based on the Equals of Gene 
        /// </summary>
        /// <param name="obj">The Chromosome of the same type to compare with</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            Chromosome<T> other = (Chromosome<T>)obj;

            if (this.GetGenesCount() != other.GetGenesCount())
            {
                return false;
            }

            LinkedListNode<T> this_genes_node = this.m_lst_genes.First;
            LinkedListNode<T> other_genes_node = other.m_lst_genes.First;

            for (int i = 0; i < GetGenesCount(); ++i)
            {
                if (!this_genes_node.Value.Equals(other_genes_node.Value))
                {
                    return false;
                }
                this_genes_node = this_genes_node.Next;
                other_genes_node = other_genes_node.Next;
            }

            return true;
        }

        /// <summary>
        /// GetHashCode override. Based on the GetHashCode of the list of genes
        /// </summary>
        /// <returns>int depending on the GetHashCode of the list of genes</returns>
        public override int GetHashCode()
        {
            return m_lst_genes.GetHashCode();
        }

        /// <summary>
        /// Sets the seed of the static random number generator for all the chromosomes
        /// </summary>
        /// <param name="p_seed">The RNG seed</param>
        public static void SetRandomSeed(int p_seed)
        {
            m_random = new Random(p_seed);
        }

        /// <summary>
        /// Mutates a gene with an offset, or without regards to the gene's current value
        /// </summary>
        /// <param name="p_gene">The gene to mutate</param>
        /// <returns>The gene after full mutation</returns>
        private T MutateGene(T p_gene)
        {
            if (p_gene is decimal)
            {
                return (T)(object)MutateGeneDecimal((decimal)(object)p_gene);
            }
            if (p_gene is int)
            {
                return (T)(object)MutateGeneInteger((int)(object)p_gene);
            }

            Debug.Assert(false);

            return default(T);
        }

        private decimal MutateGeneDecimal(decimal p_gene)
        {
            switch (m_genetic_operator_rules.m_mutation_type)
            {
                case MUTATION_TYPE.DELTA:
                    int sign = (m_random.Next(2) == 0) ? 1 : -1;
                    decimal offset = ((decimal)m_random.NextDouble()) * m_genetic_operator_rules.m_mutation_delta * sign;
                    return Math.Max(m_genetic_operator_rules.m_mutation_lower_bound, Math.Min(m_genetic_operator_rules.m_mutation_upper_bound, p_gene + offset));
                case MUTATION_TYPE.FULL:
                    return ((decimal)m_random.NextDouble()) * (m_genetic_operator_rules.m_mutation_upper_bound - m_genetic_operator_rules.m_mutation_lower_bound) + m_genetic_operator_rules.m_mutation_lower_bound;
                default:
                    Debug.Assert(false);
                    return p_gene;
            }
        }
        
        private int MutateGeneInteger(int p_gene)
        {
            switch (m_genetic_operator_rules.m_mutation_type)
            {
                case MUTATION_TYPE.DELTA:
                    int sign = (m_random.Next(2) == 0) ? 1 : -1;
                    int offset = m_random.Next((int)m_genetic_operator_rules.m_mutation_delta) * sign;
                    return Math.Max((int)m_genetic_operator_rules.m_mutation_lower_bound, Math.Min((int)m_genetic_operator_rules.m_mutation_upper_bound, p_gene + offset));
                case MUTATION_TYPE.FULL:
                    return ((m_random.Next() % (int)(m_genetic_operator_rules.m_mutation_upper_bound - m_genetic_operator_rules.m_mutation_lower_bound)) + (int)m_genetic_operator_rules.m_mutation_lower_bound);
                default:
                    Debug.Assert(false);
                    return p_gene;
            }
        }
    }
}
