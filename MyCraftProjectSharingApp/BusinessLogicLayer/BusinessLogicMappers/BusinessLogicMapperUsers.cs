using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DataAccessObjects;
using BusinessLogicLayer.BusinessLogicObjects;

namespace BusinessLogicLayer.BusinessLogicMappers
{
    public class BusinessLogicMapperUsers
    {
        static PaswordHashLogic _businessLogic = new PaswordHashLogic();
        public DataAccessUsers MapUser(BusinessLogicUsers bUser)
        {
            DataAccessUsers dUser = new DataAccessUsers();
            dUser.UserId = bUser.UserId;
            dUser.FirstName = bUser.FirstName;
            dUser.LastName = bUser.LastName;
            dUser.Age = bUser.Age;
            dUser.Gender = bUser.Gender;
            dUser.Email = bUser.Email;
            dUser.Username = bUser.Username;
            dUser.Password = _businessLogic.HashPassword(bUser.Password);
            dUser.House_Id = bUser.House_ID;
            dUser.RoleID = bUser.Role_ID;
            dUser.RoleName = bUser.RoleName;
            return dUser;
        }
        public BusinessLogicUsers MapUser(DataAccessUsers dUser)
        {
            BusinessLogicUsers bUser = new BusinessLogicUsers();
            bUser.UserId = dUser.UserId;
            bUser.FirstName = dUser.FirstName;
            bUser.LastName = dUser.LastName;
            bUser.Age = dUser.Age;
            bUser.Gender = dUser.Gender;
            bUser.Email = dUser.Email;
            bUser.Username = dUser.Username;
            bUser.Password = dUser.Password;
            bUser.House_Id = dUser.House_Id;
            bUser.Role_ID = dUser.RoleID;
            bUser.RoleName = dUser.RoleName;
            return bUser;
        }
        public List<BusinessLogicUsers> MapUsers(List<DataAccessUsers> dUsers)
        {
            List<BusinessLogicUsers> bUsers = new List<BusinessLogicUsers>();
            foreach (DataAccessUsers dUser in dUsers)
            {
                bUsers.Add(MapUser(dUser));
            }
            return bUsers;
        }
        public List<DataAccessUsers> MapUsers(List<BusinessLogicUsers> bUsers)
        {
            List<DataAccessUsers> dUsers = new List<DataAccessUsers>();
            foreach (BusinessLogicUsers bUser in bUsers)
            {
                dUsers.Add(MapUser(bUser));
            }
            return dUsers;
        }

    }
}
