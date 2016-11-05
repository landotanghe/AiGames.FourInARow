using System;


using main;

namespace move
{

	/**
	 * This Move is used in the second part of each round. It represents the attack or transfer of armies from
	 * fromRegion to toRegion. If toRegion is owned by the player himself, it's a transfer. If toRegion is
	 * owned by the opponent, this Move is an attack. 
	 */

	public class AttackTransferMove : Move {
		
		
		public AttackTransferMove(string playerName, Region fromRegion, Region toRegion, int armies)
		{
			PlayerName = playerName;
			FromRegion = fromRegion;
			ToRegion = toRegion;
            Armies = armies;
		}

		public int Armies
		{
            get; set;
		}

		public Region FromRegion
		{
            get; private set;
		}

		public Region ToRegion
		{
            get; private set;
		}

		public string String
		{
			get { 
				if(IsLegalMove)
					return PlayerName + " attack/transfer " + FromRegion.Id + " " + ToRegion.Id + " " + Armies;
				else
					return PlayerName + " illegal_move " + IllegalMoveMessage; 
			}
		}

	}
}