using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Popasu.Domain.Model;
using Popasu.Infrastructure.Data;

namespace Popasu.Infrastructure.Repository
{
    public class ParameterValueRepository
    {
        private readonly Context _context;

        public ParameterValueRepository(Context context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        // Получение всех значений параметра
        public List<ParameterValue> GetAll()
        {
            return _context.ParametersValues.ToList();
        }

        // Получение значения параметра по Id
        public ParameterValue GetById(Guid id)
        {
            return _context.ParametersValues.FirstOrDefault(p => p.Id == id);
        }

        // Добавление значения параметра
        public ParameterValue Add(ParameterValue parameterValue)
        {
            ParameterValue newParameterValue = _context.ParametersValues.Add(parameterValue).Entity;
            _context.SaveChanges();

            return newParameterValue;
        }

        // Удаление значения параметра
        public void Delete(ParameterValue parameterValue)
        {
            _context.ParametersValues.Remove(parameterValue);
            _context.SaveChanges();
        }

        // Обновление значения параметра
        public ParameterValue Update(ParameterValue parameterValue)
        {
            ParameterValue? oldParameterValue = GetById(parameterValue.Id);
            if (oldParameterValue == null)
            {
                throw new Exception("NotFound");
            }

            oldParameterValue.Value = parameterValue.Value;
            oldParameterValue.Date = parameterValue.Date;
            oldParameterValue.Month = parameterValue.Month;

            _context.SaveChanges();

            return oldParameterValue;
        }
    }
}
