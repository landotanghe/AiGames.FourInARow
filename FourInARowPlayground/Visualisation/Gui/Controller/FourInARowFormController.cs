using FourInARow.Strategies;
using FourInARow.Strategies.Evaluators;
using System.Linq;
using System.Windows.Forms;
using System;
using FourInARow.Visualisation.Forms;
using Visualisation.Gui.Controller;
using System.Collections.Generic;

namespace FourInARow.Visualisation.Controller
{
    public class FourInARowFormController
    {
        private const string HumanOption = "Human";
        private const string BotOption = "Bot";
        private Stack<int> movesMade;

        private Board Board;
        private FourInARowForm Form;

        private string[] PlayerOptions;
        private Bot[] Bots;

        private int CurrentPlayer = 1;

        public bool IsGameEnded { get; private set; }

        public FourInARowFormController()
        {
            movesMade = new Stack<int>();
        }


        public void EnableValidPlayerMoves()
        {
            if(IsGameEnded)
            {
                Form.Board.EnablePlayerMoves(Board.GetOpenColumns());
            }
        }

        public void DisablePlayerMoves()
        {
            Form.Board.DisablePlayerMoves();
        }

        internal void Undo()
        {
            var lastMove = movesMade.Pop();
            Board.RemoveTopDisc(lastMove);
            SwitchPlayer();

            if (!movesMade.Any())
            {
                Form.Edit.DisableUndo();
            }
        }

        public void Init()
        {
            PlayerOptions = new string[] { HumanOption, BotOption };
            Bots = new Bot[3];
            Bots[2] = new Bot(this, 2);

            Board = new Board(6, 7);
            Form = new FourInARowForm(this);

            Application.Run(Form);
        }

        public void CheckWin(int row, int column)
        {
            var IsDraw = !Board.GetOpenColumns().Any();
            var Player1Wins = Board.HasFourInARow(1, row, column);
            var Player2Wins = Board.HasFourInARow(2, row, column);
            IsGameEnded = IsDraw || Player1Wins || Player2Wins;
            if (IsDraw)
            {
                Form.WarnDraw();
            }else if (Player1Wins)
            {
                Form.PlayerWins(1);
            }
            else if (Player2Wins)
            {
                Form.PlayerWins(2);
            }


            DisablePlayerMoves();
        }

        public void PickPlayerOption(int player, string option)
        {
            if(option == BotOption)
            {
                Bots[player] = new Bot(this, player);
            }
            else if(option == HumanOption)
            {
                Bots[player] = null;
            }
            else
            {
                throw new System.Exception();
            }

            if(!IsGameEnded)
                Bots[CurrentPlayer]?.Think();
        }
        
        public int DropDisc(int column)
        {
            int row = -1;
            if (Board.GetOpenColumns().Contains(column))
            {
                row = Board.DropDisc(column, CurrentPlayer);
                Form.Board.UpdateCellColor(row, column, CurrentPlayer);
                if(row == 0)
                {
                    Form.Board.DisableColumn(column);
                }

                SwitchPlayer();
            }

            if (!IsGameEnded)
                Bots[CurrentPlayer]?.Think();

            movesMade.Push(column);
            Form.Edit.EnableUndo();

            return row;
        }

        public void KickStartBot()
        {
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
