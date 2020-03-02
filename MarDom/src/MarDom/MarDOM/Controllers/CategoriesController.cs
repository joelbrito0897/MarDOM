using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model;
using Model.DTO;
using ReflectionIT.Mvc.Paging;
using Service.Interfaces;

namespace MarDOM.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _service;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService service,IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(int page = 1, string sortExpression = "Name")
        {
            var query = _service.GetAll();
            var model = await PagingList.CreateAsync(query, 5, page, sortExpression, "Name");
            return View(model);
        }
        [Route("api/Category/Add")]
        [HttpPost]
        public async Task<IActionResult> Add(Category model)
        {
            if (model == null) return BadRequest();
            if (ModelState.IsValid)
            {
                var result = await _service.Add(model);
                return Ok(result);
            }
            return BadRequest();
        }
        [Route("api/Category/Update")]
        [HttpPost]
        public async Task<IActionResult> Update(CategoryDTO model)
        {
            if (model == null) return BadRequest();
            var category = await _service.GetById(model.Id);
            var entry = _mapper.Map<CategoryDTO, Category>(model, category);
            var result = await _service.Update(entry);
            return Ok(result);
        }
        [Route("api/Category/GetById")]
        [HttpPost]
        public async Task<IActionResult> GetOne(Guid Id)
        {
            if (Id == null) return BadRequest();
            var warehouse = await _service.GetById(Id);
            return Ok(warehouse);
        }
        [Route("api/Category/Disable")]
        [HttpPost]
        public async Task<IActionResult> Disable(Guid Id)
        {
            if (Id == null) return BadRequest();
            var result = await _service.Disable(Id);
            return Ok(result);
        }
        [Route("api/Category/GetAll")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var category = _service.GetAll();
            return Ok(category.Select(x=> new { x.Id, x.Name }));
        }
    }
} 