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
    [Route("api/[controller]")]
    [ApiController]
    public class MonitorsCommunicationController : ControllerBase
    {

        MonitorVitalsSignsConnectionByVideo monitorConection = new();
        ListennerMonitor listenner = new();
        static List<Thread> threads = new();


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

            var image = await Task.Run(() => System.IO.File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + "\\video\\hola1.png"));
            return File(image, "image/png");


         
        }

        [Route("/saturation")]
        [HttpGet]
        public async Task<IActionResult> GetSaturation()
        {

            var image = await Task.Run(() => System.IO.File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + "\\video\\hola2.png"));
            return File(image, "image/png");



        }


        [Route("/non-invasive-blood-presure")]
        [HttpGet]
        public async Task<IActionResult> GetNonInvasiveBloodPresureAsync()
        {
            var image =  await Task.Run(() => System.IO.File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + "\\video\\hola3.png"));
            
            return File(image, "image/png");



        }


        [Route("/connect")]
        [HttpGet]
        public  IActionResult GetHealthAsync() {

            Console.WriteLine(threads.Count);
            if (threads.Count > 0)
            {
                return BadRequest(" The sistem is processing right now another monitor, cancel connection to allow a new connection");
            }

            Console.WriteLine("creating thread");
            Thread hilo = new(new ThreadStart(listenner.ListenerMonitores));  
            hilo.Start();
            threads.Add(hilo);
            Console.WriteLine(threads.Count);
            Console.WriteLine( "thread created");
            return Ok("System connected");

        }

        [Route("/disconnect")]
        [HttpGet]
        public IActionResult DisconnectMonitor()
        {
            if (threads.Count > 0)
            {
                Thread thread = threads.First<Thread>();
                thread.Interrupt();
                threads.Clear();
                GC.Collect();
            }
            Console.WriteLine("thread fnished");
            return Ok("System disconnected");

        }

    }
}

//"api/awareness-plus/monitors-communication/"