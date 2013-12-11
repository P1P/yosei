using UnityEngine;
using System.Collections;

namespace givemeaname.Genetic
{
    public class Gene<T>
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
    }
}