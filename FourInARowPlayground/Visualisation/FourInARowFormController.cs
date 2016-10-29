using System.Linq;
using System.Windows.Forms;

namespace FourInARow.Visualisation
{
    public class FourInARowFormController
    {
        private Board Board;
        private FourInARowForm Form;

        private int CurrentPlayer = 1;

        public FourInARowFormController()
        {
        }

        public void Init()
        {
            Board = new Board(6, 7);
            Form = new FourInARowForm(this);

            Application.Run(Form);
        }

        public void DropDisc(int column)
        {
            if (Board.GetOpenColumns().Contains(column))
            {
                int row = Board.DropDisc(column, CurrentPlayer);
                Form.Board.UpdateCellColor(row, column, CurrentPlayer);

                SwitchPlayer();
            }
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
