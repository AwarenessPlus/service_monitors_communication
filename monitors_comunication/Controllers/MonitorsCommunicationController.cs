using Emgu.CV;
using Emgu.CV.Structure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using monitors_comunication.Models;
using monitors_comunication.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using monitors_comunication.Configuration;

namespace monitors_comunication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitorsCommunicationController : ControllerBase
    {

        private readonly IMonitorConnection<FileStream> _monitorConnection;
        
       

        public MonitorsCommunicationController(IMonitorConnection<FileStream> monitorConnection)
        {
            this._monitorConnection = monitorConnection;

        }

        [Route("/health-status")]
        [HttpGet]
        public IActionResult GetHealth()
        {
            return Ok("Service connected");
        }


        [Route("/Cardiac-frecuency")]
        [HttpGet]
        public async Task<IActionResult> GetCardiacFrecuency()
        {

            var image = _monitorConnection.GetDataMonitor(Configuration.Configuration.CARDIAC_FRECUENCY);
            return File(await image, "image/png");


         
        }

        [Route("/saturation")]
        [HttpGet]
        public async Task<IActionResult> GetSaturation()
        {
            var image = _monitorConnection.GetDataMonitor(Configuration.Configuration.SATURATION);
            return File(await image, "image/png");
            




        }


        [Route("/non-invasive-blood-presure")]
        [HttpGet]
        public async Task<IActionResult> GetNonInvasiveBloodPresure()
        {
            var image = _monitorConnection.GetDataMonitor(Configuration.Configuration.NON_INVASIVE_BLOOD_PRESURE);
            return File(await image, "image/png");




        }


        [Route("/connect")]
        [HttpGet]
        public  IActionResult ConnectMonitor() {

            Boolean hilo = _monitorConnection.ConnectMonitor();
            if (!hilo)
            {
                return Ok(" The sistem is processing right now another monitor, cancel connection to allow a new connection");
            }
            else
            {
                return Ok("System connected");
            }

        }

        [Route("/disconnect")]
        [HttpGet]
        public IActionResult DisconnectMonitor()
        {
            if (_monitorConnection.DisconnectMonitor())
            {
                return Ok("System disconnected");
            }
            return Ok("There is no monitor connected");
        



    }

    }
}

//"api/awareness-plus/monitors-communication/"