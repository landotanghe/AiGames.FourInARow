using Algorithm.LocalGames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    public class GeneticAlgorithm
    {
        private const int EliteSize = 3;

        private List<Individual> Population;
        private Dictionary<Individual, int> Scores;
        private static Random random = new Random();

        public GeneticAlgorithm(int populationSize)
        {
            Population = new List<Individual>(populationSize);
            for(int i=0; i< populationSize; i++)
            {
                Population.Add(new Individual());
            }

            EvaluatePopulation();
        }

        private void EvaluatePopulation()
        {
            Scores = new Dictionary<Individual, int>();
            for(int i = 0; i< Population.Count; i++)
            {
                Scores.Add(Population[i], 0);
            }

            for(int i = 0; i < Population.Count - 1; i++)
            {
                for(int j = i + 1; j < Population.Count; j++)
                {
                    var individual1 = Population[i];
                    var individual2 = Population[2];

                    var game = new LocalGame();
                    int result = game.PlayGame(individual1.Player(), individual2.Player());

                    if(result == 1)
                    {
                        Scores[individual1]++;
                        Scores[individual2]--;
                    }
                    else if(result == 2)
                    {
                        Scores[individual2]++;
                        Scores[individual1]--;
                    }
                }
            }
        }

        public void Evolve(int generationsToEvolve)
        {
            for (int generation = 0; generation < generationsToEvolve; generation++)
            {
                Console.WriteLine($"Generation {generation}...");
                var surivivors = BestIndividuals().Take(Population.Count / 2).ToList();

                Population = NextGeneration(surivivors);
                MutatePopulation();

                EvaluatePopulation();
                Console.WriteLine(String.Join("\n", BestIndividuals()));
            }
        }

        private void MutatePopulation()
        {
            foreach(var individual in Population)
            {
                individual.Mutate();
            }
        }

        private List<Individual> NextGeneration(List<Individual> survivors)
        {
            // Elitism
            var nextGeneration = survivors.Take(EliteSize).ToList();

            // Cross-over
            while (nextGeneration.Count < Population.Count)
            {
                var mother = survivors[random.Next(survivors.Count)];
                var father = survivors[random.Next(survivors.Count)];
                nextGeneration.Add(Individual.CrossOver(mother, father));
            }

            return nextGeneration;
        }

        public Individual GetMostEvolvedIndividual()
        {
            return BestIndividuals()
                .First();
        }

        private IEnumerable<Individual> BestIndividuals()
        {
            return Scores
                        .OrderByDescending(score => score.Value)
                        .Select(score => score.Key);
        }
    }
}
