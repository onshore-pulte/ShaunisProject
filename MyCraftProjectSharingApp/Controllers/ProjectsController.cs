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
    public class ProjectsController : Controller
    {
        // GET: Projects
        static ProjectsLogic _projectBusinessLogic = new ProjectsLogic();
        static CraftsLogic _craftBusinessLogic = new CraftsLogic();
        static UsersLogic _userBusinessLogic = new UsersLogic();
        static MapperProjects _projectMapper = new MapperProjects();
        static MapperCrafts _craftMapper = new MapperCrafts();
        static MapperUsers _userMapper = new MapperUsers();
        static HousesController _houseController = new HousesController();
        public ActionResult Index()
        {
            return View("Index", "Home", new { area = "" });
        } //return to main homepage
        [HttpGet]
        public ActionResult CreateProject()
        {
            ViewModel projectViewModel = new ViewModel();
            if (Session["RoleID"] != null)
            {
                ViewModel users = new ViewModel();
                projectViewModel.Crafts = _craftMapper.MapCrafts(_craftBusinessLogic.GetCrafts());
                projectViewModel.SingleProject.U_Id = (int)Session["UserId"];
                return View(projectViewModel);
            }
            else
            {
                return RedirectToAction("Login", "Users", new { area = "" });
            }
        } //create project for all users
        [HttpPost]
        public ActionResult CreateProject(ViewModel projectToAdd)
        {
            if (Session["RoleID"] != null)
            {
                if (ModelState.IsValid)
                {
                    _projectBusinessLogic.AddProject(_projectMapper.MapProject(projectToAdd.SingleProject));
                    ViewModel projects = new ViewModel();
                    projects.Projects = _projectMapper.MapProjects(_projectBusinessLogic.GetProjects());
                    _houseController.AddPoints(20, (int)Session["H_Id"]);
                    if ((int)Session["H_Id"] == 1)
                    {
                        TempData["ProjectSuccess"] = "20 points to GRYFFINDOR!";
                    }
                    else if ((int)Session["H_Id"] == 2)
                    {
                        TempData["ProjectSuccess"] = "20 points to SLYTHERIN!";
                    }
                    else if ((int)Session["H_Id"] == 3)
                    {
                        TempData["ProjectSuccess"] = "20 points to RAVENCLAW!";
                    }
                    else if ((int)Session["H_Id"] == 4)
                    {
                        TempData["ProjectSuccess"] = "20 points to HUFFLEPUFF!";
                    }
                    return RedirectToAction("ViewProjects", "Projects", new { area = "" });
                }
                else
                {
                    ViewModel projectViewModel = new ViewModel();
                    ViewModel users = new ViewModel();
                    projectViewModel.Crafts = _craftMapper.MapCrafts(_craftBusinessLogic.GetCrafts());
                    projectViewModel.SingleProject.U_Id = (int)Session["UserId"];
                    return View(projectViewModel);
                }
            }
            else
            {
                return RedirectToAction("Login", "Users", new { area = "" });
            }

        } //create project for all users
        public ActionResult ViewProjects()
        {
            if (Session["RoleID"] != null)
            {
                ViewModel projects = new ViewModel();
                ViewModel users = new ViewModel();
                ViewModel crafts = new ViewModel();
                projects.Projects = _projectMapper.MapProjects(_projectBusinessLogic.GetProjects());
                foreach (Projects project in projects.Projects)
                {
                    users.SingleUser = _userMapper.MapUser(_userBusinessLogic.GetUserByUserId(project.U_Id));
                    if (project.U_Id == users.SingleUser.UserId)
                    {
                        project.Username = users.SingleUser.Username;
                    }
                }
                foreach (Projects project in projects.Projects)
                {
                    crafts.SingleCraft = _craftMapper.MapCraft(_craftBusinessLogic.GetCraftByCraftId(project.C_Id));
                    if (project.C_Id == crafts.SingleCraft.CraftId)
                    {
                        project.CraftName = crafts.SingleCraft.CraftName;
                    }
                }
                return View(projects);
            }
            else
            {
                return RedirectToAction("Login", "Users", new { area = "" });
            }
        } //view project for all users
        [HttpGet]
        public ActionResult UpdateProject(int projectToUpdate)
        {
            if (Session["RoleID"] != null)
            {
                Projects project = new Projects();
                project = _projectMapper.MapProject(_projectBusinessLogic.GetProjectByProjectId(projectToUpdate));
                if ((int)Session["UserId"] == project.U_Id || (int)Session["RoleID"] == 3)
                {
                    ViewModel projectViewModel = new ViewModel();
                    projectViewModel.Crafts = _craftMapper.MapCrafts(_craftBusinessLogic.GetCrafts());
                    projectViewModel.SingleProject.ProjectId = projectToUpdate;
                    projectViewModel.SingleProject.U_Id = project.U_Id;
                    return View(projectViewModel);
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
        }   //update project for all users
        [HttpPost]
        public ActionResult UpdateProject(ViewModel projectToUpdate)
        {
            if (Session["RoleId"] != null)
            {
                if (ModelState.IsValid)
                {
                    Projects project = new Projects();
                    project = _projectMapper.MapProject(_projectBusinessLogic.GetProjectByProjectId(projectToUpdate.SingleProject.ProjectId));
                    if ((int)Session["UserId"] == projectToUpdate.SingleProject.U_Id || (int)Session["RoleID"] == 3)
                    {
                        _projectBusinessLogic.UpdateProject(projectToUpdate.SingleProject.ProjectId, _projectMapper.MapProject(projectToUpdate.SingleProject));
                        TempData["ProjectSuccess"] = "Project successfully updated.";
                        return RedirectToAction("ViewProjects", "Projects", new { area = "" });
                    }
                    else
                    {
                        return RedirectToAction("PageError", "Error", new { area = "" });
                    }
                }
                else
                {
                    ViewModel projectViewModel = new ViewModel();
                    projectViewModel.Crafts = _craftMapper.MapCrafts(_craftBusinessLogic.GetCrafts());
                    projectViewModel.SingleProject.ProjectId = projectToUpdate.SingleProject.ProjectId;
                    return View(projectViewModel);
                }
            }
            else
            {
                return RedirectToAction("Login", "Users", new { area = "" });

            }
        }  //update project for all users
        public ActionResult DeleteProject(int projectToDelete)
        {
            if (Session["RoleID"] != null)
            {
                Projects project = new Projects();
                project = _projectMapper.MapProject(_projectBusinessLogic.GetProjectByProjectId(projectToDelete));
                if ((int)Session["UserId"] == project.U_Id || (int)Session["RoleID"] != 3)
                {
                    _projectBusinessLogic.DeleteProject(projectToDelete);
                    _houseController.AddPoints(-20, (int)Session["H_Id"]);
                    TempData["ProjectDeleted"] = "Project has been deleted successfully.";
                    return RedirectToAction("ViewProjects", "Projects", new { area = "" });
                }
                else
                {
                    ViewModel user = new ViewModel();
                    user.SingleUser = _userMapper.MapUser(_userBusinessLogic.GetUserByUserId(project.U_Id));
                    if (project.U_Id == user.SingleUser.UserId)
                    {
                        _houseController.AddPoints(-20, user.SingleUser.H_Id);
                    }
                    _projectBusinessLogic.DeleteProject(projectToDelete);
                    TempData["ProjectDeleted"] = "Project has been deleted successfully.";
                    return RedirectToAction("ViewProjects", "Projects", new { area = "" });
                }
            }
            else
            {
                return RedirectToAction("Login", "Users", new { area = "" });

            }
        } //delete project for user based on role or userId
    }
}