using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataAccessLayer.DataAccessObjects;

namespace DataAccessLayer.DataAccessMappers
{
    public class DataAccessMapperUsers
    {
        public List<DataAccessUsers> TableToListOfUsers(DataTable usersTable)
        {
            List<DataAccessUsers> dUsers = new List<DataAccessUsers>();
            if (usersTable != null && usersTable.Rows.Count > 0)
            {
                foreach (DataRow row in usersTable.Rows)
                {
                    DataAccessUsers dUser = new DataAccessUsers();
                    dUser = RowToUsers(row);
                    dUsers.Add(dUser);
                }
            }
            return dUsers;
        }
        public static DataAccessUsers RowToUsers(DataRow tableRow)
        {
            DataAccessUsers dUser = new DataAccessUsers();
            dUser.UserId = (int)tableRow["UserId"];
            dUser.FirstName = tableRow["FirstName"].ToString();
            dUser.LastName = tableRow["LastName"].ToString();
            dUser.Age = (int)tableRow["Age"];
            dUser.Gender = tableRow["Gender"].ToString();
            dUser.Email = tableRow["Email"].ToString();
            dUser.Username = tableRow["Username"].ToString();
            dUser.Password = tableRow["Password"].ToString();
            dUser.House_Id = (int)tableRow["House_Id"];
            dUser.RoleID = (int)tableRow["RoleID"];
            return dUser;
        }
    }
}
