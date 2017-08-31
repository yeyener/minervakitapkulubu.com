using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Microsoft.AspNetCore.Mvc;
using Minerva.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Minerva.Web.Models;

namespace Minerva.Web.Controllers
{
    public class HomeController : CommonController
    {


        public IActionResult Index()
        {
            MainModel mainModel = new MainModel();
            mainModel.ThemeModel = ThemeModel.GetFirst();
            mainModel.JoinUsModel = new JoinUsModel();
            return View(mainModel);
        }


        public IActionResult Welcome()
        {
            
            
            return View();
        }
        
    }
}
