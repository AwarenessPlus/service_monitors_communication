using Emgu.CV;
using Emgu.CV.Structure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using monitors_comunication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace monitors_comunication.Controllers
{
    // https://localhost:44303/MonitorsCommunication
    [Route("api/[controller]")]
    [ApiController]
    public class MonitorsCommunicationController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetHealth() {

            MonitorVitalsSignsConnectionByVideo<Image<Bgr,byte>> monitor = new();
            monitor.ConnectMonitor();
            return Ok(monitor.GetDataMonitor());
        }

    }
}

//"api/awareness-plus/monitors-communication/"