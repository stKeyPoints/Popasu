using Microsoft.AspNetCore.Mvc;
using Popasu.Infrastructure;
using Popasu.Infrastructure.Repository;
using Popasu.Domain.Model;
using Popasu.Domain.DTOs;
using Popasu.Infrastructure.Data;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Popasu.API.Controllers
{
    [ApiController]
    [Route("windrose")]
    public class WindRoseController : ControllerBase
    {
        private readonly Context _context;
        private readonly WindRoseRepository _windRoseRepository;
        private readonly ParameterRepository _parameterRepository;

        public WindRoseController(Context context)
        {
            _context = context;
            _windRoseRepository = new WindRoseRepository(context);
            _parameterRepository = new ParameterRepository(context);
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var windRoses = _windRoseRepository.GetAll();
            var windRoseDTOs = windRoses.Select(w => new WindRoseDTO
            {
                Id = w.Id,
                Parameters = w.Parameters.Select(p => p.ParameterValue?.Value.ToString() ?? "N/A").ToList()
            }).ToList();

            return Ok(windRoseDTOs);
        }

        [HttpGet("{guid}")]
        public IActionResult GetById(Guid guid)
        {
            var windRose = _windRoseRepository.GetById(guid);
            if (windRose == null)
            {
                return NotFound();
            }

            var windRoseDTO = new WindRoseDTO
            {
                Id = windRose.Id,
                Parameters = windRose.Parameters.Select(p => p.ParameterValue?.Value.ToString() ?? "N/A").ToList()
            };

            return Ok(windRoseDTO);
        }

        [HttpPost]
        public IActionResult Create(WindRoseCreateDTO windRoseDTO)
        {
            var parameters = _parameterRepository.GetByIds(windRoseDTO.ParameterIDs);
            if (parameters.Count == 0)
            {
                return NotFound("Parameters are not found");
            }

            var windRose = new WindRose
            {
                Parameters = parameters
            };

            var newWindRose = _windRoseRepository.Add(windRose);
            var windRoseDTOResponse = new WindRoseDTO
            {
                Id = newWindRose.Id,
                Parameters = newWindRose.Parameters.Select(p => p.Name).ToList()
            };

            return Ok(windRoseDTOResponse);
        }

        [HttpPut]
        public IActionResult Update(WindRoseUpdateDTO windRoseDTO)
        {
            var oldWindRose = _windRoseRepository.GetById(windRoseDTO.Id);
            if (oldWindRose == null)
            {
                return NotFound();
            }

            var parameters = windRoseDTO.ParametersIDs.Select(Guid.Parse).ToList();
            if (parameters.Count == 0)
            {
                return NotFound("Parameters are not found");
            }

            // Обновляем параметры WindRose
            oldWindRose.Parameters.Clear();
            oldWindRose.Parameters.AddRange(_parameterRepository.GetByIds(parameters));

            var result = _windRoseRepository.Update(oldWindRose);
            var windRoseDTOResponse = new WindRoseDTO
            {
                Id = result.Id,
                Parameters = result.Parameters.Select(p => p.ParameterValue?.Value.ToString() ?? "N/A").ToList()
            };

            return Ok(windRoseDTOResponse);
        }


        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var oldWindRose = _windRoseRepository.GetById(guid);
            if (oldWindRose == null)
            {
                return NotFound();
            }

            _windRoseRepository.Delete(oldWindRose);
            return Ok();
        }
    }
}
