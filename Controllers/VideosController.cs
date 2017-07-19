using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class VideosController : Controller
    {
        private ApplicationDbContext _dbcontext;

        public VideosController()
        {
            _dbcontext = new ApplicationDbContext();
        }
        // GET: Videos
        public ActionResult Index()
        {
            var videos = _dbcontext.Videos.ToList();
            return View(videos);
        }
        public ActionResult New()
        {
            return View();
        }
        public ActionResult Add(Video video)
        {
            _dbcontext.Videos.Add(video);
            _dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var video = _dbcontext.Videos.SingleOrDefault(v => v.ID == id);

            if (video == null)
                return HttpNotFound();

            return View(video);
        }

        [HttpPost]
        public ActionResult Update(Video video)
        {
            var videoInDb = _dbcontext.Videos.SingleOrDefault(v => v.ID == video.ID);

            if (videoInDb == null)
                return HttpNotFound();

            videoInDb.Title = video.Title;
            videoInDb.Description = video.Description;
            videoInDb.Genre = video.Genre;
            _dbcontext.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var video = _dbcontext.Videos.SingleOrDefault(v => v.ID == id);

            if (video == null)
                return HttpNotFound();

            return View(video);
        }
        [HttpPost]
        public ActionResult DoDelete(int id)
        {
            var video = _dbcontext.Videos.SingleOrDefault(v => v.ID == id);
            if (video == null)
                return HttpNotFound();

            _dbcontext.Videos.Remove(video);
            _dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}