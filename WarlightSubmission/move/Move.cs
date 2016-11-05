using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace move
{

    public class Move
    {
        public Move()
        {
            IllegalMoveMessage = String.Empty;
        }
        
        // Name of the player that did this move
        public string PlayerName
        {
            get; set;
        }

        // Gets the value of the error message if move is illegal, else remains empty
        public string IllegalMoveMessage
        {
            get; set;
        }

        public bool IsLegalMove
        {
            get { return String.IsNullOrEmpty(IllegalMoveMessage); }
        }

    }
}