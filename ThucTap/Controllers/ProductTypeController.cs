﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThucTap.Entities;
using ThucTap.Handle.Page;
using ThucTap.IServices;
using ThucTap.Payloads.Requests;
using ThucTap.Payloads.Requests.ProductType;
using ThucTap.Services;
using ThucTap.Services.Implement;
using ThucTap.Services.IServices;

namespace ThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeService _service;

        public ProductTypeController()
        {
            _service = new ProductTypeService();
        }
        [HttpPost("AddProductType")]
        public IActionResult AddProductType(AddProductTypeRequest request)
        {
            return Ok(_service.AddProductType(request));
        }
        [HttpPut("UpdateProductType")]
        public IActionResult UpdateProductType(UpdateProductTypeRequest request)
        {
            return Ok(_service.UpdateProductType(request));
        }
        [HttpDelete("DeleteProductType")]
        public IActionResult DeleteProductType(int id)
        {
            return Ok(_service.DeleteProductType(id));
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll([FromQuery] Pagination pagination)
        {
            return Ok(_service.GetAll(pagination));
        }
    }
}
