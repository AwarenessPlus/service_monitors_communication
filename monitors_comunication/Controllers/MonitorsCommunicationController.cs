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

namespace monitors_comunication.Controllers
{
    // https://localhost:44303/MonitorsCommunication
    [Route("api/[controller]")]
    [ApiController]
    public class MonitorsCommunicationController : ControllerBase
    {

        MonitorVitalsSignsConnectionByVideo monitorConection = new();

        [Route("/Cardiac-frecunecy")]
        [HttpGet]
        public IActionResult GetCardiacFrecuency()
        {

           // monitorConection.GetDataMonitor();            
            var image = System.IO.File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + "\\video\\hola1.png");
            return File(image, "image/png");


         
        }

        [Route("/saturation")]
        [HttpGet]
        public IActionResult GetSaturation()
        {
           // monitorConection.GetDataMonitor();
            var image = System.IO.File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + "\\video\\hola2.png");
            return File(image, "image/png");



        }


        [Route("/non-invasive-blood-presure")]
        [HttpGet]
        public IActionResult GetNonInvasiveBloodPresure()
        {
            //monitorConection.GetDataMonitor();
            var image = System.IO.File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + "\\video\\hola3.png");
            return File(image, "image/png");



        }


        [Route("/connect")]
        [HttpGet]
        public IActionResult GetHealth() {

            ListennerMonitor listenner = new();
            Console.WriteLine("creating thread");
            Thread hilo = new(new ThreadStart(listenner.ListenerMonitores));
            hilo.Start();
            Console.WriteLine( "thread created");
            return Ok("System connected");

        }

    }
}

//"api/awareness-plus/monitors-communication/"