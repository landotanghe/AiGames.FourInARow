using FourInARow.Visualisation.Controller;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Visualisation.Gui.Panels;

namespace FourInARow.Visualisation.Forms
{
    public class FourInARowForm : Form
    {
        public BoardPanel Board { get; set;  }
        public EditPanel Edit { get; set; }
        public StatisticsPanel Statistics { get; set; }
        private Label Result;

        private FourInARowFormController controller;

        public FourInARowForm(FourInARowFormController controller)
        {
            this.controller = controller;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            SuspendLayout();

            ClientSize = new Size(1200, 800);

            Board = new BoardPanel(controller);
            Edit = new EditPanel(controller);
            Statistics = new StatisticsPanel(controller);

            Result = new Label();
            Result.Height = 100;
            Result.Font = new Font(Result.Font.FontFamily, 15);
            
            var MultiPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                Dock = DockStyle.Fill
            };
            MultiPanel.Controls.Add(Result);
            MultiPanel.Controls.Add(Board);
            MultiPanel.Controls.Add(Edit);
            
            var TotalPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                Dock = DockStyle.Fill
            };
            TotalPanel.Controls.Add(MultiPanel);
            TotalPanel.Controls.Add(Statistics);
            
            Controls.Add(MultiPanel);

            Name = "FourInARow";
            ResumeLayout(false);
        }

        internal void PlayerWins(int player)
        {
            Result.Text = $"Player {player} wins";
        }

        internal void WarnDraw()
        {
            Result.Text = "Game is a draw";
        }

        internal void ClearWinMessage()
        {
            Result.Text = "";
        }
    }
}
