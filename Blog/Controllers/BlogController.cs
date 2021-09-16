using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnviroment;

        public BlogController(ApplicationDbContext db, IWebHostEnvironment hostEnviroment)
        {
            _db = db;
            this._hostEnviroment = hostEnviroment;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> New(int? id)
        {
            BlogItem blog = new BlogItem();
            if (id == null)
            {
                return View(blog);
            }
            else
            {
                blog = await _db.Blog.FindAsync(id);
                return View(blog);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(BlogItem blog)
        {
            if (ModelState.IsValid)
            {

                if (blog.UploadedImage != null)
                {
                    string wwwRoot = _hostEnviroment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(blog.UploadedImage.FileName);
                    string extension = Path.GetExtension(blog.UploadedImage.FileName);
                    blog.blogUrlImage = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRoot + "/img/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await blog.UploadedImage.CopyToAsync(fileStream);
                    }
                }


                if (blog.blogId == 0)//crea el registro
                {
                    await _db.Blog.AddAsync(blog);
                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(New));
                }
                else
                {
                    _db.Blog.Update(blog);
                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(New), new { id = 0 });
                }
            }

            return View(blog);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var blogs = await _db.Blog.ToListAsync();

            return Json(new { data = blogs });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var blog = await _db.Blog.FindAsync(id);
            if (blog != null)
            {

                _db.Blog.Remove(blog);
                await _db.SaveChangesAsync();
                return Json(new { success = true, message = "Blog Delete Succesfully!" });
            }
            else {
                return Json(new { success = false, message = "Error!" });
            }

            //return  RedirectToAction(nameof(New), new { id = 0 });

        }
    }
}


