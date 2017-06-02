using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DataAccessObjects;
using BusinessLogicLayer.BusinessLogicObjects;

namespace BusinessLogicLayer.BusinessLogicMappers
{
    public class BusinessLogicMapperProjects
    {
        public DataAccessProjects MapProject(BusinessLogicProjects bProject)
        {
            DataAccessProjects dProject = new DataAccessProjects();
            dProject.ProjectId = bProject.ProjectId;
            dProject.User_Id = bProject.User_Id;
            dProject.Craft_Id = bProject.Craft_Id;
            dProject.ProjectName = bProject.ProjectName;
            dProject.ProjectBody = bProject.ProjectBody;
            dProject.Difficulty_ID = bProject.Difficulty_ID;
            dProject.DifficultyLevel = bProject.DifficultyLevel;
            return dProject;
        }
        public BusinessLogicProjects MapProject(DataAccessProjects dProject)
        {
            BusinessLogicProjects bProject = new BusinessLogicProjects();
            bProject.ProjectId = dProject.ProjectId;
            bProject.User_Id = dProject.User_Id;
            bProject.Craft_Id = dProject.Craft_Id;
            bProject.ProjectName = dProject.ProjectName;
            bProject.ProjectBody = dProject.ProjectBody;
            bProject.Difficulty_ID = dProject.Difficulty_ID;
            bProject.DifficultyLevel = dProject.DifficultyLevel;
            return bProject;
        }
        public List<BusinessLogicProjects> MapProjects(List<DataAccessProjects> dProjects)
        {
            List<BusinessLogicProjects> bProjects = new List<BusinessLogicProjects>();
            foreach (DataAccessProjects dProject in dProjects)
            {
                bProjects.Add(MapProject(dProject));
            }
            return bProjects;
        }
        public List<DataAccessProjects> MapProjects(List<BusinessLogicProjects> bProjects)
        {
            List<DataAccessProjects> dProjects = new List<DataAccessProjects>();
            foreach (BusinessLogicProjects bProject in bProjects)
            {
                dProjects.Add(MapProject(bProject));
            }
            return dProjects;
        }
    }
}
