using FourInARow;
using FourInARow.Strategies;
using FourInARow.Strategies.Evaluators;
using FourInARow.Visualisation.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visualisation.Gui.Controller
{
    public class Bot
    {
        private int _me;
        private FourInARowFormController _controller;
        private IStrategy _strategy;

        public Bot(FourInARowFormController controller, int player)
        {
            _controller = controller;
            _me = player;
            _strategy = new AlphaBetaStrategyWithOrdering(new ImmediateEvaluatorOnlyEmptyCells(), 4);
        }

        public void Think()
        {
            _controller.DisablePlayerMoves();
            int column = _strategy.NextMove(new Board(_controller.GetBoard(), _me, 1 + (_me % 2)));
            int row = _controller.DropDisc(column);
            _controller.EnableValidPlayerMoves();
        }
    }
}
