using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyCraftProjectSharingApp.Models;
using MyCraftProjectSharingApp.Mappers;
using BusinessLogicLayer;

namespace MyCraftProjectSharingApp.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        static UsersLogic _userBusinessLogic = new UsersLogic();
        static PaswordHashLogic _hashBusinessLogic = new PaswordHashLogic();
        static MapperUsers _userMapper = new MapperUsers();
        static HousesController _houseController = new HousesController();
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home", new { area = "" });
        } //return to home page
        [HttpGet]
        public ActionResult Login(string username, string password)
        {
            return View();
        } //add user info to session for all users
        [HttpPost]
        public ActionResult Login(ViewModel userToLogin)
        {
            Users user = new Users();
            user = _userMapper.MapUser(_userBusinessLogic.GetUserByUsername(userToLogin.SingleUser.Username));
            if (ModelState.IsValid)
            {
                if (user.Username != null)
                {
                    if (user.Password != null)
                    {
                        string userPassword = _hashBusinessLogic.HashPassword(userToLogin.SingleUser.Password);   //giving different hashed values even without salt
                        if (userPassword == user.Password)
                        {
                            Session["UserId"] = user.UserId;
                            Session["Username"] = user.Username;
                            Session["RoleID"] = user.RoleID;
                            Session["H_Id"] = user.H_Id;
                            return RedirectToAction("Index", "Home", new { area = "" });
                        }
                        TempData["LoginError"] = "Username or password is incorrect. Please check and try again.";
                        return View();
                    }
                    else
                    {
                        TempData["LoginError"] = "Username or password is incorrect. Please check and try again.";
                        return View();
                    }
                }
                TempData["LoginError"] = "Username does not exist. Please check and try again.";
                return View();
            }
            else
            {
                return View();
            }
        } //add user info to session for all users
        public ActionResult Logout()
        {
            if (Session["RoleID"] != null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            else
            {
                return RedirectToAction("Login", "Users", new { area = "" });
            }
        } //clear session for all users
        [HttpGet]
        public ActionResult CreateUser()
        {
            return View();
        } //create reg user
        [HttpPost]
        public ActionResult CreateUser(ViewModel userToAdd)
        {
            if (!ModelState.IsValid)
            {
                TempData["CreateError"] = "All fields are required.";
                return View();
            }
            else
            {
                if (userToAdd.SingleUser.H_Id != null && userToAdd.SingleUser.FirstName != null && userToAdd.SingleUser.LastName != null && userToAdd.SingleUser.Email != null && userToAdd.SingleUser.Age != null)
                {
                    Users user = new Users();
                    user = _userMapper.MapUser(_userBusinessLogic.GetUserByUsername(userToAdd.SingleUser.Username));
                    if (user.Username == null)
                    {
                        _userBusinessLogic.AddUser(_userMapper.MapUser(userToAdd.SingleUser));
                        user = _userMapper.MapUser(_userBusinessLogic.GetUserByUsername(userToAdd.SingleUser.Username));
                        if (user.Username == userToAdd.SingleUser.Username)
                        {
                            _houseController.AddPoints(5, userToAdd.SingleUser.H_Id);
                            if (userToAdd.SingleUser.H_Id == 1)
                            {
                                TempData["UserCreateSuccess"] = "5 points to GYFFINDOR!";
                            }
                            else if (userToAdd.SingleUser.H_Id == 2)
                            {
                                TempData["UserCreateSuccess"] = "5 points to SLYTHERIN!";
                            }
                            else if (userToAdd.SingleUser.H_Id == 3)
                            {
                                TempData["UserCreateSuccess"] = "5 points to RAVENCLAW!";
                            }
                            else if (userToAdd.SingleUser.H_Id == 4)
                            {
                                TempData["UserCreateSuccess"] = "5 points to HUFFLEPUFF!";
                            }
                            return RedirectToAction("Login", "Users", new { area = "" });
                        }
                        else
                        {
                            TempData["UsernameError"] = "Username is already in use. Please choose another.";
                            return View();
                        }
                    }
                    else
                    {
                        TempData["UsernameError"] = "Username is already in use. Please choose another.";
                        return View();
                    }
                }
                else
                {
                    TempData["CreateError"] = "All fields are required.";
                    return View();
                }
            }
        } //create reg user
        public ActionResult ViewUserByUserId()
        {
            Users user = new Users();
            ViewModel userViewModel = new ViewModel();
            if (Session["RoleID"] != null && Session["UserId"] != null)
            {
                int userToGet = (int)Session["UserId"];
                user = _userMapper.MapUser(_userBusinessLogic.GetUserByUserId(userToGet));
                if ((int)Session["UserId"] == user.UserId)
                {
                    userViewModel.SingleUser = _userMapper.MapUser(_userBusinessLogic.GetUserByUserId(userToGet));
                    return View(userViewModel);
                }
                else
                {
                    return RedirectToAction("PageError", "Error", new { area = "" });
                }
            }
            else
            {
                return RedirectToAction("Login", "Users", new { area = "" });
            }
        } //View for individual users
        public ActionResult ViewAllUsers()
        {
            if (Session["RoleID"] != null)
            {
                if ((int)Session["RoleID"] == 3)
                {
                    ViewModel users = new ViewModel();
                    users.Users = _userMapper.MapUsers(_userBusinessLogic.GetUsers());
                    return View(users);
                }
                else
                {
                    return RedirectToAction("PageError", "Error", new { area = "" });
                }
            }
            else
            {
                return RedirectToAction("Login", "Users", new { area = "" });
            }
        } //View for admin user
        [HttpGet]
        public ActionResult UpdateUser(int userId)
        {
            if (Session["RoleID"] != null)
            {
                if (userId == (int)Session["UserId"] || (int)Session["RoleID"] == 3)
                {
                    ViewModel userViewModel = new ViewModel();
                    userViewModel.SingleUser.UserId = userId;
                    return View(userViewModel);
                }
                else
                {
                    return RedirectToAction("PageError", "Error", new { area = "" });
                }
            }
            else
            {
                return RedirectToAction("Login", "Users", new { area = "" });
            }
        } //Update for individual users and admin
        [HttpPost]
        public ActionResult UpdateUser(ViewModel userToUpdate)
        {
            if (Session["RoleID"] != null && Session["UserId"] != null)
            {
                if (userToUpdate.SingleUser.UserId == (int)Session["UserId"] || (int)Session["RoleID"] == 3)
                {
                    if (ModelState.IsValid)
                    {
                        if (userToUpdate.SingleUser.FirstName != null && userToUpdate.SingleUser.LastName != null && userToUpdate.SingleUser.Email != null && userToUpdate.SingleUser.Age != null)
                        {
                            Users user = new Users();
                            user = _userMapper.MapUser(_userBusinessLogic.GetUserByUsername(userToUpdate.SingleUser.Username));
                            if (user.Username == null || user.Username == userToUpdate.SingleUser.Username)
                            {
                                if ((int)Session["RoleID"] == 3)
                                {
                                    if (userToUpdate.SingleUser.RoleID != null)
                                    {
                                        user.RoleID = userToUpdate.SingleUser.RoleID;
                                        _userBusinessLogic.UpdateUser(userToUpdate.SingleUser.UserId, _userMapper.MapUser(userToUpdate.SingleUser));
                                        if (userToUpdate.SingleUser.UserId == (int)Session["UserId"])
                                        {
                                            return RedirectToAction("ViewUserByUserId", "Users", new { area = "" });
                                        }
                                        else
                                        {
                                            return RedirectToAction("ViewAllUsers", "Users", new { area = "" });
                                        }
                                    }

                                    else
                                    {
                                        TempData["UpdateError"] = "All fields are required.";
                                        return View();
                                    }
                                }
                                else
                                {
                                    userToUpdate.SingleUser.RoleID = (int)Session["RoleID"];
                                    _userBusinessLogic.UpdateUser(userToUpdate.SingleUser.UserId, _userMapper.MapUser(userToUpdate.SingleUser));
                                    return RedirectToAction("ViewUserByUserId", "Users", new { area = "" });
                                }
                            }
                            else
                            {
                                TempData["UsernameError"] = "Username is already in use. Please choose another.";
                                return View();
                            }
                        }
                        else
                        {
                            TempData["UpdateError"] = "All fields are required.";
                            return View();
                        }
                    }
                    else
                    {
                        TempData["UpdateError"] = "All fields are required.";
                        return View();
                    }
                }
                else
                {
                    return RedirectToAction("PageError", "Error", new { area = "" });
                }
            }
            else
            {
                return RedirectToAction("Login", "Users", new { area = "" });
            }
        } //Update for individual users and admin
        [HttpGet]
        public ActionResult DeleteUser(int userId)
        {
            if (Session["RoleId"] != null)
            {
                if ((int)Session["RoleID"] == 3)
                {
                    Users user = new Users();
                    user = _userMapper.MapUser(_userBusinessLogic.GetUserByUserId(userId));
                    _userBusinessLogic.DeleteUser(userId);
                    _houseController.AddPoints(-5, user.H_Id);
                    return RedirectToAction("ViewAllUsers", "Users", new { area = "" });
                }
                else
                {
                    _userBusinessLogic.DeleteUser(userId);
                    _houseController.AddPoints(-5, (int)Session["H_Id"]);
                    return RedirectToAction("Login", "Users", new { area = "" });
                }
            }
            else
            {
                TempData["DeleteSuccess"] = "User has been deleted.";
                return RedirectToAction("Login", "Users", new { area = "" });
            }
        } //Delete for individual users and admin
    }
}