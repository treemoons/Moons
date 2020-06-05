using System.Net;
using System.ComponentModel;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using MyPersonalWeb.Models;
using ModelsLibrary.User;
using ModelsLibrary;
using CommonUtils;

namespace MyPersonalWeb.Controllers
{
    public class SectionController : PermissionController
    {

        private readonly ILogger<SectionController> _logger;

        public SectionController(ILogger<SectionController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Searched(string searchtext) =>
            await Task.Run(() =>
            {
                return View(nameof(Searched),searchtext);
            });
    }
}