using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using monitors_comunication.Controllers;

namespace ServiceTest
{
    public class ServiceMonitorsConnectionTest
    {
        MonitorsCommunicationController _controller;

        public ServiceMonitorsConnectionTest()

        {
            _controller = new();
        }

        [Fact]
        public void ok_GetHealthStatus_withResults()
        {
            IActionResult okResult = _controller.GetHealth();
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public async void ok_GetCardiacFrecuency_withResults()
        {
            FileStreamResult okResult = (FileStreamResult )await _controller.GetCardiacFrecuency();
            
           
            Assert.Equal("image/png", okResult.ContentType);
        }

        [Fact]
        public async void ok_GetSaturation_withResults()
        {
            FileStreamResult okResult = (FileStreamResult)await _controller.GetSaturation();

            Assert.Equal("image/png", okResult.ContentType);
        }

        [Fact]
        public async void ok_GetNonInvasiveBloodPresureAsync_withResults()
        {
            FileStreamResult okResult = (FileStreamResult)await _controller.GetNonInvasiveBloodPresureAsync();
            Assert.Equal("image/png", okResult.ContentType);
        }

        [Fact]
        public void ok_ConnectMonitor_withResults()
        {
            IActionResult okResult = _controller.ConnectMonitor();
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }


        [Fact]
        public void ok_DisconnectMonitor_withResults()
        {
            IActionResult okResult = _controller.DisconnectMonitor();
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }






    }

}
