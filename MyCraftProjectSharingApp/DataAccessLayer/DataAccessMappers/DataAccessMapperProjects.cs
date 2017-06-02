using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataAccessLayer.DataAccessObjects;

namespace DataAccessLayer.DataAccessMappers
{
    public class DataAccessMapperProjects
    {
        public List<DataAccessProjects> TableToListOfProjects(DataTable projectsTable)
        {
            List<DataAccessProjects> dProjects = new List<DataAccessProjects>();
            if (projectsTable != null && projectsTable.Rows.Count > 0)
            {
                foreach (DataRow row in projectsTable.Rows)
                {
                    DataAccessProjects dProject = new DataAccessProjects();
                    dProject = RowToProjects(row);
                    dProjects.Add(dProject);
                }
            }
            return dProjects;
        }
        public static DataAccessProjects RowToProjects(DataRow tableRow)
        {
            DataAccessProjects dProject = new DataAccessProjects();
            dProject.ProjectId = (int)tableRow["ProjectId"];
            dProject.U_Id = (int)tableRow["U_Id"];
            dProject.C_Id = (int)tableRow["C_Id"];
            dProject.ProjectName = tableRow["ProjectName"].ToString();
            dProject.ProjectBody = tableRow["ProjectBody"].ToString();
            dProject.Difficulty = tableRow["Difficulty"].ToString();
            return dProject;
        }
    }
}
