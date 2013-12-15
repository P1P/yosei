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
        /// Copy constructor, does a copy of the given gene's data
        /// Data held by genes are guaranteed to be structs, hence shallow copy
        /// </summary>
        /// <param name="p_other"></param>
        public Gene(Gene<T> p_other)
        {
            m_data = p_other.m_data;
        }

        /// <summary>
        /// Retrieves this gene's data
        /// </summary>
        /// <returns>The gene data</returns>
        public T GetData()
        {
            return m_data;
        }

        /// <summary>
        /// Sets the gene data
        /// </summary>
        /// <param name="p_data">The new gene data</param>
        public void SetData(T p_data)
        {
            m_data = p_data;
        }

        /// <summary>
        /// Returns the ToString() of this gene's data
        /// </summary>
        /// <returns>A string representation of the gene's data</returns>
        public override string ToString()
        {
            return String.Format("{0:000.00}", m_data);
        }

        /// <summary>
        /// Equals override. Based on the Equals of T 
        /// </summary>
        /// <param name="obj">The Gene of the same type to compare with</param>
        /// <returns>Boolean depending on the Equals of T</returns>
        public override bool Equals(object obj)
        {
            return m_data.Equals(((Gene<T>)obj).m_data);
        }

        /// <summary>
        /// GetHashCode override. Based on the GetHashCode of T
        /// </summary>
        /// <returns>int depending on the GetHashCode of T</returns>
        public override int GetHashCode()
        {
            return m_data.GetHashCode();
        }
    }
}