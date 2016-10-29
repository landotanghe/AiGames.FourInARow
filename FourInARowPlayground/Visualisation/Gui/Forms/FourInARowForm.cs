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

            var MultiPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown
            };
            MultiPanel.Dock = DockStyle.Fill;
            MultiPanel.Controls.Add(Result);
            MultiPanel.Controls.Add(Board);
            MultiPanel.Controls.Add(Edit);

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
    }
}
