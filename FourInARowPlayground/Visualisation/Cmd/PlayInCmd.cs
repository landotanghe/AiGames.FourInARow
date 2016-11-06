using Algorithm.LocalGames;
using FourInARow.Strategies;
using FourInARow.Strategies.Evaluators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourInARow.Visualisation
{
    public class PlayInCmd
    {
        private const string VerboseArg = "-v";
        private const string GamesToPlayArg = "-gtp:";
        private const int GameGroupSize = 5;
        
        private static int GamesToPlay;

        public void PlayBotGames(string[] args)
        {
            var localGame = new LocalGame
            {
                Verbose = args.Contains(VerboseArg)
            };

            string gamesToPlayString = args.Where(gtp => gtp.StartsWith(GamesToPlayArg))
                .Select(gtp => gtp.Substring(GamesToPlayArg.Length)).FirstOrDefault();
            GamesToPlay = gamesToPlayString == null ? 100 : int.Parse(gamesToPlayString);

            int[] results = { 0, 0, 0};
            
            var player1 = new Player
            {
                Strategy = new AlphaBetaStrategy(new SimpleEvaluator(), 3)
            };

            var player2 = new Player
            {
                Strategy = new AlphaBetaStrategy(new SimpleEvaluator(), 4)
            };

            for (int gamesPlayed = 0; gamesPlayed < GamesToPlay; gamesPlayed++)
            {
                if(gamesPlayed % GameGroupSize == 0)
                {
                    Console.WriteLine($"Playing next {GameGroupSize} games now @ game{gamesPlayed}");
                }
                int result = localGame.PlayGame(player1, player2);
                results[result]++;
            }

            Console.WriteLine($"Draws:         {results[0]}");
            Console.WriteLine($"Player 1 wins(avg t= {player1.TimesPerMove.Average()}): {results[1]}");
            Console.WriteLine($"Player 2 wins(avg t= {player2.TimesPerMove.Average()}: {results[2]}");
            Console.WriteLine("All games played...");
        }

    }
}
