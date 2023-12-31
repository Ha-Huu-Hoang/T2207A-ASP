﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T2207A_API.DTOs;
using T2207A_API.Entities;
using T2207A_API.Model;

namespace T2207A_API.Controllers
{
    [ApiController]
    [Route("/api/product")]
    public class ProductController : ControllerBase
    {
        private readonly T2207aApiContext _context;
        public ProductController(T2207aApiContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Product> products = _context.Products.ToList();

            List<ProductDTO> data = new List<ProductDTO>();
            foreach (Product p in products)
            {
                data.Add(new ProductDTO { id = p.Id, name = p.Name, price = p.Price, qty = p.Qty, description = p.Description, thumbnai = p.Thumbnall, category = p.CategoryId });
            }
            return Ok(data);
        }

        [HttpGet]
        [Route("get-by-id")]
        public IActionResult Get(int id)
        {
            try
            {
                Product p = _context.Products.Find(id);
                if (p != null)
                {
                    return Ok(new ProductDTO { id = p.Id, name = p.Name, price = p.Price, qty = p.Qty, description = p.Description, thumbnai = p.Thumbnall, category = p.CategoryId });
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return NotFound();
        }
        [HttpGet]
        [Route("get-by-categoryId")]
        public IActionResult GetbyCategory(int categoryId)
        {
            try
            {
                List<Product> products = _context.Products.Where(p => p.CategoryId == categoryId).ToList();
                if (products != null)
                {
                    List<ProductDTO> data = products.Select(c => new ProductDTO
                    {
                        id = c.Id,
                        name = c.Name,
                        price = c.Price,
                        description = c.Description,
                        thumbnai = c.Thumbnall,
                        qty = c.Qty,
                        category = c.CategoryId
                    }).ToList();

                    return Ok(data);
                }
                else
                {
                    return NotFound("Không tìm thấy sản phẩm trong danh mục này.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public IActionResult Create(CreateProduct model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Product data = new Product { Name = model.name, Price = model.price, Qty = model.qty, Description = model.description, Thumbnall = model.thumbnai, CategoryId = model.category };
                    _context.Products.Add(data);
                    _context.SaveChanges();
                    return Created($"get-by-id?id={data.Id}", new ProductDTO { id = data.Id, name = data.Name, price = data.Price, qty = data.Qty, description = data.Description, thumbnai = data.Thumbnall, category = data.CategoryId });
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            var msgs = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
            return BadRequest(string.Join(" | ", msgs));
        } 
        [HttpPut]
        public IActionResult Update(EditProduct model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Product product = new Product { Id = model.id, Name = model.name, Price = model.price, Qty = model.qty, Description = model.description, Thumbnall = model.thumbnai, CategoryId = model.category };
                    if (product != null)
                    {
                        _context.Products.Update(product);
                        _context.SaveChanges();
                        return NoContent();
                    }
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);

                }

            }
            return BadRequest();
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                Product product = _context.Products.Find(id);
                if (product == null)
                    return NotFound();
                _context.Products.Remove(product);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

