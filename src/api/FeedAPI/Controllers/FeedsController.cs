using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedAPI.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace FeedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedsController : ControllerBase
    {
        private IApiService _service;
        public FeedsController(Func<Type, IApiService> serviceAccessor)
        {
            this._service = serviceAccessor(this.GetType());
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                this._service.Load();

                return this._service.Get();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("top/{quantity}")]
        public IActionResult GetTop([FromRoute] int quantity)
        {
            try
            {
                this._service.Load();

                return this._service.GetTop(quantity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
