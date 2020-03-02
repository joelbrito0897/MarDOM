using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DTO;
using Model.Enum;
using Model.ViewModel;
using ReflectionIT.Mvc.Paging;
using Service.Interfaces;

namespace MarDOM.Controllers
{
    public class WarehouseController : Controller
    {
        private readonly IWarehouseService _service;
        private readonly IMapper _mapper;
        public WarehouseController(IWarehouseService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int page=1, string sortExpression = "Name")
        {
            var query = _service.GetAll();
            var model = await PagingList.CreateAsync(query, 5, page, sortExpression, "Name");
            return View(model);
        }
        [Route("api/Warehouse/QuantityAvailable")]
        [HttpPost]
        public async Task<IActionResult> QuantityAvailable(Guid Id)
        {
            if (Id == null) return BadRequest();
            var quantity = await _service.QuantityAvailable(Id);
            return Ok(quantity);           
            
        }
        [Route("api/Warehouse/Add")]
        [HttpPost]
        public async Task<IActionResult> Add(Warehouse model)
        {
            if (model == null) return BadRequest();
            if (ModelState.IsValid)
            {
                var result = await _service.Add(model);
                return Ok(result);
            }
            return BadRequest();
        }
        [Route("api/Warehouse/Update")]
        [HttpPost]
        public async Task<IActionResult> Update(WarehouseDTO model)
        {            
            if(model==null) return BadRequest();
            var warehouse = await _service.GetById(model.Id);
            var entry = _mapper.Map<WarehouseDTO, Warehouse>(model, warehouse);
            var result = await _service.Update(entry);
            return Ok(result);
        }
        [Route("api/Warehouse/GetById")]
        [HttpPost]
        public async Task<IActionResult> GetOne(Guid Id)
        {
            if (Id == null) return BadRequest();
            var warehouse = await _service.GetById(Id);
            return Ok(warehouse);
        }
        [Route("api/Warehouse/Disable")]
        [HttpPost]
        public async Task<IActionResult> Disable(Guid Id)
        {
            if (Id == null) return BadRequest();
            var result = await _service.Disable(Id);
            return Ok(result);
        }
        [Route("api/Warehouse/GetAll")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var warehouse = _service.GetAll();
            return Ok(warehouse.Select(x => new { x.Id, x.Name }));
        }

        [Route("api/Warehouse/IsAllowToInsert/{quantity}/{warehouseId}")]
        [HttpGet]
        public async Task<IActionResult> IsAllowedToInsert(int quantity,Guid warehouseId)
        {
            var result = await _service.IsAllowedToInsert(quantity, warehouseId);
            return Ok(result);
        }
        [Route("api/Warehouse/AddMovement")]
        [HttpPost]
        public async Task<IActionResult> AddMovement(MovementArticleViewModel model)
        {
            bool result;
            var data = _mapper.Map<MovementArticle>(model);
            //try
            //{
            //    result = await _service.AddMovement(data);

            //    if (result)
            //    {
            //        if (data.Type.Equals(Convert.ToInt32(MoveType.Input)))
            //        {
            //            result = await _service.IncreaseCurrentQuantity(data.WarehouseId, data.MovementArticleDetails.Quantity);
            //        }
            //        else
            //        {
            //            result = await _service.DecreaseCurrentQuantity(data.WarehouseId, data.MovementArticleDetails.Quantity);
            //        }
            //    }
            //}
            //catch(Exception e)
            //{
            //    Console.WriteLine(e);
            //}
            result = await _service.AddMovement(data);

            if (result)
            {
                if (model.MovementType.Equals(Convert.ToInt32(MoveType.Input)))
                {
                   result = await _service.IncreaseCurrentQuantity(data.WarehouseId, data.MovementArticleDetails.Quantity);
                }
                else if(model.MovementType.Equals(Convert.ToInt32(MoveType.Output)))
                {
                   result = await _service.DecreaseCurrentQuantity(data.WarehouseId, data.MovementArticleDetails.Quantity);
                }
            }
            return Ok(result);
        }


    }
}