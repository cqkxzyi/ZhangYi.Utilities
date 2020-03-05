﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Core3._0_Web.Models;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;

namespace Core3._0_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServerAddressesFeature _serverAddressesFeature;
        private readonly IHttpContextAccessor _httpContextAccessor;

       public HomeController(ILogger<HomeController> logger, IServerAddressesFeature serverAddressesFeature, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _serverAddressesFeature = serverAddressesFeature;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //[Route("api/[controller]")]
        public IActionResult GetServiceAddres()
        {
            var ip = _serverAddressesFeature.Addresses.FirstOrDefault();
            return Json("本机IP："+ ip);
        }

        [HttpGet]
        public IActionResult GetRemoteIpAddress()
        {
            var ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            return Json("客户端IP：" + ip);
        }


    }
}
