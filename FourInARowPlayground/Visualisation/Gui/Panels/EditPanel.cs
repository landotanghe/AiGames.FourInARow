using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FourInARow.Visualisation.Controller;
using System.Drawing;

namespace Visualisation.Gui.Panels
{
    public class EditPanel : FlowLayoutPanel
    {
        private FourInARowFormController _controller;

        private Button undoButton;
        private Button kickStartButton;

        public EditPanel(FourInARowFormController controller)
        {
            _controller = controller;

            FlowDirection = FlowDirection.LeftToRight;

            kickStartButton = new Button();
            kickStartButton.Click += new EventHandler(kickStartButton_Clicked);
            kickStartButton.Text = "Force bot";

            undoButton = new Button();
            undoButton.Enabled = false;
            undoButton.Click += new EventHandler(undoButton_Clicked);
            undoButton.Text = "Undo";

            Controls.Add(undoButton);
        }

        private void undoButton_Clicked(object sender, EventArgs e)
        {
            _controller.Undo();
        }

        private void kickStartButton_Clicked(object sender, EventArgs e)
        {
            _controller.KickStartBot();
        }

        public void DisableUndo()
        {
            undoButton.Enabled = false;
        }

        public void EnableUndo()
        {
            undoButton.Enabled = true;
        }
    }
}
