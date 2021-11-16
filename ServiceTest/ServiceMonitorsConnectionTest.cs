using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using monitors_comunication.Controllers;
using System.IO;
using monitors_comunication.Services;
using Moq;
using System.Threading.Tasks;

namespace ServiceTest
{
    public class ServiceMonitorsConnectionTest
    {
        MonitorsCommunicationController _controller;
        Mock<IMonitorConnection<FileStream>> monitorConnection;

        public ServiceMonitorsConnectionTest()

        {
            monitorConnection  = new Mock<IMonitorConnection<FileStream>>();
            _controller = new(monitorConnection.Object);
        }

        [Fact]
        public void ok_GetHealthStatus_withResults()
        {
            IActionResult okResult = _controller.GetHealth();
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void ok_ConnectMonitor_withResults()
        {
            monitorConnection.Setup(P => P.ConnectMonitor()).Returns(true);
            IActionResult okResult = _controller.ConnectMonitor();
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }


        [Fact]
        public void ok_DisconnectMonitor_withResults()
        {
            monitorConnection.Setup(P => P.DisconnectMonitor()).Returns(true);
            IActionResult okResult = _controller.DisconnectMonitor();
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }






    }

}
