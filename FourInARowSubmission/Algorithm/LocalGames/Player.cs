using FourInARow;
using FourInARow.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.LocalGames
{
    public class Player
    {
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
}
