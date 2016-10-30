using FourInARow.Visualisation.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visualisation.Gui.Panels
{
    public class StatisticsPanel : Panel
    {
        private const int ListHeight = 800;
        private const int ListWidth = 75;
        private FourInARowFormController _controller;
        private TextBox[] _playerTimes;

        public StatisticsPanel(FourInARowFormController controller)
        {
            _controller = controller;
            _playerTimes = new TextBox[2];
            Width = ListWidth * 2;
            Height = ListHeight;

            _playerTimes[0] = new TextBox
            {
                Width = ListWidth,
                Height = ListHeight
            };
            _playerTimes[1] = new TextBox
            {
                Width = ListWidth,
                Height = ListHeight,
                Left = ListWidth
            };
        }

        public void UpdatePlayerTimes(int playerIndex, List<long> times)
        {
            _playerTimes[playerIndex].Text = string.Join("\n", times);
        }        
    }
}
