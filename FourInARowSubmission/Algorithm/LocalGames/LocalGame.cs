using FourInARow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.LocalGames
{
    public class LocalGame
    {
        public bool Verbose { get; set; }

        public LocalGame()
        {
            Verbose = false;
        }

        public int PlayGame(Player player1, Player player2)
        {
            player1.Id = 1;
            player2.Id = 2;

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
