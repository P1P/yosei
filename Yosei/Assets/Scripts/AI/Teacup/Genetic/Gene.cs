using System;
using System.Collections.Generic;

namespace Teacup.Genetic
{
    /// <summary>
    /// A gene that contains one unit of genetic data
    /// </summary>
    /// <typeparam name="T">The type of genetic information (struct)</typeparam>
    public class Gene<T> where T : struct
    {
        private T m_data;

        /// <summary>
        /// Initializes the gene with data
        /// </summary>
        /// <param name="p_data">The gene data</param>
        public Gene(T p_data)
        {
            m_data = p_data;
        }

        /// <summary>
        /// Retrievs this gene's data
        /// </summary>
        /// <returns>The gene data</returns>
        public T get_data()
        {
            return m_data;
        }

        /// <summary>
        /// Sets the gene data
        /// </summary>
        /// <param name="p_data">The new gene data</param>
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