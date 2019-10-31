using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeWork_4_29_2019.Models;
using Microsoft.AspNetCore.Hosting;
using Homework_4_29_2019.Data;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace HomeWork_4_29_2019.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment _environment;
        private string _connectionstring;

        public HomeController(IHostingEnvironment environment,
            IConfiguration configuration)
        {
            _environment = environment;
            _connectionstring = configuration.GetConnectionString("ConStr");
        }

        //ImagedbManager mgr = new ImagedbManager(_connectionstring)

        public IActionResult Index()
        {
            ImagedbManager mgr = new ImagedbManager(_connectionstring);
            IEnumerable<Image> images = mgr.GetImages();
            return View(images);
        }

        public IActionResult About()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadImage(IFormFile myFile, Image I)
        {
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(myFile.FileName)}";
            string fullPath = Path.Combine(_environment.WebRootPath, "uploads", fileName);
            using (FileStream stream = new FileStream(fullPath, FileMode.CreateNew))
            {
                myFile.CopyTo(stream);
            }

            ImagedbManager mgr = new ImagedbManager(_connectionstring);
            I.FileName = fileName;
            mgr.AddImage(I);

            return Redirect("/");
        }

        public IActionResult Images()
        {
            ImagedbManager mgr = new ImagedbManager(_connectionstring);
            IEnumerable<Image> images = mgr.GetImages();
            return View(images);
        }

        [HttpPost]
        public IActionResult Image(int imageid)
        {
            ImagedbManager mgr = new ImagedbManager(_connectionstring);
            ImageViewMdl Model = new ImageViewMdl();
            Model.image = mgr.GetImageById(imageid);
            Model.LikedImage = true;
            return View(Model);
        }

        public void AddLikeToImage(int iid)
        {
            ImagedbManager mgr = new ImagedbManager(_connectionstring);
            mgr.AddLikeToImage(iid);
        }

        public int GetImageLikeCount(int id)
        {
            ImagedbManager mgr = new ImagedbManager(_connectionstring);
            return mgr.GetNumberOfLikesForImage(id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
