using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyMath
{
    /// <summary>
    /// Static Class that contains functions for polynomial math operations
    /// </summary>
    public static class Polymath
    {
        /// <summary>
        /// Multiplies two polynomials together and simplifies it
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static Polynomial PolyMultiply(Polynomial first, Polynomial second)
        {
            var tempPoly = new Polynomial();

            //multiply each term in the first poly by each term in the second poly
            foreach (var term1 in first.polynomial)
            {
                foreach (var term2 in second.polynomial)
                {
                    tempPoly.polynomial.Add(new PolyTerm(term1.coeff * term2.coeff, term1.exponent + term2.exponent));
                }
            }

            var finalPoly = new Polynomial();

            //add the coefficients of like exponent terms
            foreach (var grouping in tempPoly.polynomial.GroupBy(n => n.exponent))
            {
                var tempterm = new PolyTerm(0, grouping.Key);
                foreach (var component in grouping)
                {
                    tempterm.coeff += component.coeff;
                }

                finalPoly.polynomial.Add(tempterm);
            }

            return finalPoly;
        }

        /// <summary>
        /// Builds a polynomail that represents a step function
        /// </summary>
        /// <param name="magnitude">set to 1 for unit</param>
        /// <param name="delay">set to 0 for no delay</param>
        /// <returns></returns>
        public static PolyExpression StepFunction(double magnitude, int delay)
        {
            var tempNumer = new Polynomial();
            tempNumer.AddTerm(magnitude, delay);

            var tempDenom = new Polynomial();
            tempDenom.AddTerm(1, 0);
            tempDenom.AddTerm(-1, 1);

            return new PolyExpression(tempNumer, tempDenom);
        }
    }
    /// <summary>
    /// holds one term for symbolic polynomial math
    /// </summary>
    public class PolyTerm
    {
        public double coeff;
        public int exponent;

        /// <summary>
        /// Constructor for an empty PolyTerm
        /// </summary>
        public PolyTerm()
        { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="coeff">Coefficient</param>
        /// <param name="exponent">Power of exponent</param>
        public PolyTerm(double coeff, int exponent)
        {
            this.coeff = coeff;
            this.exponent = exponent;
        }
    }

    /// <summary>
    /// Holds one polynomial as a numerator and one as a denomenator
    /// </summary>
    public class PolyExpression
    {
        public Polynomial numerator;
        public Polynomial denomenator;

        /// <summary>
        /// Constructor to create and empty expression
        /// </summary>
        public PolyExpression()
        {
            numerator = new Polynomial();
            denomenator = new Polynomial();
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="numer">Polynomial expression for numerator</param>
        /// <param name="denom">Polynomail expression for denomenator</param>
        public PolyExpression(Polynomial numer, Polynomial denom)
        {
            numerator = numer;
            denomenator = denom;
        }

        /// <summary>
        /// Returns the largets exponent power
        /// </summary>
        /// <returns></returns>
        public double MaxPower()
        {
            return Math.Max(numerator.polynomial.Max(n => n.exponent), denomenator.polynomial.Max(n => n.exponent));
        }
    }

    /// <summary>
    /// Represents a collectional of polynomail terms  for a single polynomial expression
    /// </summary>
    public class Polynomial
    {
        public List<PolyTerm> polynomial;

        /// <summary>
        /// Basic constructor to produce and empty polynomial
        /// </summary>
        public Polynomial()
        {
            polynomial = new List<PolyTerm>();
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="polynomial">Takes a list of PolyTerms</param>
        public Polynomial(List<PolyTerm> polynomial)
        {
            this.polynomial = polynomial;
        }

        /// <summary>
        /// Function to add a single term to a polynomial expression
        /// </summary>
        /// <param name="coeff">Coefficienct</param>
        /// <param name="exponent">Power of the exponent</param>
        public void AddTerm(double coeff, int exponent)
        {
            polynomial.Add(new PolyTerm(coeff, exponent));
        }
    }
}
