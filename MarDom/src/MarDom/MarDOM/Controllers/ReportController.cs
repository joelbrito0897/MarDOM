using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using Service.Interfaces;

namespace MarDOM.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("api/Report/ForWarehouse/{warehouseID}")]
        [HttpGet]
        public async Task<IActionResult> ForWarehouse(Guid warehouseID)
        {
            var data = await _service.GetMovementForWarehouse(warehouseID);
            return new ViewAsPdf(data);
        }
        [Route("api/Report/ForInOrOut/{inOut}")]
        [HttpGet]
        public async Task<IActionResult> ForInOrOut(int inOut)
        {
            var data = await _service.GetMovementForInOrOut(inOut);
            return new ViewAsPdf(data);
        }
    }
}