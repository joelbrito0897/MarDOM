using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;
using Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using Model;
using Model.DTO;
using Model.ViewModel;

namespace MarDOM.Controllers
{
    public class ArticlesController : Controller
    {

        private readonly IArticleService _service;
        private readonly ICategoryService _categoryService;
        private readonly IWarehouseService _warehouseService;
        private readonly IMapper _mapper;
        public ArticlesController(IArticleService service,ICategoryService categoryService, IWarehouseService warehouseService, IMapper mapper)
        {
            _service = service;
            _categoryService = categoryService;
            _warehouseService = warehouseService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var query =  _service.GetList();
            var model = await PagingList.CreateAsync(query, 10, page,"Name","Name");

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Add() {

            var category = await _categoryService.GetList();
            var lista = category.Select(a=>new SelectListItem() { 
                Value=a.Id.ToString(),
                Text=a.Name
            }).ToList();
            ViewBag.CategoryId = lista;
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Add(Article model)
        {
            if (model == null) return BadRequest();
            if (ModelState.IsValid)
            {
                var result = await _service.Add(model);
                return Ok(result);
            }
            return BadRequest();
        }

        [Route("api/Article/Update")]
        [HttpPost]
        public async Task<IActionResult> Update(ArticleDTO model)
        {
            if (model == null) return BadRequest();
            var article = await _service.GetById(model.Id);
            var entry = _mapper.Map<ArticleDTO,Article>(model,article);
            var result = await _service.Update(entry);
            return Ok(result);
        }

        [Route("api/Article/Disable")]
        [HttpPost]
        public async Task<IActionResult> Disable(Guid Id)
        {
            if (Id == null) return BadRequest();
            var result = await _service.Disable(Id);
            return Ok(result);
        }

        public IActionResult Movement()
        {
            var warehouse = _warehouseService.GetAll();
            var list = warehouse.ToList().Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToList();
           
            ViewBag.WarehouseList = list;
            return View();
        }

        [Route("api/Article/GetAll")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var Article = _service.GetAll();
            return Ok(Article.Select(x => new { x.Id, x.Name }));
        }
    }
}