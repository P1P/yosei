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
        /// <summary>
        /// The type of mutations to occur
        /// DELTA: new gene = old gene +- random(0, delta); guaranteed to stay within bounds
        /// FULL: new gene = random(0, bounds); guaranteed to stay within bounds
        /// </summary>
        public MUTATION_TYPE m_mutation_type { get; set; }

        /// <summary>
        /// The probability for a crossover to occur on each chromosome
        /// </summary>
        public double m_crossover_rate { get; set; }

        /// <summary>
        /// The probability for a mutation to occur on each chromosome (not on each gene)
        /// </summary>
        public double m_mutation_rate { get; set; }

        /// <summary>
        /// The variation (in case of delta mutation)
        /// A negative or positive offset of absolute value up to it will be added to the gene
        /// </summary>
        public decimal m_mutation_delta { get; set; }

        /// <summary>
        /// The lower limit or the gene's value
        /// </summary>
        public decimal m_mutation_lower_bound { get; set; }

        /// <summary>
        /// The upper limit or the gene's value
        /// </summary>
        public decimal m_mutation_upper_bound { get; set; }

        private LinkedList<Gene<T>> m_lst_genes;
        private string m_name;

        private static Random m_random = new Random();

        /// <summary>
        /// Initializes the chromosome with a list of genes
        /// </summary>
        /// <param name="p_array_genes">The array of genes</param>
        public Chromosome(string p_name, double p_crossover_rate, MUTATION_TYPE p_mutation_type, double p_mutation_rate, decimal p_mutation_delta, decimal p_mutation_lower_bound, decimal p_mutation_upper_bound, params Gene<T>[] p_array_genes)
        {
            m_name = p_name;
            m_lst_genes = new LinkedList<Gene<T>>(p_array_genes);

            m_crossover_rate = p_crossover_rate;
            m_mutation_type = p_mutation_type;
            m_mutation_rate = p_mutation_rate;
            m_mutation_delta = p_mutation_delta;
            m_mutation_lower_bound = p_mutation_lower_bound;
            m_mutation_upper_bound = p_mutation_upper_bound;
        }

        /// <summary>
        /// Initializes the chromosome with a list of data
        /// </summary>
        /// <param name="p_lst_genes">The list of genes</param>
        public Chromosome(string p_name, double p_crossover_rate, MUTATION_TYPE p_mutation_type, double p_mutation_rate, decimal p_mutation_delta, decimal p_mutation_lower_bound, decimal p_mutation_upper_bound, params T[] p_array_data)
        {
            m_name = p_name;

            m_crossover_rate = p_crossover_rate;
            m_mutation_type = p_mutation_type;
            m_mutation_rate = p_mutation_rate;
            m_mutation_delta = p_mutation_delta;
            m_mutation_lower_bound = p_mutation_lower_bound;
            m_mutation_upper_bound = p_mutation_upper_bound;

            m_lst_genes = new LinkedList<Gene<T>>();

            foreach (T data in p_array_data)
            {
                m_lst_genes.AddLast(new Gene<T>(data));
            }
        }

        /// <summary>
        /// Copy constructor, deep-copies the list of genes of the given chromosome
        /// </summary>
        /// <param name="p_other">The chromosome to copy</param>
        public Chromosome(Chromosome<T> p_other)
        {
            m_name = p_other.m_name;

            m_crossover_rate = p_other.m_crossover_rate;
            m_mutation_type = p_other.m_mutation_type;
            m_mutation_rate = p_other.m_mutation_rate;
            m_mutation_delta = p_other.m_mutation_delta;
            m_mutation_lower_bound = p_other.m_mutation_lower_bound;
            m_mutation_upper_bound = p_other.m_mutation_upper_bound;

            m_lst_genes = new LinkedList<Gene<T>>();

            foreach (Gene<T> gene in p_other.m_lst_genes)
            {
                m_lst_genes.AddLast(new Gene<T>(gene));
            }
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

            LinkedListNode<Gene<T>> node_chr_1, node_chr_2;
            Gene<T> gene_tmp;

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

        public delegate Gene<T> Mutation(Gene<T> p_gene);

        /// <summary>
        /// Mutates a chromosome with an offset, or without regards to the genes' current values
        /// </summary>
        /// <param name="p_mutation_type">
        /// The type of mutations to occur
        /// DELTA: new gene = old gene +- random(0, delta); guaranteed to stay within bounds
        /// FULL: new gene = random(0, bounds); guaranteed to stay within bounds</param>
        /// <param name="p_delta">
        /// The variation (in case of delta mutation)
        /// A negative or positive offset of absolute value up to it will be added to the gene
        /// </param>
        /// <param name="p_lower_bound">The lower limit or the gene's value</param>
        /// <param name="p_upper_bound">The upper limit or the gene's value</param>
        /// <param name="p_index">The index of the gene the mutation should occur on. No value or -1 for random index</param>
        public void Mutate(MUTATION_TYPE p_mutation_type, decimal p_delta, decimal p_lower_bound, decimal p_upper_bound, int p_index = -1)
        {
            // Random mutation location
            if (p_index < 0)
            {
                p_index = m_random.Next(0, this.m_lst_genes.Count);
            }

            LinkedListNode<Gene<T>> node = this.m_lst_genes.First;
            for (int i = 0; i < this.m_lst_genes.Count; ++i)
            {
                if (i == p_index)
                {
                    node.Value = MutateGene(node.Value, p_mutation_type, p_delta, p_lower_bound, p_upper_bound);
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
        /// Returns the gene at the given index
        /// </summary>
        /// <param name="p_index">Index of the gene. Must be lower than gene count</param>
        /// <returns>The gene at p_index</returns>
        public Gene<T> GetGene(int p_index)
        {
            Debug.Assert(p_index < m_lst_genes.Count);

            int i = 0;
            foreach (Gene<T> gene in m_lst_genes)
            {
                if (i == p_index)
                {
                    return gene;
                }
                ++i;
            }

            return null;
        }

        /// <summary>
        /// Adds a gene at the end of the chromosome
        /// </summary>
        /// <param name="p_gene">The new gene</param>
        public void AddGene(Gene<T> p_gene)
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
        public Gene<T>[] GetGenesCopyArray()
        {
            Gene<T>[] array_genes = new Gene<T>[m_lst_genes.Count];

            int i = 0;
            foreach (Gene<T> gene in m_lst_genes)
            {
                array_genes[i] = new Gene<T>(gene);
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
            foreach (Gene<T> gene in m_lst_genes)
            {
                array_data[i] = gene.GetData();
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
            foreach (Gene<T> gene in m_lst_genes)
            {
                str += gene.ToString();

                if (i < m_lst_genes.Count - 1)
                {
                    str += ", ";
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

            LinkedListNode<Gene<T>> this_genes_node = this.m_lst_genes.First;
            LinkedListNode<Gene<T>> other_genes_node = other.m_lst_genes.First;

            for (int i = 0; i < GetGenesCount(); ++i)
            {
                if (this_genes_node.Value != other_genes_node.Value)
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
        /// <param name="p_mutation_type">
        /// The type of mutation to occur
        /// DELTA: new gene = old gene +- random(0, delta); guaranteed to stay within bounds
        /// FULL: new gene = random(0, bounds); guaranteed to stay within bounds</param>
        /// <param name="p_gene">The gene to mutate</param>
        /// <param name="p_delta">
        /// The variation (in case of delta mutation)
        /// A negative or positive offset of absolute value up to it will be added to the gene
        /// </param>
        /// <param name="p_lower_bound">The lower limit or the gene's value</param>
        /// <param name="p_upper_bound">The upper limit or the gene's value</param>
        /// <returns>The gene after full mutation</returns>
        private static Gene<T> MutateGene(Gene<T> p_gene, MUTATION_TYPE p_mutation_type, decimal p_delta, decimal p_lower_bound, decimal p_upper_bound)
        {
            if (p_gene is Gene<decimal>)
            {
                switch (p_mutation_type)
                {
                    case MUTATION_TYPE.DELTA:
                        int sign = (m_random.Next(2) == 0) ? 1 : -1;
                        decimal offset = ((decimal)m_random.NextDouble()) * p_delta * sign;

                        (p_gene as Gene<decimal>).SetData(Math.Max(p_upper_bound, Math.Min(p_lower_bound, (p_gene as Gene<decimal>).GetData() + offset)));
                        break;
                    case MUTATION_TYPE.FULL:
                        decimal newvalue = ((decimal)m_random.NextDouble()) * p_lower_bound;

                        (p_gene as Gene<decimal>).SetData(Math.Max(p_upper_bound, Math.Min(p_lower_bound, newvalue)));

                        break;
                }
            }
            else if (p_gene is Gene<int>)
            {
                switch (p_mutation_type)
                {
                    case MUTATION_TYPE.DELTA:
                        int sign = (m_random.Next(2) == 0) ? 1 : -1;
                        int offset = m_random.Next((int)p_delta) * sign;

                        (p_gene as Gene<int>).SetData(Math.Max((int)p_upper_bound, Math.Min((int)p_lower_bound, (p_gene as Gene<int>).GetData() + offset)));
                        break;
                    case MUTATION_TYPE.FULL:
                        int newvalue = m_random.Next() % (int)(p_lower_bound);

                        (p_gene as Gene<int>).SetData(Math.Max((int)p_upper_bound, Math.Min((int)p_lower_bound, newvalue)));

                        break;
                }
            }
            else
            {
                Debug.Assert(false);
            }

            return p_gene;
        }
    }
}
