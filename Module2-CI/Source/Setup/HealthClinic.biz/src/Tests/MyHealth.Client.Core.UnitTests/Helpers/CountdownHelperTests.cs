using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyHealth.Client.Core.Helpers;
using MyHealth.Client.Core.Model;
using System.Collections.Generic;
using Microsoft.QualityTools.Testing.Fakes;
using System.Fakes;

namespace MyHealth.Client.Core.UnitTests.Helpers
{
    [TestClass]
    public class CountdownHelperTests
    {
        [TestMethod]
        public void Test_CalcCountDownForNullMed()
        {
            var count = CountdownHelper.CalcCountDownValue(null);
            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void Test_CalcCountDownForMedNoTimes_Is99()
        {
            using (ShimsContext.Create())
            {
                var time = new DateTime(2016, 2, 25, 8, 0, 0);
                ShimDateTime.NowGet = () => time;

                var medWithDoses = new MedicineWithDoses(new Medicine(), () => time);
                var count = CountdownHelper.CalcCountDownValue(medWithDoses);
                Assert.AreEqual(99, count);
            } 
        }

        [TestMethod]
        public void Test_CalcCountDownForMed_Pref_Breakfast_IsCorrect()
        {
            using (ShimsContext.Create())
            {
                var time = new DateTime(2016, 2, 25, 16, 0, 0);
                ShimDateTime.NowGet = () => time;

                var medWithDoses = new MedicineWithDoses(new Medicine(), () => time);
                medWithDoses.AddDoseTime(TimeOfDay.Breakfast);
                var count = CountdownHelper.CalcCountDownValue(medWithDoses);
                Assert.AreEqual(66, count);
            }
        }

        [TestMethod]
        public void Test_CalcCountDownForMed_Pref_Lunch_IsCorrect()
        {
            using (ShimsContext.Create())
            {
                var time = new DateTime(2016, 2, 25, 8, 0, 0);
                ShimDateTime.NowGet = () => time;

                var medWithDoses = new MedicineWithDoses(new Medicine(), () => time);
                medWithDoses.AddDoseTime(TimeOfDay.Lunch);
                var count = CountdownHelper.CalcCountDownValue(medWithDoses);
                Assert.AreEqual(25, count);
            }
        }

        [TestMethod]
        public void Test_CalcCountDownForMed_Pref_LunchDinner_AfterDinner_IsCorrect()
        {
            using (ShimsContext.Create())
            {
                var time = new DateTime(2016, 2, 25, 22, 0, 0);
                ShimDateTime.NowGet = () => time;

                var medWithDoses = new MedicineWithDoses(new Medicine(), () => time);
                medWithDoses.AddDoseTimes(new[] { TimeOfDay.Lunch, TimeOfDay.Dinner });
                var count = CountdownHelper.CalcCountDownValue(medWithDoses);
                Assert.AreEqual(94, count);
            }
        }
    }
}
