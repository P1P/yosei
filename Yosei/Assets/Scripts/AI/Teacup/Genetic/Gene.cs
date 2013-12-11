using UnityEngine;
using System.Collections;

namespace Teacup.Genetic
{
    public class Gene<T> where T : struct
    {
        private T m_data;

        public Gene(T p_data)
        {
            m_data = p_data;
        }

        public T get_data()
        {
            return m_data;
        }

        public void set_data(T p_data)
        {
            m_data = p_data;
        }

        public string ToString()
        {
            return m_data.ToString();
        }
    }
}