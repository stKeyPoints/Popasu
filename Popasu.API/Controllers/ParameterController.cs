using Microsoft.AspNetCore.Mvc;
using Popasu.Infrastructure;
using Popasu.Infrastructure.Repository;
using Popasu.Domain.Model;
using Popasu.Domain.DTOs;
using Popasu.Infrastructure.Data;

namespace Popasu.API.Controllers
{
    [ApiController]
    [Route("parameter")]
    public class ParameterController : ControllerBase
    {
        private readonly Context _context;
        private readonly ParameterRepository _parameterRepository;
        private readonly ParameterValueRepository _parameterValueRepository;

        public ParameterController(Context context)
        {
            _context = context;
            _parameterRepository = new ParameterRepository(context);
            _parameterValueRepository = new ParameterValueRepository(context);
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var parameters = _parameterRepository.GetAll();
            var parameterDTOs = parameters.Select(p => new ParameterDTO
            {
                Id = p.Id,
                Name = p.Name,
                Unit = p.Unit,
                Year = p.Year,
                ParameterValue = p.ParameterValue?.Value.ToString() ?? "N/A" // Изменил эту строку
            }).ToList();

            return Ok(parameterDTOs);
        }

        [HttpGet("{guid}")]
        public IActionResult GetById(Guid guid)
        {
            var parameter = _parameterRepository.GetById(guid);
            if (parameter == null)
            {
                return NotFound();
            }

            var parameterDTO = new ParameterDTO
            {
                Id = parameter.Id,
                Name = parameter.Name,
                Unit = parameter.Unit,
                Year = parameter.Year,
                ParameterValue = parameter.ParameterValue?.Value.ToString() ?? "N/A"
            };

            return Ok(parameterDTO);
        }

        [HttpPost]
        public IActionResult Create(ParameterCreateDTO parameterDTO)
        {
            var parameterValue = _parameterValueRepository.GetById(parameterDTO.ParameterValueId);
            if (parameterValue == null)
            {
                return NotFound("ParameterValue is not found");
            }

            var parameter = new Parameter
            {
                Name = parameterDTO.Name,
                Unit = parameterDTO.Unit,
                Year = parameterDTO.Year,
                ParameterValue = parameterValue
            };

            var newParameter = _parameterRepository.Add(parameter);
            var parameterDTOResponse = new ParameterDTO
            {
                Id = newParameter.Id,
                Name = newParameter.Name,
                Unit = newParameter.Unit,
                Year = newParameter.Year,
                ParameterValue = newParameter.ParameterValue?.Value.ToString() ?? "N/A"
            };

            return Ok(parameterDTOResponse);
        }

        [HttpPut]
        public IActionResult Update(ParameterUpdateDTO parameterDTO)
        {
            var oldParameter = _parameterRepository.GetById(parameterDTO.Id);
            if (oldParameter == null)
            {
                return NotFound();
            }

            var parameterValue = _parameterValueRepository.GetById(parameterDTO.ParameterValueId);
            if (parameterValue == null)
            {
                return NotFound("ParameterValue is not found");
            }

            var updatedParameter = new Parameter
            {
                Id = parameterDTO.Id,
                Name = parameterDTO.Name,
                Unit = parameterDTO.Unit,
                Year = parameterDTO.Year,
                ParameterValue = parameterValue
            };

            var result = _parameterRepository.Update(updatedParameter);
            var parameterDTOResponse = new ParameterDTO
            {
                Id = result.Id,
                Name = result.Name,
                Unit = result.Unit,
                Year = result.Year,
                ParameterValue = result.ParameterValue?.Value.ToString() ?? "N/A"
            };

            return Ok(parameterDTOResponse);
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var oldParameter = _parameterRepository.GetById(guid);
            if (oldParameter == null)
            {
                return NotFound();
            }

            _parameterRepository.Delete(oldParameter);
            return Ok();
        }
    }
}
