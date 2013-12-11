using UnityEngine;
using System.Collections.Generic;
using Teacup.Genetic;

public class Brewer : MonoBehaviour {
    private List<Chromosome<float>> m_lst_chromosomes = new List<Chromosome<float>>();

    public List<string> m_lst_str_chromosomes = new List<string>();

    public bool m_mutate;
    public bool m_crossover;
    public int m_index;

	public void Start()
    {
        int nb_chromosomes = 2;
        int nb_genes = 5;

        for (int i = 0; i < nb_chromosomes; ++i)
        {
            LinkedList<Gene<float>> lst_genes = new LinkedList<Gene<float>>();

            for (int j = 0; j < nb_genes; ++j)
            {
                lst_genes.AddLast(new Gene<float>(((int)(Random.value * 100f)) / 100f));
            }

            m_lst_chromosomes.Add(new Chromosome<float>(lst_genes));
        }
	}

    public void Update()
    {
        // TODO: unit testing

        if (m_crossover)
        {
            m_crossover = false;

            Chromosome<float>.CrossOver(m_lst_chromosomes[0], m_lst_chromosomes[1], m_index);
        }

        if (m_mutate)
        {
            m_mutate = false;

            m_lst_chromosomes[0].Mutate(new Gene<float>(((int)(Random.value * 100f)) / 100f), m_index);
        }

        m_lst_str_chromosomes.Clear();
        foreach (Chromosome<float> chr in m_lst_chromosomes)
        {
            m_lst_str_chromosomes.Add(chr.ToString());
        }
	}
}
