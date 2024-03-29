﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.Extensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductService _productService;                          //ProductContreller IProductService'in ProductManager olduğunu bilmiyor
                      
        public ProductsController(IProductService productService) //ProductManager IProductDal ın EFProductDal olduğunu bilmiyor
        {                                                         //Dependency Injection Configuration yapmamız lazım
            _productService = productService;                     //Bu Dependency Yönetimi Autofac ile ayağa kaldıralacak
        }  //Dependency Injection desenini ayaklandırdık

        [HttpGet("getall")]

        //[Authorize(Roles ="Product.List")]

        public IActionResult GetList()
        {
            User.ClaimRoles(); //User Startup da Authorization middle waire i sayesinde geliyor.
                               //Bizim veri tabanındaki User nesnemiz değil ( ClaimPrincable )
                               //Bunu extend ettiğimden dolayı Extensionsda CLaimRoles geliyoyor Buradan erişebiliyorum
            var result = _productService.GetList();

            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getlistbycategory")]

        public IActionResult GetListByCategory(int categoryId)
        {
            var result = _productService.GetListByCategory(categoryId);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getbyid")]

        public IActionResult GetById(int productId)
        {
            var result = _productService.GetById(productId);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("add")]

        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);

            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("delete")]

        public IActionResult Delete(Product product)
        {
            var result = _productService.Delete(product);

            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("update")]

        public IActionResult Update(Product product)
        {
            var result = _productService.Update(product);

            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
