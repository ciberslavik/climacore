using System.Linq;
using Clima.Basics;

namespace Clima.Core.DataModel.GraphModel
{
    public class VentilationGraph : GraphBase<MinMaxByDayPoint>
    {
        public VentilationGraph()
        {
        }

        public MinMaxByDayPoint GetDayPoint(int day)
        {
            if (_points.Count == 0)
                return new MinMaxByDayPoint(0, 1, 2);
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
                var minValue = MathUtils.Lerp(
                    smallerNumberCloseToInput.MinValue,
                    largerNumberCloseToInput.MinValue,
                    point);
                var maxValue = MathUtils.Lerp(
                    smallerNumberCloseToInput.MaxValue,
                    largerNumberCloseToInput.MaxValue,
                    point);
                
                //Возвращаем найденую точку
                return new MinMaxByDayPoint(day, minValue, maxValue);
            }

            return new MinMaxByDayPoint(0, 1, 2);
        }

        public MinMaxByDayPoint GetFirstDay()
        {
            return _points.OrderBy(p => p.Day).First();
        }

        public MinMaxByDayPoint GetLastDay()
        {
            return _points.OrderByDescending(p => p.Day).First();
        }
    }
}