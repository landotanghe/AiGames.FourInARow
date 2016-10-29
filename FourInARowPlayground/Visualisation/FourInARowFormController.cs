using FourInARow.Strategies;
using FourInARow.Strategies.Evaluators;
using System.Linq;
using System.Windows.Forms;
using System;

namespace FourInARow.Visualisation
{
    public class FourInARowFormController
    {
        private const string HumanOption = "Human";
        private const string BotOption = "Bot";

        private Board Board;
        private FourInARowForm Form;

        private string[] PlayerOptions;
        private Bot[] Bots;

        private int CurrentPlayer = 1;

        public FourInARowFormController()
        {
        }

        private class Bot
        {
            private FourInARowFormController _controller;
            private bool Busy { get; set; }
            private IStrategy _strategy;

            public Bot(FourInARowFormController controller)
            {
                _controller = controller;
                _strategy = new AlphaBetaStrategyWithOrdering(new ImmediateEvaluator(), 2);
            }

            public void Think()
            {
                _controller.DisablePlayerMoves();
                _controller.EnablePlayerMoves();
            }
        }

        private void EnablePlayerMoves()
        {
            Form.Board.EnablePlayerMoves(Board.GetOpenColumns());
        }

        private void DisablePlayerMoves()
        {
            Form.Board.DisablePlayerMoves();
        }

        public void Init()
        {
            PlayerOptions = new string[] { HumanOption, BotOption };
            Bots = new Bot[3];
            Bots[2] = new Bot(this);

            Board = new Board(6, 7);
            Form = new FourInARowForm(this);

            Application.Run(Form);
        }

        public void PickPlayerOption(int player, string option)
        {
            if(option == BotOption)
            {
                Bots[player] = new Bot(this);
            }
            else if(option == HumanOption)
            {
                Bots[player] = null;
            }
            else
            {
                throw new System.Exception();
            }
            Bots[CurrentPlayer]?.Think();
        }
        
        public void DropDisc(int column)
        {
            if (Board.GetOpenColumns().Contains(column) && Bots[CurrentPlayer] == null)
            {
                int row = Board.DropDisc(column, CurrentPlayer);
                Form.Board.UpdateCellColor(row, column, CurrentPlayer);
                if(row == 0)
                {
                    Form.Board.DisableColumn(column);
                }

                SwitchPlayer();
            }

            Bots[CurrentPlayer]?.Think();
        }

        private void SwitchPlayer()
        {
            CurrentPlayer = 1 + (CurrentPlayer % 2);
        }

        public Board GetBoard()
        {
            return Board;
        }
    }
}
