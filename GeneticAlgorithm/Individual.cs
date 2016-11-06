using Algorithm.LocalGames;
using FourInARow.Strategies;
using FourInARow.Strategies.Evaluators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm
{
    public class Individual
    {
        private const double MutationRate = 0.015;
        private static Random random = new Random();

        public Individual()
        {
            WeightsEvenPlayer = new List<double>(6);
            WeightsOddPlayer = new List<double>(6);

            InitializeWeights(WeightsEvenPlayer);
            InitializeWeights(WeightsOddPlayer);
        }

        public Individual(Individual mother)
        {
            WeightsEvenPlayer = mother.WeightsEvenPlayer.ToList();
            WeightsOddPlayer = mother.WeightsOddPlayer.ToList();
        }

        private void InitializeWeights(List<double> weights)
        {
            for (int i = 0; i < 6; i++)
            {
                weights.Add(random.NextDouble() * 10);
            }
        }

        public List<double> WeightsEvenPlayer { get; set; }
        public List<double> WeightsOddPlayer { get; set; }

        public Player Player()
        {
            var evaluator = new ImmediateEvaluatorOnlyEmptyCells(WeightsEvenPlayer.ToArray(), WeightsOddPlayer.ToArray());
            var strategy = new AlphaBetaStrategyWithOrdering(evaluator, 3);
            return new Player
            {
                Strategy = strategy
            };
        }

        internal static Individual CrossOver(Individual mother, Individual father)
        {
            var child = new Individual(mother);

            bool isSwitchEvenWeights = random.NextDouble() < 0.5;
            int crossOverPlace = random.Next(6);

            if (isSwitchEvenWeights)
            {
                for(int i = crossOverPlace; i < 6; i++)
                {
                    child.WeightsEvenPlayer[i] = father.WeightsEvenPlayer[i];
                }
            }
            else
            {
                for (int i = crossOverPlace; i < 6; i++)
                {
                    child.WeightsOddPlayer[i] = father.WeightsOddPlayer[i];
                }
            }

            return child;
        }

        internal void Mutate()
        {
            MutateWeights(WeightsEvenPlayer);
            MutateWeights(WeightsOddPlayer);
        }

        public void MutateWeights(List<double> weights)
        {
            for (int i = 0; i < 6; i++)
            {
                if (random.NextDouble() < MutationRate)
                {
                    weights[i] = random.NextDouble() * 10;
                }
            }
        }
    }
}
