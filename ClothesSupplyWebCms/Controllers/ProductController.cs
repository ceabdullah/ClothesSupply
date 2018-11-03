using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClothesSupplyWebCms.Models;
using ClothesSupplyWebCms.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothesSupplyWebCms.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly IFilesService _filesService;

        public ProductController(IProductsService productsService, IFilesService filesService)
        {
            _productsService = productsService;
            _filesService = filesService;
        }

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {

                    byte[] data = new byte[file.Length];
                    file.OpenReadStream().Read(data);

                    _filesService.PostFiles(new Models.Files
                    {
                        File = data,
                        FileContentType = file.ContentType,
                        FileExtension = System.IO.Path.GetExtension(file.FileName),
                        FileName = file.FileName
                    });
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Route("/file/{fileName}")]
        [HttpGet]
        public async Task<IActionResult> GetFile([FromRoute]string fileName)
        {
            Files file = await _filesService.GetFile(fileName);

            if (file == null)
            {
                return NotFound();
            }

            return File(file.File, file.FileContentType);
        }

    }
}