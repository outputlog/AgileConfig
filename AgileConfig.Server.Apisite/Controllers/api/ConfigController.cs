﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgileConfig.Server.Apisite.Models;
using AgileConfig.Server.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AgileConfig.Server.Apisite.Controllers.api
{
    [Route("api/[controller]")]
    public class ConfigController : Controller
    {
        private readonly IConfigService _configService;
        private readonly ILogger _logger;

        public ConfigController(IConfigService configService, ILoggerFactory loggerFactory)
        {
            _configService = configService;
            _logger = loggerFactory.CreateLogger<ConfigController>();
        }
        // GET: api/<controller>
        [HttpGet("app/{appId}")]
        public async Task<List<ConfigVM>> Get(string appId)
        {
            var configs = await _configService.GetByAppId(appId);

            var vms = configs.Select(c => {
                return new ConfigVM() {
                    Id = c.Id,
                    AppId = c.AppId,
                    Group = c.Group,
                    Key = c.Key,
                    Value = c.Value,
                    Status = c.Status
                };
            });

            _logger.LogTrace($"get app {appId} configs .");

            return vms.ToList();
        }
       
    }
}
