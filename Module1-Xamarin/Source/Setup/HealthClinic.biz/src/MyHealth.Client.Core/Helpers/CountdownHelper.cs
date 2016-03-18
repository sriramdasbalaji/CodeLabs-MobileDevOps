using MyHealth.Client.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHealth.Client.Core.Helpers
{
    public static class CountdownHelper
    {
        static readonly int CountDownMaxValue = 99;
        static readonly Func<DateTime> _dateTimeProvider = () => DateTime.Now;

        public static int CalcCountDownValue(MedicineWithDoses medicine)
        {
            return CalcCountDownValue(medicine, _dateTimeProvider);
        }

        public static int CalcCountDownValue(MedicineWithDoses medicine, Func<DateTime> dateTimeProvider)
        {
            if (medicine == null)
                return 0;

            var previousDoseTime = medicine.PreviousDoseTime;
            var nextDoseTime = medicine.NextDoseTime;
            var totalTime = TimeOfDayHelper.GetTimeBetween(previousDoseTime, nextDoseTime);
            var remainingTime = TimeOfDayHelper.GetTimeOffsetForNextPill(medicine.NextDoseTime, dateTimeProvider);
            var countDown = ((int)remainingTime.TotalMinutes * 100) /
                (int)totalTime.TotalMinutes;

            return countDown > CountDownMaxValue ? CountDownMaxValue : countDown;
        }
    }
}
