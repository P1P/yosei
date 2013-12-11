using UnityEngine;
using System.Collections.Generic;

namespace Teacup.Genetic
{
    public class Chromosome<T> where T : struct
    {
        private LinkedList<Gene<T>> m_lst_genes;

        public Chromosome()
        {
            m_lst_genes = new LinkedList<Gene<T>>();
        }

        public Chromosome(LinkedList<Gene<T>> p_lst_genes)
        {
            m_lst_genes = p_lst_genes;
        }

        /// <summary>
        /// Performs a chromosomal crossover
        /// Swaps will happen after the junction point
        /// Requires both chromosomes to have the same length
        /// </summary>
        /// <param name="p_chr_1">The first chromosome</param>
        /// <param name="p_chr_2">The second chromosome</param>
        /// <param name="p_junction_index">The point of junction. Default parameter or -1 for random index</param>
        public static void CrossOver(Chromosome<T> p_chr_1, Chromosome<T> p_chr_2, int p_junction_index)
        {
            int chr_1_length = p_chr_1.m_lst_genes.Count;

            // Requires same length on both chromosomes
            System.Diagnostics.Debug.Assert(chr_1_length == p_chr_2.m_lst_genes.Count);

            // Random junction
            if (p_junction_index < 0)
            {
                p_junction_index = Random.Range(0, chr_1_length);
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

        /// <summary>
        /// Mutates this chromosome by changing a gene at either a given or randomly selected index
        /// </summary>
        /// <param name="p_new_value">The new value the gene should take</param>
        /// <param name="p_index">The index of the mutation. Default parameter or -1 for random index</param>
        public void Mutate(Gene<T> p_new_value, int p_index = -1)
        {
            // Random mutation location
            if (p_index < 0)
            {
                p_index = Random.Range(0, this.m_lst_genes.Count);
            }

            LinkedListNode<Gene<T>> node = this.m_lst_genes.First;
            for (int i = 0; i < this.m_lst_genes.Count; ++i)
            {
                if (i == p_index)
                {
                    node.Value = p_new_value;
                    break;
                }
                node = node.Next;
            }
        }

        public string ToString()
        {
            string str = "|";

            foreach (Gene<T> gene in m_lst_genes)
            {
                str += gene.ToString() + "|";
            }

            return str;
        }
    }
}
