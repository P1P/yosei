using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teacup.Genetic
{
    /// <summary>
    /// Represents the set of parameters from which the genetic operators
    /// such as crossovers and mutations will happen
    /// </summary>
    public class GeneticOperatorRules
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

        public GeneticOperatorRules(double p_crossover_rate, MUTATION_TYPE p_mutation_type, double p_mutation_rate, decimal p_mutation_delta, decimal p_mutation_lower_bound, decimal p_mutation_upper_bound)
        {
            m_crossover_rate = p_crossover_rate;
            m_mutation_type = p_mutation_type;
            m_mutation_rate = p_mutation_rate;
            m_mutation_delta = p_mutation_delta;
            m_mutation_lower_bound = p_mutation_lower_bound;
            m_mutation_upper_bound = p_mutation_upper_bound;
        }
    }
}
