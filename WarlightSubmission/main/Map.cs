using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace main
{


    public class Map
    {
        public Map()
        {
            Regions = new List<Region>();
            SuperRegions = new List<SuperRegion>();
        }

        public Map(List<Region> regions, List<SuperRegion> superRegions)
        {
            Regions = regions;
            SuperRegions = superRegions;
        }

        /**
         * Add a Region to the map
         * @param region : Region to be Added
         */
        public void Add(Region region)
        {
            foreach (Region r in Regions)
                if (r.Id == region.Id)
                {
                    Console.Error.WriteLine("Region cannot be Added: id already exists.");
                    return;
                }
            Regions.Add(region);
        }

        /**
         * Add a SuperRegion to the map
         * @param superRegion : SuperRegion to be Added
         */
        public void Add(SuperRegion superRegion)
        {
            foreach (SuperRegion sr in SuperRegions)
                if (sr.Id == superRegion.Id)
                {
                    Console.Error.WriteLine("SuperRegion cannot be Added: id already exists.");
                    return;
                }
            SuperRegions.Add(superRegion);
        }

        /**
         * @return : a new Map object exactly the same as this one
         */
        public Map GetMapCopy()
        {
            Map newMap = new Map();
            foreach (SuperRegion sr in SuperRegions) //copy superRegions
            {
                SuperRegion newSuperRegion = new SuperRegion(sr.Id, sr.ArmiesReward);
                newMap.Add(newSuperRegion);
            }
            foreach (Region r in Regions) //copy regions
            {
                Region newRegion = new Region(r.Id, newMap.GetSuperRegion(r.SuperRegion.Id), r.PlayerName, r.Armies);
                newMap.Add(newRegion);
            }
            foreach (Region r in Regions) //Add neighbors to copied regions
            {
                Region newRegion = newMap.GetRegion(r.Id);
                foreach (Region neighbor in r.Neighbors)
                    newRegion.AddNeighbor(newMap.GetRegion(neighbor.Id));
            }
            return newMap;
        }

        /**
         * @param id : a Region id number
         * @return : the matching Region object
         */
        public Region GetRegion(int id)
        {
            return Regions.Where(r => r.Id == id).FirstOrDefault();
        }

        /**
         * @param id : a SuperRegion id number
         * @return : the matching SuperRegion object
         */
        public SuperRegion GetSuperRegion(int id)
        {
            return SuperRegions.Where(r => r.Id == id).FirstOrDefault();
        }

        public List<Region> Regions
        {
            get; private set;
        }

        public List<SuperRegion> SuperRegions
        {
            get; private set;
        }

        public string MapString
        {
            get { return string.Join(" ", Regions.Select(region => region.Id + ";" + region.PlayerName + ";" + region.Armies)); }
        }
    }
}