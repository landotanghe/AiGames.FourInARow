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
        private class Player {
            public int Id { get; set; }
            public IStrategy Strategy { get; set; }

            public List<long> TimesPerMove { get; private set; }

            public Player()
            {
                TimesPerMove = new List<long>();
            }
            
            public int PickColumn(Board board, int opponentId)
            {
                var boardCopy = new Board(board, Id, opponentId);
                var timer = new System.Diagnostics.Stopwatch();
                timer.Start();
                var column = Strategy.NextMove(boardCopy);
                timer.Stop();
                TimesPerMove.Add(timer.ElapsedMilliseconds);
                return column;
            }
        }

        private const string VerboseArg = "-v";
        private const string GamesToPlayArg = "-gtp:";
        private const int GameGroupSize = 5;

        private static bool Verbose;
        private static int GamesToPlay;

        public void PlayBotGames(string[] args)
        {
            Verbose = args.Contains(VerboseArg);
            string gamesToPlayString = args.Where(gtp => gtp.StartsWith(GamesToPlayArg))
                .Select(gtp => gtp.Substring(GamesToPlayArg.Length)).FirstOrDefault();
            GamesToPlay = gamesToPlayString == null ? 100 : int.Parse(gamesToPlayString);

            int[] results = { 0, 0, 0};
            
            var player1 = new Player
            {
                Id = 1,
                Strategy = new AlphaBetaStrategy(new SimpleEvaluator(), 3)
            };

            var player2 = new Player
            {
                Id = 2,
                Strategy = new AlphaBetaStrategy(new SimpleEvaluator(), 4)
            };

            for (int gamesPlayed = 0; gamesPlayed < GamesToPlay; gamesPlayed++)
            {
                if(gamesPlayed % GameGroupSize == 0)
                {
                    Console.WriteLine($"Playing next {GameGroupSize} games now @ game{gamesPlayed}");
                }
                int result = PlayGame(player1, player2);
                results[result]++;
            }

            Console.WriteLine($"Draws:         {results[0]}");
            Console.WriteLine($"Player 1 wins(avg t= {player1.TimesPerMove.Average()}): {results[1]}");
            Console.WriteLine($"Player 2 wins(avg t= {player2.TimesPerMove.Average()}: {results[2]}");
            Console.WriteLine("All games played...");
        }

        private int PlayGame(Player player1, Player player2)
        {
            var board = new Board(6, 7);
            for (int turn = 1; turn <= 21; turn++)
            {
                PrintVerboseText($"Turn: {turn}");

                if (Play(player1, board, player2))
                {
                    var strategyUsed = player1.Strategy.GetType().ToString();
                    PrintVerboseText($"Player 1 wins with {strategyUsed}");
                    return 1;
                }

                if (Play(player2, board, player1))
                {
                    var strategyUsed = player2.Strategy.GetType().ToString();
                    PrintVerboseText($"Player 2 wins with {strategyUsed}");
                    return 2;
                }
            }
            PrintVerboseText("Draw");
            return 0;
        }

        private void PrintVerboseText(string text)
        {
            if (Verbose)
                Console.WriteLine(text);
        }

        private bool Play(Player player, Board board, Player opponent)
        {
            int column = player.PickColumn(board, opponent.Id);
            int row = board.DropDisc(column, player.Id);
            PrintVerboseText($"Player {player.Id} played in column {column} (row {row})");

            PrintVerboseText(board.ToString());

            return board.HasFourInARow(player.Id, row, column);
        }
    }
}
