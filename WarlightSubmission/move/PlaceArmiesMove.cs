using System;
using main;

namespace move
{

/**
 * This Move is used in the first part of each round. It represents what Region is increased
 * with how many armies.
 */

	public class PlaceArmiesMove : Move {
				
		public PlaceArmiesMove(string playerName, Region region, int armies)
		{
			PlayerName = playerName;
            Region = region;
            Armies = armies;
		}

		public int Armies { set; get; }

		public Region Region
		{
            get; private set;
		}

		public string String
		{
			get { 
				if(IsLegalMove)
					return PlayerName + " place_armies " + Region.Id + " " + Armies;
				else
					return PlayerName + " illegal_move " + IllegalMoveMessage;
			}
		}
	}
}