using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace main
{



    public class Region
    {
        public Region(int id, SuperRegion superRegion)
        {
            Id = id;
            SuperRegion = superRegion;
            Neighbors = new List<Region>();
            PlayerName = "unknown";
            Armies = 0;

            superRegion.AddSubRegion(this);
        }

        public Region(int id, SuperRegion superRegion, string playerName, int armies)
        {
            Id = id;
            SuperRegion = superRegion;
            Neighbors = new List<Region>();
            PlayerName = playerName;
            Armies = armies;

            superRegion.AddSubRegion(this);
        }

        public void AddNeighbor(Region neighbor)
        {
            if (!Neighbors.Contains(neighbor))
            {
                Neighbors.Add(neighbor);
                neighbor.AddNeighbor(this);
            }
        }

        /**
         * @param region a Region object
         * @return True if this Region is a neighbor of given Region, false otherwise
         */
        public bool IsNeighbor(Region region)
        {
            return (Neighbors.Contains(region));
        }

        /**
         * @param playerName A string with a player's name
         * @return True if this region is owned by given playerName, false otherwise
         */
        public bool OwnedByPlayer(String playerName)
        {
            return PlayerName == playerName;
        }

        public int Armies
        {
            get; set;
        }

        public String PlayerName
        {
            get; set;
        }

        public int Id
        {
            get; private set;
        }

        public List<Region> Neighbors
        {
            get; private set;
        }

        public SuperRegion SuperRegion
        {
            get; private set;
        }

    }

}