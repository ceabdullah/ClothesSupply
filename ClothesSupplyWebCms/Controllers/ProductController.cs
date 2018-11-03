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
        public async Task<IActionResult> Index()
        {
            var products = await _productsService.GetProduct();
            return View(products);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productsService.GetProduct(id);
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            try
            {
                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {

                    byte[] data = new byte[file.Length];
                    file.OpenReadStream().Read(data);

                    Files cFile = await _filesService.PostFiles(new Models.Files
                    {
                        File = data,
                        FileContentType = file.ContentType,
                        FileExtension = System.IO.Path.GetExtension(file.FileName),
                        FileName = file.FileName
                    });

                    await _productsService.PostProduct(new ProductDto
                    {
                        LastUpdated = DateTime.Now,
                        Name = collection["Name"],
                        Photo = "/file/" + cFile.FileName,
                        Price = Double.Parse(collection["Price"])
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
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productsService.GetProduct(id);
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection collection)
        {
            ProductDto cProduct = null;
            try
            {
                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {

                    byte[] data = new byte[file.Length];
                    file.OpenReadStream().Read(data);

                    Files cFile = await _filesService.PostFiles(new Models.Files
                    {
                        File = data,
                        FileContentType = file.ContentType,
                        FileExtension = System.IO.Path.GetExtension(file.FileName),
                        FileName = file.FileName
                    });

                    cProduct = await _productsService.PutProduct(id, new ProductDto
                    {
                        Id = id,
                        LastUpdated = DateTime.Now,
                        Name = collection["Name"],
                        Photo = "/file/" + cFile.FileName,
                        Price = Double.Parse(collection["Price"])
                    });
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(cProduct);
            }
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productsService.GetProduct(id);
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _productsService.DeleteProduct(id);

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