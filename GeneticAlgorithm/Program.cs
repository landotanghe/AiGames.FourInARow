using FourInARow.Strategies.Evaluators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    public class Program
    {
        public const int GenerationsToEvolve = 50;
        public const int PopulationSize = 20;
        public static void Main(string[] args)
        {
            var geneticAlgorithm = new GeneticAlgorithm(PopulationSize);
            geneticAlgorithm.Evolve(GenerationsToEvolve);
            var solution = geneticAlgorithm.GetMostEvolvedIndividual();
            Console.WriteLine(String.Join(", ", solution));
        }
    }
}
