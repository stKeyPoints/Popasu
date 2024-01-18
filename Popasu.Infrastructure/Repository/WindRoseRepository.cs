using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Popasu.Domain.Model;
using Popasu.Infrastructure.Data;

namespace Popasu.Infrastructure.Repository
{
    public class WindRoseRepository
    {
        private readonly Context _context;

        public WindRoseRepository(Context context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        // Получение всех WindRose
        public List<WindRose> GetAll()
        {
            return _context.WindRoses.Include(w => w.Parameters).ThenInclude(p => p.ParameterValue).ToList();
        }

        // Получение WindRose по Id
        public WindRose GetById(Guid id)
        {
            return _context.WindRoses.Include(w => w.Parameters).ThenInclude(p => p.ParameterValue).FirstOrDefault(w => w.Id == id);
        }

        // Добавление WindRose
        public WindRose Add(WindRose windRose)
        {
            WindRose newWindRose = _context.WindRoses.Add(windRose).Entity;
            _context.SaveChanges();

            return newWindRose;
        }

        // Удаление WindRose
        public void Delete(WindRose windRose)
        {
            _context.WindRoses.Remove(windRose);
            _context.SaveChanges();
        }

        // Обновление WindRose
        public WindRose Update(WindRose windRose)
        {
            WindRose oldWindRose = GetById(windRose.Id);
            if (oldWindRose == null)
            {
                throw new Exception("NotFound");
            }

            // Очищаем существующие параметры и добавляем новые
            oldWindRose.Parameters.Clear();
            oldWindRose.Parameters.AddRange(windRose.Parameters);

            _context.SaveChanges();

            return oldWindRose;
        }
    }
}
