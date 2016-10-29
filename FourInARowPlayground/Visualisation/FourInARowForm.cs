using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FourInARow.Visualisation
{
    public class FourInARowForm : Form
    {
        public BoardPanel Board { get; set;  }
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
            Controls.Add(Board);
            Name = "FourInARow";
            ResumeLayout(false);

        }
        

        public class Cell : Label
        {
            private int _row;
            private int _column;
            public const int CellRadius = 40;
            public Color DiscColor;

            public Cell(int row, int column, Color color)
            {
                _row = row;
                _column = column;

                Width = CellRadius;
                Height = CellRadius;
                Left = column * CellRadius;
                Top = row * CellRadius;
                DiscColor = color;
                SetStyle(ControlStyles.UserPaint, true);
            }

            public void ChangeColor(Color color)
            {
                Console.WriteLine($"change color {_row}.{_column} to {color}");
                DiscColor = color;
                Invalidate();
            }

            protected override void OnPaint(PaintEventArgs pevent)
            {
                Console.WriteLine($"painting {_row}.{_column}");
                Rectangle rectangle = new Rectangle(4, 4, Size.Width - 8,
                                            Size.Height - 8);

                pevent.Graphics.FillEllipse(new SolidBrush(DiscColor), rectangle);
            }            
        }
                

        public class Column : Button
        {
            private int _column;
            private bool IsPickedColumn = false;
            private FourInARowFormController _controller;

            public Column(int column, int rowCount, FourInARowFormController controller)
            {
                _controller = controller;

                _column = column;
                Left = column * Cell.CellRadius;
                Top = 500;
                Width = Cell.CellRadius;
                Height = Cell.CellRadius * rowCount + 50;
            }

            protected override void OnPaint(PaintEventArgs pevent)
            {
                if (IsPickedColumn)
                {
                    Rectangle rectangle = new Rectangle(0, 0, Size.Width, Size.Height);
                    //pevent.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(10, 0, 0, 0)), rectangle);
                    pevent.Graphics.DrawRectangle(new Pen(Color.Black), rectangle);
                }
            }

            protected override void OnMouseHover(EventArgs e)
            {
                base.OnMouseHover(e);
                IsPickedColumn = true;
                Invalidate();
            }

            protected override void OnMouseLeave(EventArgs e)
            {
                base.OnMouseLeave(e);
                IsPickedColumn = false;
                Invalidate();
            }

            protected override void OnClick(EventArgs e)
            {
                _controller.DropDisc(_column);
            }
        }
        
        public class BoardPanel : Panel
        {
            FourInARowFormController _controller;
            Cell[][] cells;
            Column[] columns;

            public BoardPanel(FourInARowFormController controller)
            {
                Dock = DockStyle.Fill;
                _controller = controller;
                CreateBoardVisualisation(controller.GetBoard());
            }

            private void CreateBoardVisualisation(Board board)
            {
                CreateCells(board);
                CreateClickableColumns(board);
            }
            
            private void CreateCells(Board board)
            {
                cells = new Cell[board.RowCount()][];
                for (int row = 0; row < board.RowCount(); row++)
                {
                    cells[row] = new Cell[board.ColumnCount()];
                    for (int column = 0; column < board.ColumnCount(); column++)
                    {
                        var cell = new Cell(row, column, PlayerColor(board.GetPlayer(row, column)));

                        cells[row][column] = cell;
                        
                        Controls.Add(cell);
                    }
                }
            }

            private void CreateClickableColumns(Board board)
            {
                columns = new Column[board.ColumnCount()];
                for (int column = 0; column < board.ColumnCount(); column++)
                {
                    var columnButton = new Column(column, board.RowCount(), _controller);
                    columns[column] = columnButton;
                    columnButton.BringToFront();
                    columnButton.Invalidate();
                    
                    Controls.Add(columnButton);
                    columnButton.BringToFront();
                }
            }


            public void EnablePlayerMoves(IEnumerable<int> openColumns)
            {
                foreach (var i in openColumns)
                {
                    var column = columns[i];
                    column.Enabled = true;
                    column.Invalidate();
                }
            }

            public void DisableColumn(int columnIndex)
            {
                var column = columns[columnIndex];
                column.Enabled = false;
                column.Invalidate();
            }

            public void DisablePlayerMoves()
            {
                foreach (var column in columns)
                {
                    column.Enabled = false;
                    column.Invalidate();
                }
            }

            public void UpdateCellColor(int row, int column, int player)
            {
                var cell = cells[row][column];
                cell.ChangeColor(PlayerColor(player));
            }

            private Color PlayerColor(int player)
            {
                if(player == 0)
                {
                    return Color.Gray;
                }
                else if(player == 1)
                {
                    return Color.Yellow;
                }
                else
                {
                    return Color.Red;
                }
            }
        }
    }
}
