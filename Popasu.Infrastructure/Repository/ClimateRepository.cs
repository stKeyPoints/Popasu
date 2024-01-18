using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Popasu.Domain.Model;
using Popasu.Infrastructure.Data;

namespace Popasu.Infrastructure.Repository
{
    public class ClimateRepository
    {
        private readonly Context _context;

        public ClimateRepository(Context context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        // Получение всех Climate
        public List<Climate> GetAll()
        {
            return _context.Climates.Include(c => c.WindRose).Include(c => c.Parameters).ThenInclude(p => p.ParameterValue).ToList();
        }

        // Получение Climate по Id
        public Climate GetById(Guid id)
        {
            return _context.Climates.Include(c => c.WindRose).Include(c => c.Parameters).ThenInclude(p => p.ParameterValue).FirstOrDefault(c => c.Id == id);
        }

        // Добавление Climate
        public Climate Add(Climate climate)
        {
            Climate newClimate = _context.Climates.Add(climate).Entity;
            _context.SaveChanges();

            return newClimate;
        }

        // Удаление Climate
        public void Delete(Climate climate)
        {
            _context.Climates.Remove(climate);
            _context.SaveChanges();
        }

        // Обновление Climate
        public Climate Update(Climate climate)
        {
            Climate oldClimate = GetById(climate.Id);
            if (oldClimate == null)
            {
                throw new Exception("NotFound");
            }

            oldClimate.WindRose = climate.WindRose;
            oldClimate.Parameters = climate.Parameters; // Изменил эту строку

            _context.SaveChanges();

            return oldClimate;
        }
    }
}
