using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogicLayer.BusinessLogicObjects;
using MyCraftProjectSharingApp.Models;

namespace MyCraftProjectSharingApp.Mappers
{
    public class MapperProjects
    {
        public BusinessLogicProjects MapProject(Projects project)
        {
            BusinessLogicProjects bProject = new BusinessLogicProjects();
            bProject.ProjectId = project.ProjectId;
            bProject.U_Id = project.U_Id;
            bProject.C_Id = project.C_Id;
            bProject.ProjectName = project.ProjectName;
            bProject.ProjectBody = project.ProjectBody;
            bProject.Difficulty = project.Difficulty;
            return bProject;
        }
        public Projects MapProject(BusinessLogicProjects bProject)
        {
            Projects project = new Projects();
            project.ProjectId = bProject.ProjectId;
            project.U_Id = bProject.U_Id;
            project.C_Id = bProject.C_Id;
            project.ProjectName = bProject.ProjectName;
            project.ProjectBody = bProject.ProjectBody;
            project.Difficulty = bProject.Difficulty;
            return project;
        }
        public List<Projects> MapProjects(List<BusinessLogicProjects> bProjects)
        {
            List<Projects> projects = new List<Projects>();
            foreach (BusinessLogicProjects bProject in bProjects)
            {
                projects.Add(MapProject(bProject));
            }
            return projects;
        }
        public List<BusinessLogicProjects> MapProjects(List<Projects> projects)
        {
            List<BusinessLogicProjects> bProjects = new List<BusinessLogicProjects>();
            foreach (Projects project in projects)
            {
                bProjects.Add(MapProject(project));
            }
            return bProjects;
        }
    }
}