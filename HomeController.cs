using CrumbCo.Models;
using CrumbCo.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrumbCo.Controllers;

public sealed class HomeController : Controller
{
    private const string AllCategories = "All";
    private readonly ProductCatalog catalog;

    public HomeController(ProductCatalog catalog)
    {
        this.catalog = catalog;
    }

    [HttpGet("/")]
    public IActionResult Index()
    {
        return View(catalog.Products.Where(product => product.Featured).ToList());
    }

    [HttpGet("/about")]
    public IActionResult About()
    {
        return View();
    }

    [HttpGet("/products")]
    public IActionResult Products(string? category)
    {
        var activeCategory = AllCategories;
        IEnumerable<Product> products = catalog.Products;

        if (!string.IsNullOrWhiteSpace(category) &&
            !string.Equals(category.Trim(), AllCategories, StringComparison.OrdinalIgnoreCase))
        {
            var matchingCategory = catalog.Products
                .Select(product => product.Category)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .FirstOrDefault(value => string.Equals(value, category.Trim(), StringComparison.OrdinalIgnoreCase));

            if (matchingCategory is not null)
            {
                activeCategory = matchingCategory;
                products = products.Where(product =>
                    string.Equals(product.Category, activeCategory, StringComparison.OrdinalIgnoreCase));
            }
        }

        ViewData["Category"] = activeCategory;
        return View(products.ToList());
    }

    [HttpGet("/gallery")]
    public IActionResult Gallery()
    {
        return View(catalog.Products.Where(product => product.Featured).ToList());
    }

    [HttpGet("/contact")]
    public IActionResult Contact()
    {
        return View();
    }

    [HttpGet("/enquiry")]
    public IActionResult Enquiry(string? product)
    {
        var matchingProduct = catalog.Products.FirstOrDefault(item =>
            string.Equals(item.Name, product?.Trim(), StringComparison.OrdinalIgnoreCase));

        ViewData["Product"] = matchingProduct?.Name ?? string.Empty;
        return View();
    }

    [HttpGet("/error")]
    public IActionResult Error()
    {
        Response.StatusCode = StatusCodes.Status500InternalServerError;
        return View();
    }

    [HttpGet("/not-found")]
    public IActionResult NotFoundPage()
    {
        Response.StatusCode = StatusCodes.Status404NotFound;
        return View("NotFound");
    }
}
