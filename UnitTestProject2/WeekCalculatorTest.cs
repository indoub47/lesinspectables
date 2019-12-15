using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kzs;
using System;

namespace ControllerTest
{
    [TestClass]
    public class WeekCalculatorTest
    {
        [TestMethod]
        public void Overdued()
        {
            DateTime startDate = new DateTime(2019, 12, 8); // sunday
            DateTime deadline1 = new DateTime(2019, 12, 7); 
            DateTime deadline2 = new DateTime(2019, 11, 11); 
            DateTime deadline3 = new DateTime(2018, 12, 13);
            int weeks1 = WeekCalculator.GetWeeksAwayCount(startDate, deadline1);
            int weeks2 = WeekCalculator.GetWeeksAwayCount(startDate, deadline2);
            int weeks3 = WeekCalculator.GetWeeksAwayCount(startDate, deadline3);
            Assert.AreEqual(-1, weeks1);
            Assert.AreEqual(-1, weeks2);
            Assert.AreEqual(-1, weeks3);
        }

        [TestMethod]
        public void SameWeek()
        {
            DateTime startDate = new DateTime(2019, 12, 8); // sunday
            DateTime deadline1 = new DateTime(2019, 12, 8); // same week
            DateTime deadline2 = new DateTime(2019, 12, 11); // same week
            DateTime deadline3 = new DateTime(2019, 12, 13); // same week
            int weeks1 = WeekCalculator.GetWeeksAwayCount(startDate, deadline1);
            int weeks2 = WeekCalculator.GetWeeksAwayCount(startDate, deadline2);
            int weeks3 = WeekCalculator.GetWeeksAwayCount(startDate, deadline3);
            Assert.AreEqual(0, weeks1);
            Assert.AreEqual(0, weeks2);
            Assert.AreEqual(0, weeks3);
        }

        [TestMethod]
        public void NextWeek()
        {
            DateTime startDate = new DateTime(2019, 12, 8); // sunday
            DateTime deadline1 = new DateTime(2019, 12, 14); // next week
            DateTime deadline2 = new DateTime(2019, 12, 16); // next week
            DateTime deadline3 = new DateTime(2019, 12, 20); // next week
            int weeks1 = WeekCalculator.GetWeeksAwayCount(startDate, deadline1);
            int weeks2 = WeekCalculator.GetWeeksAwayCount(startDate, deadline2);
            int weeks3 = WeekCalculator.GetWeeksAwayCount(startDate, deadline3);
            Assert.AreEqual(1, weeks1);
            Assert.AreEqual(1, weeks2);
            Assert.AreEqual(1, weeks3);
        }

        [TestMethod]
        public void UberNextWeek()
        {
            DateTime startDate = new DateTime(2019, 12, 8); // sunday
            DateTime deadline1 = new DateTime(2019, 12, 21); // next week
            DateTime deadline2 = new DateTime(2019, 12, 24); // next week
            DateTime deadline3 = new DateTime(2019, 12, 27); // next week
            int weeks1 = WeekCalculator.GetWeeksAwayCount(startDate, deadline1);
            int weeks2 = WeekCalculator.GetWeeksAwayCount(startDate, deadline2);
            int weeks3 = WeekCalculator.GetWeeksAwayCount(startDate, deadline3);
            Assert.AreEqual(2, weeks1);
            Assert.AreEqual(2, weeks2);
            Assert.AreEqual(2, weeks3);
        }
        [TestMethod]
        public void OverduedStartSaturday()
        {
            DateTime startDate = new DateTime(2019, 12, 7); // saturday
            DateTime deadline1 = new DateTime(2019, 12, 6);
            DateTime deadline2 = new DateTime(2019, 11, 11); 
            DateTime deadline3 = new DateTime(2018, 12, 13);
            int weeks1 = WeekCalculator.GetWeeksAwayCount(startDate, deadline1);
            int weeks2 = WeekCalculator.GetWeeksAwayCount(startDate, deadline2);
            int weeks3 = WeekCalculator.GetWeeksAwayCount(startDate, deadline3);
            Assert.AreEqual(-1, weeks1);
            Assert.AreEqual(-1, weeks2);
            Assert.AreEqual(-1, weeks3);
        }

        [TestMethod]
        public void SameWeekStartSaturday()
        {
            DateTime startDate = new DateTime(2019, 12, 7); // saturday
            DateTime deadline1 = new DateTime(2019, 12, 7); // same week
            DateTime deadline2 = new DateTime(2019, 12, 11); // same week
            DateTime deadline3 = new DateTime(2019, 12, 13); // same week
            int weeks1 = WeekCalculator.GetWeeksAwayCount(startDate, deadline1);
            int weeks2 = WeekCalculator.GetWeeksAwayCount(startDate, deadline2);
            int weeks3 = WeekCalculator.GetWeeksAwayCount(startDate, deadline3);
            Assert.AreEqual(0, weeks1);
            Assert.AreEqual(0, weeks2);
            Assert.AreEqual(0, weeks3);
        }

        [TestMethod]
        public void NextWeekStartSaturday()
        {
            DateTime startDate = new DateTime(2019, 12, 7); // sunday
            DateTime deadline1 = new DateTime(2019, 12, 14); // next week
            DateTime deadline2 = new DateTime(2019, 12, 16); // next week
            DateTime deadline3 = new DateTime(2019, 12, 20); // next week
            int weeks1 = WeekCalculator.GetWeeksAwayCount(startDate, deadline1);
            int weeks2 = WeekCalculator.GetWeeksAwayCount(startDate, deadline2);
            int weeks3 = WeekCalculator.GetWeeksAwayCount(startDate, deadline3);
            Assert.AreEqual(1, weeks1);
            Assert.AreEqual(1, weeks2);
            Assert.AreEqual(1, weeks3);
        }

        [TestMethod]
        public void UberNextWeekStartSaturday()
        {
            DateTime startDate = new DateTime(2019, 12, 7); // sunday
            DateTime deadline1 = new DateTime(2019, 12, 21); // next week
            DateTime deadline2 = new DateTime(2019, 12, 24); // next week
            DateTime deadline3 = new DateTime(2019, 12, 27); // next week
            int weeks1 = WeekCalculator.GetWeeksAwayCount(startDate, deadline1);
            int weeks2 = WeekCalculator.GetWeeksAwayCount(startDate, deadline2);
            int weeks3 = WeekCalculator.GetWeeksAwayCount(startDate, deadline3);
            Assert.AreEqual(2, weeks1);
            Assert.AreEqual(2, weeks2);
            Assert.AreEqual(2, weeks3);
        }
    }
}
