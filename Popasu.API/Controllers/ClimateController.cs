using Microsoft.AspNetCore.Mvc;
using Popasu.Infrastructure;
using Popasu.Infrastructure.Repository;
using Popasu.Domain.Model;
using Popasu.Domain.DTOs;
using Popasu.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Popasu.API.Controllers
{
    [ApiController]
    [Route("climate")]
    public class ClimateController : ControllerBase
    {
        private readonly Context _context;
        private readonly ClimateRepository _climateRepository;
        private readonly WindRoseRepository _windRoseRepository;
        private readonly ParameterRepository _parameterRepository;

        public ClimateController(Context context)
        {
            _context = context;
            _climateRepository = new ClimateRepository(context);
            _windRoseRepository = new WindRoseRepository(context);
            _parameterRepository = new ParameterRepository(context);
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var climates = _climateRepository.GetAll();
            var climateDTOs = climates.Select(c => new ClimateDTO
            {
                Id = c.Id,
                WindRose = new WindRoseDTO
                {
                    Id = c.WindRose?.Id ?? Guid.Empty,
                    Parameters = c.WindRose?.Parameters.Select(p => p.ParameterValue?.Value.ToString() ?? "N/A").ToList() ?? new List<string>()
                },
                Parameters = c.Parameters.Select(p => p.ParameterValue?.Value.ToString() ?? "N/A").ToList()
            }).ToList();

            return Ok(climateDTOs);
        }

        [HttpGet("{guid}")]
        public IActionResult GetById(Guid guid)
        {
            var climate = _climateRepository.GetById(guid);
            if (climate == null)
            {
                return NotFound();
            }

            var climateDTO = new ClimateDTO
            {
                Id = climate.Id,
                WindRose = new WindRoseDTO
                {
                    Id = climate.WindRose?.Id ?? Guid.Empty,
                    Parameters = climate.WindRose?.Parameters.Select(p => p.ParameterValue?.Value.ToString() ?? "N/A").ToList() ?? new List<string>()
                },
                Parameters = climate.Parameters.Select(p => p.ParameterValue?.Value.ToString() ?? "N/A").ToList()
            };

            return Ok(climateDTO);
        }

        [HttpPost]
        public IActionResult Create(ClimateCreateDTO climateDTO)
        {
            var windRose = _windRoseRepository.GetById(climateDTO.WindRoseId);
            if (windRose == null)
            {
                return NotFound("WindRose is not found");
            }

            var parameters = _parameterRepository.GetByIds(climateDTO.ParameterIDs);
            if (parameters.Count != climateDTO.ParameterIDs.Count)
            {
                return NotFound("One or more parameters are not found");
            }

            var climate = new Climate
            {
                WindRose = windRose,
                Parameters = parameters
            };

            var newClimate = _climateRepository.Add(climate);
            var climateDTOResponse = new ClimateDTO
            {
                Id = newClimate.Id,
                WindRose = new WindRoseDTO
                {
                    Id = newClimate.WindRose?.Id ?? Guid.Empty,
                    Parameters = newClimate.WindRose?.Parameters.Select(p => p.ParameterValue?.Value.ToString() ?? "N/A").ToList() ?? new List<string>()
                },
                Parameters = newClimate.Parameters.Select(p => p.ParameterValue?.Value.ToString() ?? "N/A").ToList()
            };

            return Ok(climateDTOResponse);
        }

        [HttpPut]
        public IActionResult Update(ClimateUpdateDTO climateDTO)
        {
            var oldClimate = _climateRepository.GetById(climateDTO.Id);
            if (oldClimate == null)
            {
                return NotFound();
            }

            var windRose = _windRoseRepository.GetById(climateDTO.WindRoseId);
            if (windRose == null)
            {
                return NotFound("WindRose is not found");
            }

            var parameters = _parameterRepository.GetByIds(climateDTO.ParameterIDs);
            if (parameters.Count != climateDTO.ParameterIDs.Count)
            {
                return NotFound("One or more parameters are not found");
            }

            var updatedClimate = new Climate
            {
                Id = climateDTO.Id,
                WindRose = windRose,
                Parameters = parameters
            };

            var result = _climateRepository.Update(updatedClimate);
            var climateDTOResponse = new ClimateDTO
            {
                Id = result.Id,
                WindRose = new WindRoseDTO
                {
                    Id = result.WindRose?.Id ?? Guid.Empty,
                    Parameters = result.WindRose?.Parameters.Select(p => p.ParameterValue?.Value.ToString() ?? "N/A").ToList() ?? new List<string>()
                },
                Parameters = result.Parameters.Select(p => p.ParameterValue?.Value.ToString() ?? "N/A").ToList()
            };

            return Ok(climateDTOResponse);
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var oldClimate = _climateRepository.GetById(guid);
            if (oldClimate == null)
            {
                return NotFound();
            }

            _climateRepository.Delete(oldClimate);
            return Ok();
        }
    }
}
