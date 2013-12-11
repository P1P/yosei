using UnityEngine;
using System.Collections.Generic;

public class NameFactory : MonoBehaviour
{
    private List<string> m_lst_phonems;
    private float m_whitespace_chance = 0.3f;
    private int m_min_phonems = 2;
    private int m_max_phonems = 4;

    public void Awake()
    {
        m_lst_phonems = new List<string>(new string[] {
            "fal", "li", "ly", "ily" , "ya", "tor", "ti", "ni", "ta", "li", "su", "ku", "ris", "phor", "ni"
        });
    }

    public string GiveMeAName()
    {
        string name = "";

        int nb_phonems = Random.Range(m_min_phonems, m_max_phonems);

        for (int i = 0; i < nb_phonems; ++i)
        {
            // Select a random phonem
            char[] phonem = m_lst_phonems[Random.Range(0, m_lst_phonems.Count)].ToCharArray();

            // Capitalize on new word, or when a random whitespace separation occurs
            if (name.Length < 1)
            {
                // First word, kappatalize
                phonem[0] = char.ToUpper(phonem[0]);
            }
            else if (Random.value < m_whitespace_chance)
            {
                // Random separation, add a whitespace before the phonem and capitalize
                char[] corrected_phonem = new char[phonem.Length + 1];
                corrected_phonem[0] = ' ';

                for (int j = 0; j < phonem.Length; ++j)
                {
                    corrected_phonem[j + 1] = (j == 0) ? char.ToUpper(phonem[j]) : phonem[j]; // First character is capitalized
                }

                phonem = corrected_phonem;
            }

            // Add curent phonem to name buffer
            foreach (char c in phonem)
            {
                name += c;
            }
        }

        return name;
    }
}
