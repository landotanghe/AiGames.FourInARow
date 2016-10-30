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

        private System.Diagnostics.Stopwatch StopWatch;
        private List<Move> movesMade;

        private Board Board;
        private FourInARowForm Form;

        private string[] PlayerOptions;
        private Bot[] Bots;

        private int CurrentPlayer = 1;

        public bool IsGameEnded { get; private set; }

        private class Move
        {
            public int Column { get; set; }
            public long MilisNeeded { get; set; }
        }

        public FourInARowFormController()
        {
            movesMade = new List<Move>();
        }


        public void EnableValidPlayerMoves()
        {
            if(!IsGameEnded)
            {
                Form.Board.EnablePlayerMoves(Board.GetOpenColumns());
            }
        }

        public void DisablePlayerMoves()
        {
            Form.Board.DisablePlayerMoves();
        }

        public void Undo()
        {
            var lastMove = movesMade.Last();
            movesMade.Remove(lastMove);
            var row = Board.RemoveTopDisc(lastMove.Column);
            Form.Board.UpdateCellColor(row, lastMove.Column, 0);

            SwitchPlayer();

            if (!movesMade.Any())
            {
                Form.Edit.DisableUndo();
            }

            IsGameEnded = false;
            Form.ClearWinMessage();
            EnableValidPlayerMoves();
            
            StopWatch = System.Diagnostics.Stopwatch.StartNew();
        }

        public void Init()
        {
            PlayerOptions = new string[] { HumanOption, BotOption };
            Bots = new Bot[2];
            Bots[1] = new Bot(this, 2);

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

            if (!IsGameEnded)
            {
                return;
            }

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
                Bots[player - 1] = new Bot(this, player);
            }
            else if(option == HumanOption)
            {
                Bots[player - 1] = null;
            }
            else
            {
                throw new System.Exception();
            }

            if(!IsGameEnded)
                KickStartBot();
        }
        
        public int DropDisc(int column)
        {
            if (!Board.GetOpenColumns().Contains(column))
            {
                return -1;
            }

            int row = Board.DropDisc(column, CurrentPlayer);
            Form.Board.UpdateCellColor(row, column, CurrentPlayer);
            if(row == 0)
            {
                Form.Board.DisableColumn(column);
            }
            
            var millisNeeded = StopWatch == null ? 0 : StopWatch.ElapsedMilliseconds;
            movesMade.Add(new Move {
                Column = column,
                MilisNeeded = millisNeeded
            });
            UpdatePlayerTimes();
            StopWatch = System.Diagnostics.Stopwatch.StartNew();

            SwitchPlayer();
            Form.Edit.EnableUndo();
            CheckWin(row, column);

            if (!IsGameEnded)
                KickStartBot();

            return row;
        }

        private void UpdatePlayerTimes()
        {
            int playerIndex = CurrentPlayer - 1;
            List<long> times = new List<long>();
            for (int i = 0; i < movesMade.Count; i++)
            {
                if(i % 2 == playerIndex) {
                    var move = movesMade[i];
                    times.Add(move.MilisNeeded);
                }
            }
            Form.Statistics.UpdatePlayerTimes(playerIndex, times);
        }

        public void KickStartBot()
        {
            Bots[CurrentPlayer - 1]?.Think();
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
