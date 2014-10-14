using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks
{
    /// <summary>
    /// Backpropagation functions that use the delta rule
    /// </summary>
    static class Backprop
    {
        //hardcode the learnrate
        private static double learnrate = .1;

        //hardcode the momentum value
        private static double momentummod = .5;

        //calculated the error of any exposed value or value with a target
        public static double CalcExposedError(double outB, double targetB, NonLinear.nonlinPrime txprime)
        {
            return txprime(outB) * (targetB - outB);
        }

        //calculate the weight change based on error and output of the conneting node
        //basic delta rule dw(t)=n*error*input 
        public static double WeightChange(double errorB, double outA)
        {
            return learnrate * (errorB * outA);
        }

        /// <summary>
        /// calculate the weight change with momentum based on error and output of the conneting node and previous results
        /// will need to set the dW as an output to do momentum learning
        /// momunetum delta rule: dw(t)=(1-a)*n*error*input+a*dw(t-1)
        /// removing the 1-a for now
        /// if a = 0, its the delta rule
        /// </summary>
        /// <param name="errorB"></param>
        /// <param name="outA"></param>
        /// <param name="weightkpast"></param>
        /// <param name="a">momentum</param>
        /// <returns></returns>
        public static double WeightChangeMomentum(double errorB, double outA, double weightkpast, double a)
        {
            return learnrate * errorB * outA + a * weightkpast;
        }

        //callulate the error of a hidden node based on the error of forward connected nodes
        public static double CalcHiddenError(double outA, double[] errorforward, double[] weightforward, NonLinear.nonlinPrime txprime)
        {
            double sum = 0;

            //might be the problem right here
            for (int i = 0; i < errorforward.Length; i++)
            {
                sum += errorforward[i] * weightforward[i];
            }

            return txprime(outA) * sum;
        }
    }

    /// <summary>
    /// Contains functions and delgates for non-linear functions and their first derivative
    /// </summary>
    public static class NonLinear
    {
        public delegate double nonlinPrime(double x);
        public delegate double nonlin(double x);
        public delegate void selectfunct(ref nonlin fun, ref nonlinPrime funprime);

        //variables to adjust the sigmoid function
        private static double a = 1;
        private static double b = 0;

        public static void SetSigmoid(ref nonlin fun, ref nonlinPrime funprime)
        {
            fun = Sigmoid;
            funprime = SigmoidPrime;
        }

        //Log sigmoid function for creating a non linearity in the memories
        public static double Sigmoid(double x)
        {

            return 1 / (1 + Math.Exp(-x));

        }

        //Log Sigmoid first dirivative for error calculation
        public static double SigmoidPrime(double x)
        {
            return x * (1 - x);
        }

        public static double SteepSigmoid(double x)
        {
            return 1 / (1 + Math.Exp(-a * x + b));
        }

        public static void SetTansig(ref nonlin fun, ref nonlinPrime funprime)
        {
            fun = Tansigmoid;
            funprime = Tansigmoid;
        }

        //Hyperbolic Tangent Sigmoid function for creating a non linearity in the memories where 0 is the point of inflection
        public static double Tansigmoid(double x)
        {
            return (Math.Exp(x) - Math.Exp(-x)) / (Math.Exp(x) + Math.Exp(-x));
        }

        public static double TansigmoidPrime(double x)
        {
            return 1 - Math.Pow(Math.Exp(x) - Math.Exp(-x), 2) / Math.Pow(Math.Exp(x) + Math.Exp(-x), 2);
        }
    }
}
