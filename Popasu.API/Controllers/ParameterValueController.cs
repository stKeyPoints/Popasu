using Microsoft.AspNetCore.Mvc;
using Popasu.Infrastructure;
using Popasu.Infrastructure.Repository;
using Popasu.Domain.Model;
using Popasu.Domain.DTOs;
using Popasu.Infrastructure.Data;

namespace Popasu.API.Controllers
{
    [ApiController]
    [Route("parametervalue")]
    public class ParameterValueController : ControllerBase
    {
        private readonly Context _context;
        private readonly ParameterValueRepository _parameterValueRepository;

        public ParameterValueController(Context context)
        {
            _context = context;
            _parameterValueRepository = new ParameterValueRepository(context);
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var parameterValueDTOs = new List<ParameterValueDTO>();

            var parameterValues = _parameterValueRepository.GetAll();

            foreach (var parameterValue in parameterValues)
            {
                parameterValueDTOs.Add(new ParameterValueDTO
                {
                    Id = parameterValue.Id,
                    Value = parameterValue.Value,
                    Date = parameterValue.Date,
                    Month = parameterValue.Month,
                });
            }

            return Ok(parameterValueDTOs);
        }

        [HttpGet("{guid}")]
        public IActionResult GetById(Guid guid)
        {
            var parameterValue = _parameterValueRepository.GetById(guid);
            if (parameterValue == null)
            {
                return NotFound();
            }

            return Ok(new ParameterValueDTO
            {
                Id = parameterValue.Id,
                Value = parameterValue.Value,
                Date = parameterValue.Date,
                Month = parameterValue.Month,
            });
        }

        [HttpPost]
        public IActionResult Create(ParameterValueCreateDTO parameterValueDTO)
        {
            var newParameterValue = new ParameterValue
            {
                Value = parameterValueDTO.Value,
                Date = parameterValueDTO.Date,
                Month = parameterValueDTO.Month,
            };

            var createdParameterValue = _parameterValueRepository.Add(newParameterValue);

            return Ok(new ParameterValueDTO
            {
                Id = createdParameterValue.Id,
                Value = createdParameterValue.Value,
                Date = createdParameterValue.Date,
                Month = createdParameterValue.Month,
            });
        }

        [HttpPut]
        public IActionResult Update(ParameterValueUpdateDTO parameterValueDTO)
        {
            var oldParameterValue = _parameterValueRepository.GetById(parameterValueDTO.Id);
            if (oldParameterValue == null)
            {
                return NotFound();
            }

            oldParameterValue.Value = parameterValueDTO.Value;
            oldParameterValue.Date = parameterValueDTO.Date;
            oldParameterValue.Month = parameterValueDTO.Month;

            var updatedParameterValue = _parameterValueRepository.Update(oldParameterValue);

            return Ok(new ParameterValueDTO
            {
                Id = updatedParameterValue.Id,
                Value = updatedParameterValue.Value,
                Date = updatedParameterValue.Date,
                Month = updatedParameterValue.Month,
            });
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var oldParameterValue = _parameterValueRepository.GetById(guid);
            if (oldParameterValue == null)
            {
                return NotFound();
            }

            _parameterValueRepository.Delete(oldParameterValue);
            return Ok();
        }
    }
}
