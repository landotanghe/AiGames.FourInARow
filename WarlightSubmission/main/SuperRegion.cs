using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace main
{

    public class SuperRegion
    {

        public SuperRegion(int id, int armiesReward)
        {
            Id = id;
            ArmiesReward = armiesReward;
            SubRegions = new List<Region>();
        }

        public void AddSubRegion(Region subRegion)
        {
            if (!SubRegions.Contains(subRegion))
                SubRegions.Add(subRegion);
        }

        /**
         * @return A string with the name of the player that fully owns this SuperRegion
         */
        public string OwnedByPlayer()
    	{
    		String playerName = SubRegions.First().PlayerName;
    		foreach(Region region in SubRegions)
    		{
    			if (playerName != region.PlayerName)
    				return null;
    		}
    		return playerName;
    	}

        public int Id
        {
            get; private set;
        }

        public int ArmiesReward
        {
            get; private set;
        }

        public List<Region> SubRegions
        {
            get; private set;
        }

    }

}