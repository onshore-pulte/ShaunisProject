using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataAccessObjects
{
    public class DataAccessProjects
    {
        public int ProjectId { get; set; }
        public int User_Id { get; set; }
        public int Craft_Id { get; set; }
        public string ProjectName { get; set; }
        public string ProjectBody { get; set; }
        public int Difficulty_ID { get; set; }
    }
}
