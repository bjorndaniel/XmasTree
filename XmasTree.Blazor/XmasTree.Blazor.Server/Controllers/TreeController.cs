using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using XmasTree.Common;

namespace XmasTree.Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    public class TreeController : Controller
    {
        private Gpio _gpio = new Gpio();

        [HttpPost("[action]")]
        public ActionResult Update([FromBody]List<LedBlink> lights)
        {
            _gpio.Off();
            foreach(var l in lights)
            {
                _gpio.WritePin((int)l.GpioPin, l.IsOn);
            }
            return Ok();
        }
    }
}
