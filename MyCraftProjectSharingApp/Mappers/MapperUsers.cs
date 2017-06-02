﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogicLayer.BusinessLogicObjects;
using MyCraftProjectSharingApp.Models;

namespace MyCraftProjectSharingApp.Mappers
{
    public class MapperUsers
    {
        public BusinessLogicUsers MapUser(Users user)
        {
            BusinessLogicUsers bUser = new BusinessLogicUsers();
            bUser.UserId = user.UserId;
            bUser.FirstName = user.FirstName;
            bUser.LastName = user.LastName;
            bUser.Age = user.Age;
            bUser.Gender = user.Gender;
            bUser.Email = user.Email;
            bUser.Username = user.Username;
            bUser.Password = user.Password;
            bUser.H_Id = user.H_Id;
            bUser.RoleID = user.RoleID;
            return bUser;
        }
        public Users MapUser(BusinessLogicUsers bUser)
        {
            Users user = new Users();
            user.UserId = bUser.UserId;
            user.FirstName = bUser.FirstName;
            user.LastName = bUser.LastName;
            user.Age = bUser.Age;
            user.Gender = bUser.Gender;
            user.Email = bUser.Email;
            user.Username = bUser.Username;
            user.Password = bUser.Password;
            user.H_Id = bUser.H_Id;
            user.RoleID = bUser.RoleID;
            return user;
        }
        public List<BusinessLogicUsers> MapUsers(List<Users> users)
        {
            List<BusinessLogicUsers> bUsers = new List<BusinessLogicUsers>();
            foreach (Users user in users)
            {
                bUsers.Add(MapUser(user));
            }
            return bUsers;
        }
        public List<Users> MapUsers(List<BusinessLogicUsers> bUsers)
        {
            List<Users> users = new List<Users>();
            foreach (BusinessLogicUsers bUser in bUsers)
            {
                users.Add(MapUser(bUser));
            }
            return users;
        }
    }
}