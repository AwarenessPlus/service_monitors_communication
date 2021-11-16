# service_monitors_comunication
This API REST service made in .NET CORE manage the comunication with the monitors to another services from Awareness+

The service exposes the followings endpoins: 

## Endpoints

###  Heath Status

 >  /health-status 

This endpoint allows the client proof if the service are working goood.


###  Get Cardiac Frecuency 
 > /Cardiac-frecuency

This endpoint allow to know  the current cardiac frecuency from vital singns monitor

### Saturation
 >  /saturation

This endpoint allow to know the current saturation from vital singns monitor

### Non invasive blood presure
 >  /non-invasive-blood-presure

This endpoint allow to know  the current sNon invasive blood presure from vital singns monitor

### Connect
 >  /connect

This endpoint allows connect the service to the vital signs monitor

### Disconnect
 >  /disconnect

This endpoint allows disconnect the service to the vital signs monitor

# Installation 

## Enviroment
you need to downlowad or check the following things depending on your development environment

### Visual Studio
- Visual Studio 2019 with the ASP.NET and web development workload.


### Visual Studio Code

- Visual Studio Code
- C# for Visual Studio
- .NET 5.0 SDK

### Visual Studio for Mac

- For Visual Studio for Mac .NET 5.0.

## On visual studio 

### NuGet

You need install the following NuGets for work properly: 

* Emgu.CV [from Emgu Corporation](https://emgu.com/wiki/index.php/Main_Page)
* Emgu.CV.Bitmap [from Emgu Corporation](https://emgu.com/wiki/index.php/Main_Page)
* Emgu.CV.runtime.windows [from Emgu Corporation](https://emgu.com/wiki/index.php/Main_Page)
* Swashbuckle.AspNetCore [from domaindrivendev ](https://www.nuget.org/packages/Swashbuckle.AspNetCore/5.6.3?_src=template)
* System.Drawing.Common [ from Microsoft ](https://www.nuget.org/packages/System.Drawing.Common/5.0.2?_src=template )


## For documentation 


The service  include Swagger which allows you to view and test each endpoint. [Swagger]( https://swagger.io/) 


