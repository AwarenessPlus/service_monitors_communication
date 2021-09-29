using Emgu.CV;
using Emgu.CV.Structure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using monitors_comunication.Models;
using monitors_comunication.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace monitors_comunication.Controllers
{
    // https://localhost:44303/MonitorsCommunication
    [Route("api/[controller]")]
    [ApiController]
    public class MonitorsCommunicationController : ControllerBase
    {

        MonitorVitalsSignsConnectionByVideo monitorConection = new();

        [Route("")]
        [HttpGet]
        public IActionResult connectMonitor()
        {
            monitorConection.ConnectMonitor();
            VitalSigns<Bitmap> monitor = monitorConection.GetDataMonitor();
            var image = System.IO.File.OpenRead("C:\\Users\\jmarinflorez\\Documents\\Uni\\service_communication_monitors\\service_monitors_communication\\monitors_comunication\\bin\\Debug\\net5.0\\video\\hola1.png");
            return File(image, "image/png");

         
        }

        [Route("/connect")]
        [HttpGet]
        public IActionResult GetHealth() {

            return Ok("System connected");

        }

    }
}

//"api/awareness-plus/monitors-communication/"