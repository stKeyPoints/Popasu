using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Popasu.Domain.Model;
using Popasu.Infrastructure.Data;

namespace Popasu.Infrastructure.Repository
{
    public class ParameterRepository
    {
        private readonly Context _context;

        public ParameterRepository(Context context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        // Получение всех параметров
        public List<Parameter> GetAll()
        {
            return _context.Parameters.Include(p => p.ParameterValue).ToList();
        }


        // Получение параметра по Id
        public Parameter GetById(Guid id)
        {
            return _context.Parameters.Include(p => p.ParameterValue).FirstOrDefault(p => p.Id == id);
        }

        // Получение параметров по списку Id
        public List<Parameter> GetByIds(List<Guid> ids)
        {
            return _context.Parameters.Include(p => p.ParameterValue)
                                      .Where(p => ids.Contains(p.Id))
                                      .ToList();
        }


        // Добавление параметра
        public Parameter Add(Parameter parameter)
        {
            Parameter newParameter = _context.Parameters.Add(parameter).Entity;
            _context.SaveChanges();

            return newParameter;
        }

        // Удаление параметра
        public void Delete(Parameter parameter)
        {
            _context.Parameters.Remove(parameter);
            _context.SaveChanges();
        }

        // Обновление параметра
        public Parameter Update(Parameter parameter)
        {
            Parameter oldParameter = GetById(parameter.Id);
            if (oldParameter == null)
            {
                throw new Exception("NotFound");
            }

            oldParameter.Name = parameter.Name;
            oldParameter.Unit = parameter.Unit;
            oldParameter.Year = parameter.Year;
            oldParameter.ParameterValue = parameter.ParameterValue; // Изменил эту строку

            _context.SaveChanges();

            return oldParameter;
        }
    }
}
