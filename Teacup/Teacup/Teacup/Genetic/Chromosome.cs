using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Teacup.Genetic
{
    /// <summary>
    /// A chromosome contains several genes
    /// Can undergo several transformations such as crossovers and gene mutations
    /// </summary>
    /// <typeparam name="T">The type of genetic information (struct)</typeparam>
    public class Chromosome<T> where T : struct
    {
        private LinkedList<Gene<T>> m_lst_genes;
        private static Random m_random = new Random();

        /// <summary>
        /// Initializes the chromosome with a list of genes
        /// </summary>
        /// <param name="p_lst_genes">The list of genes</param>
        public Chromosome(params Gene<T>[] p_array_genes)
        {
            m_lst_genes = new LinkedList<Gene<T>>(p_array_genes);
        }

        /// <summary>
        /// Initializes the chromosome with a list of data
        /// </summary>
        /// <param name="p_lst_genes">The list of genes</param>
        public Chromosome(params T[] p_array_data)
        {
            m_lst_genes = new LinkedList<Gene<T>>();

            foreach (T data in p_array_data)
            {
                m_lst_genes.AddLast(new Gene<T>(data));
            }
        }

        /// <summary>
        /// Copy constructor, clones the list of genes of the given chromosome
        /// </summary>
        /// <param name="p_other">The Chromosome to copy</param>
        public Chromosome(Chromosome<T> p_other)
        {
            m_lst_genes = new LinkedList<Gene<T>>(p_other.m_lst_genes);
        }

        /// <summary>
        /// Performs a chromosomal crossover
        /// Swaps will happen after the junction point
        /// Requires both chromosomes to have the same length
        /// </summary>
        /// <param name="p_chr_1">The first chromosome</param>
        /// <param name="p_chr_2">The second chromosome</param>
        /// <param name="p_junction_index">The point of junction. No parameter or -1 for random index</param>
        public static void CrossOver(Chromosome<T> p_chr_1, Chromosome<T> p_chr_2, int p_junction_index)
        {
            int chr_1_length = p_chr_1.m_lst_genes.Count;

            // Requires same length on both chromosomes
            Debug.Assert(chr_1_length == p_chr_2.m_lst_genes.Count);

            // Random junction
            if (p_junction_index < 0)
            {
                p_junction_index = m_random.Next(0, chr_1_length);
            }

            LinkedListNode<Gene<T>> node_chr_1, node_chr_2;
            Gene<T> gene_tmp;

            node_chr_1 = p_chr_1.m_lst_genes.First;
            node_chr_2 = p_chr_2.m_lst_genes.First;

            // Cross genes over after the junction point
            for (int i = 0; i < chr_1_length; ++i)
            {
                if (i >= p_junction_index)
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
        /// Mutates this chromosome by changing a gene at either a given or randomly selected index
        /// </summary>
        /// <param name="p_index">The index of the gene the mutation should occur on. No value or -1 for random index</param>
        public void Mutate(int p_index = -1)
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
                    node.Value = MutateGene(node.Value);
                    break;
                }
                node = node.Next;
            }
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
                array_data[i] = gene.get_data();
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
            string str = "|";

            foreach (Gene<T> gene in m_lst_genes)
            {
                str += gene.ToString() + "|";
            }

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
        /// Mutates a gene
        /// </summary>
        /// <param name="p_gene">The gene to mutate</param>
        /// <returns>The gene after mutation</returns>
        public static Gene<T> MutateGene(Gene<T> p_gene)
        {
            if (p_gene is Gene<decimal>)
            {
                (p_gene as Gene<decimal>).set_data((decimal) m_random.NextDouble());
            }
            else if (p_gene is Gene<int>)
            {
                (p_gene as Gene<int>).set_data(m_random.Next());
            }
            else
            {
                Debug.Assert(false);
            }

            return p_gene;
        }
    }
}
