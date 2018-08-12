﻿using System;
using BerryHomeController.Api.Contracts;
using BerryHomeController.Api.Extensions;
using BerryHomeController.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace BerryHomeController.Api.Controllers
{
    [Route("api/Device")]
    public class DeviceController : Controller
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IDeviceService _devService;

        public DeviceController(ILoggerManager logger, 
                                IRepositoryWrapper repoWrapper,
                                IDeviceService devService)
        {
            _logger = logger;
            _repoWrapper = repoWrapper;
            _devService = devService;
        }

        [HttpGet]
        public IActionResult GetAllDevices()
        {
            try
            {
                var devices = _repoWrapper.Devices.GetAllDevices();
                _logger.LogInfo("Returned all devices from database.");
                return Ok(devices);
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong in GetAllDevices action: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id}", Name = "DeviceById")]
        public IActionResult GetDeviceById(Guid id)
        {
            try
            {
                var device = _repoWrapper.Devices.GetDeviceById(id);
                if (device.IsEmptyObject())
                {
                    _logger.LogError($"Device with ID: {id} couldn't be found.");
                    return NotFound("Device with such ID couldn't be found.");
                }

                _logger.LogInfo($"Returned device with id: {id}");
                return Ok(device);
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong in GetDeviceById action: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        public IActionResult CreateDevice([FromBody] Device device)
        {
            try
            {
                if (device.IsObjectNull())
                {
                    _logger.LogError($"Device object sent from client is null.");
                    return BadRequest($"Device object is null.");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError($"Device object sent from client is invalid.");
                    return BadRequest($"Device object is invalid.");
                }

                _devService.RegisterDevice(device);
                return CreatedAtRoute("DeviceById", new { id = device.Id }, device);
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong inside CreateDevice action: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDevice(Guid id, [FromBody] Device device)
        {
            try
            {
                
                if (device.IsObjectNull())
                {
                    _logger.LogError($"Device object sent from client is empty.");
                    return BadRequest($"Device object is null.");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError($"Device object sent from client is invalid.");
                    return BadRequest($"Device object is invalid.");
                }

                var dbDevice = _repoWrapper.Devices.GetDeviceById(id);
                if (dbDevice.IsEmptyObject())
                {
                    _logger.LogError($"Device with ID: {id} couldn't be found.");
                    return NotFound("Device with such ID couldn't be found.");
                }

                _devService.UpdateDevice(id, dbDevice, device);
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong inside UpdateDevice action: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDevice(Guid id)
        {
            try
            {
                var device = _repoWrapper.Devices.GetDeviceById(id);
                if (device.IsEmptyObject())
                {
                    _logger.LogError($"Device with ID: {id} couldn't be found.");
                    return NotFound("Device with such ID couldn't be found.");
                }

                _devService.DeregisterDevice(device);
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong in DeleteDevice action: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}