using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogicLayer.BusinessLogicObjects;
using MyCraftProjectSharingApp.Models;

namespace MyCraftProjectSharingApp.Mappers
{
    public class MapperCrafts
    {
        public BusinessLogicCrafts MapCraft(Crafts craft)
        {
            BusinessLogicCrafts bCraft = new BusinessLogicCrafts();
            bCraft.CraftId = craft.CraftId;
            bCraft.CraftName = craft.CraftName;
            bCraft.Description = craft.Description;
            bCraft.U_Id = craft.U_Id;
            return bCraft;
        }
        public Crafts MapCraft(BusinessLogicCrafts bCraft)
        {
            Crafts craft = new Crafts();
            craft.CraftId = bCraft.CraftId;
            craft.CraftName = bCraft.CraftName;
            craft.Description = bCraft.Description;
            craft.U_Id = bCraft.U_Id;
            return craft;
        }
        public List<Crafts> MapCrafts(List<BusinessLogicCrafts> bCrafts)
        {
            List<Crafts> crafts = new List<Crafts>();
            foreach (BusinessLogicCrafts bCraft in bCrafts)
            {
                crafts.Add(MapCraft(bCraft));
            }
            return crafts;
        }
        public List<BusinessLogicCrafts> MapCrafts(List<Crafts> crafts)
        {
            List<BusinessLogicCrafts> bCrafts = new List<BusinessLogicCrafts>();
            foreach (Crafts craft in crafts)
            {
                bCrafts.Add(MapCraft(craft));
            }
            return bCrafts;
        }
    }
}