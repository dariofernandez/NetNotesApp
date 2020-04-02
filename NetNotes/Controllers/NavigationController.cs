using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using NetNotes.Models;
using NetNotes.Class;
using System.Dynamic;


namespace NetNotes.Controllers
{
    public class NavigationController : Controller
    {
        // GET: Navigation
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Menu()
        {
            return PartialView("_NavMenuNotes");
        }


        public ActionResult HorizontalTabs()
        {
            return PartialView("_HorizontalTabs");
        }


        public ActionResult MyModalDialog(string DialogName)
        {
            string sTest = DialogName;
            return PartialView("_ViewModalDialog");
        }


        public ActionResult MenuVertical()
        {
            List<MenuViewModel> menuViewModel = new List<MenuViewModel>();

            MenuViewModel menu = new MenuViewModel() { MenuID = 1, Action = "Index", Controller = "Dashboard", IsAction = true, Class = "class", SubMenu = null, Title = "Dashboard" };
            menuViewModel.Add(menu);

            menu = new MenuViewModel() { MenuID = 2, IsAction = false, Class = "class", Link = "javascript:void(0);", Title = "Application Setup" };

            menu.SubMenu = new List<MenuViewModel>();
            MenuViewModel subMenu = new MenuViewModel() { Action = "Register", Controller = "Account", IsAction = true, Class = "", SubMenu = null, Title = "User Manager" };
            menu.SubMenu.Add(subMenu);

            subMenu = new MenuViewModel() { Action = "Index", Controller = "Manage", IsAction = true, Class = "", SubMenu = null, Title = "Manage" };
            menu.SubMenu.Add(subMenu);

            subMenu = new MenuViewModel() { Action = "ChangePassword", Controller = "Manage", IsAction = true, Class = "", SubMenu = null, Title = "Change Password" };
            menu.SubMenu.Add(subMenu);

            subMenu = new MenuViewModel() { IsAction = false, Link = "javascript:document.getElementById('logoutForm').submit()", Class = "", SubMenu = null, Title = "Logoff" };
            menu.SubMenu.Add(subMenu);

            menuViewModel.Add(menu);

            return PartialView("_NavMenuNotes", menuViewModel);
        }




    }
}