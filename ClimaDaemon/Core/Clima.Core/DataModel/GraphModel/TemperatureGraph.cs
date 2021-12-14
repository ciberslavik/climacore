using System;
using System.Collections.Generic;
using System.Linq;
using Clima.Basics;

namespace Clima.Core.DataModel.GraphModel
{
    public class TemperatureGraph : GraphBase<ValueByDayPoint>
    {
        public TemperatureGraph()
        {
        }

        public ValueByDayPoint GetDayPoint(int day)
        {
            //Если в графике нет точек, возвращаем точку по умолчанию
            if (_points.Count == 0)
                return new ValueByDayPoint(0, 30.0f);
            
            var firstDay = _points.Min(p => p.Day);
            var lastDay = _points.Max(p => p.Day);
            
            if (day >= lastDay)
                return _points.OrderByDescending(p => p.Day).First();
            if (day <= firstDay)
                return _points.OrderBy(p => p.Day).First();
            if (_points.Any(p => p.Day == day))
                return _points.First(p => p.Day == day);
            
            
            //Если запрашиваемый день не найден то
            //получаем ближайший день до запрашиваемого
            var smallerNumberCloseToInput = (from n1 in _points
                where n1.Day < day
                orderby n1.Day descending
                select n1).First();
            //получаем ближайший день после запрашиваемого
            var largerNumberCloseToInput = (from n1 in _points
                where n1.Day > day
                orderby n1.Day
                select n1).FirstOrDefault();
            //Если нашли ближайшие точки
            if ((largerNumberCloseToInput != null) && (smallerNumberCloseToInput != null))
            {
                var periodDays = largerNumberCloseToInput.Day - smallerNumberCloseToInput.Day;
                float diff = day - smallerNumberCloseToInput.Day;
                var point = diff / periodDays;
                //Производим интерполяцию между ближайшими точками
                var temperature = MathUtils.Lerp(
                    smallerNumberCloseToInput.Value,
                    largerNumberCloseToInput.Value,
                    point);
                //Создаем новую точку и возвращаем
                return new ValueByDayPoint(day, temperature);
            }
            //Возвращаем точку по умолчанию если не один из методов получения не сработал
            return new ValueByDayPoint(0, 30.0f);
        }

        public ValueByDayPoint GetFirstPoint()
        {
            return _points.OrderBy(p => p.Day).First();
        }

        public ValueByDayPoint GetLastPoint()
        {
            return _points.OrderByDescending(p => p.Day).First();
        }
        public bool ContainsDay(int day)
        {
            var lastDay = _points.Max(p => p.Day);
            var firstDay = _points.Min(p => p.Day);
            var result = (day >= firstDay) && (day <= lastDay);

            return result;
        }
    }
}