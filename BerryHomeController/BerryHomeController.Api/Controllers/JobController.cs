﻿using System;
using System.Linq;
using BerryHomeController.Api.Contracts;
using BerryHomeController.Api.Extensions;
using BerryHomeController.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace BerryHomeController.Api.Controllers
{
    [Route("api/Job")]
    public class JobController : Controller
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IScheduleManager _scheduleManager;

        public JobController(ILoggerManager logger, IRepositoryWrapper repoWrapper, IScheduleManager scheduleManager)
        {
            _logger = logger;
            _repoWrapper = repoWrapper;
            _scheduleManager = scheduleManager;
        }

        [HttpGet]
        public IActionResult GetAllJobs()
        {
            try
            {
                var job = _repoWrapper.Job.GetAllJobs();
                _logger.LogInfo("Returned all jobs from database.");
                return Ok(job);
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong in GetAllJobs action: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id}", Name = "JobById")]
        public IActionResult GetJobById(Guid id)
        {
            try
            {
                var job = _repoWrapper.Job.GetJobById(id);
                if (job.IsEmptyObject())
                {
                    _logger.LogError($"Job with ID: {id} couldn't be found.");
                    return NotFound("Job with such ID couldn't be found.");
                }

                _logger.LogInfo($"Returned job with id: {id}");
                return Ok(job);
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong in GetJobById action: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        public IActionResult CreateJob([FromBody] Job job)
        {
            try
            {
                if (job.IsObjectNull())
                {
                    _logger.LogError($"Job object sent from client is null.");
                    return BadRequest($"Job object is null.");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError($"Job object sent from client is invalid.");
                    return BadRequest($"Job object is invalid.");
                }

                var device = _repoWrapper.Devices.GetDeviceById(job.DeviceId);
                if (device.IsObjectNull())
                {
                    _logger.LogError($"Device with ID: {job.DeviceId} couldn't be found.");
                    return NotFound("Device with such ID couldn't be found.");
                }

                job.Id = _scheduleManager.AddJob(device, job.Start, job.End, job.DaysList.ToArray());
                _repoWrapper.Job.CreateJob(job);
                return CreatedAtRoute("JobById", new { id = job.Id }, job);
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong inside CreateJob action: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateJob(Guid id, [FromBody] Job job)
        {
            try
            {

                if (job.IsObjectNull())
                {
                    _logger.LogError($"Job object sent from client is null.");
                    return BadRequest($"Job object is null.");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError($"Job object sent from client is invalid.");
                    return BadRequest($"Job object is invalid.");
                }

                var dbJob = _repoWrapper.Job.GetJobById(id);
                if (dbJob.IsEmptyObject())
                {
                    _logger.LogError($"Job with ID: {id} couldn't be found.");
                    return NotFound("Job with such ID couldn't be found.");
                }

                _repoWrapper.Job.UpdateJob(dbJob, job);
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong inside UpdateJob action: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteJob(Guid id)
        {
            try
            {
                var job = _repoWrapper.Job.GetJobById(id);
                if (job.IsEmptyObject())
                {
                    _logger.LogError($"Job with ID: {id} couldn't be found.");
                    return NotFound("Job with such ID couldn't be found.");
                }

                _repoWrapper.Job.DeleteJob(job);
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong in DeleteJob action: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
