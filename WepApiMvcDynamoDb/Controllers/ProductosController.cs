using Microsoft.AspNetCore.Mvc;
using WepApiMvcDynamoDb.Models;
using WepApiMvcDynamoDb.Services;

namespace WepApiMvcDynamoDb.Controllers
{
    public class ProductosController : Controller
    {
        private ServiceDynamoDB service;

        public ProductosController(ServiceDynamoDB service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Producto> productos = await this.service.GetProductoAsync();
            return View(productos);
        }

        public async Task<IActionResult> Details(string id)
        {
            Producto pro = await this.service.FindProductoAsync(id);
            return View(pro);
        }
        public async Task<IActionResult> Edit(string id)
        {
            Producto pro = await this.service.FindProductoAsync(id);
            if (pro == null)
            {
                return NotFound();
            }
            return View(pro);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Producto pro)
        {
            await this.service.UpdateProductoAsync(pro);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(string id)
        {
            await this.service.DeleteProductoAsync(id);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Producto pro)
        {
            await this.service.CreateProductoAsync(pro);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return RedirectToAction("Index");
            }

            List<Producto> productos = await this.service.GetProductoAsync();
            List<Producto> searchResults = productos.Where(p => p.Name.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            return View("Index", searchResults);
        }

    }
}
