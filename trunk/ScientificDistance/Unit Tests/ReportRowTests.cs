using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ScientificDistance.Unit_Tests
{
    /// <summary>
    /// Generate report rows for various people in the database
    /// See the following files for more details on where this data comes from:
    ///    http://stellman-greene.com/ScientificDistance/Steps%20for%20generating%20Scientific%20Distance%20test%20data.doc
    ///    http://stellman-greene.com/ScientificDistance/Scientific%20Distance%20unit%20test%20data.xls
    /// </summary>
    [TestFixture]
    public class ReportRowTests
    {
        [TestFixtureSetUp]
        public void RebuildDatabase()
        {
            TestDatabase.Rebuild();
        }

        [Test]
        public void HowardOConnor()
        {
            string expectedRow = "X0000001,2000-2005,X0000002,2000-2005,37,52,59,119,15,25";
            Publications howardPublications = new Publications(new Database(TestDatabase.DSN), "X0000001", "2000-2005", null);
            Publications oconnorPublications = new Publications(new Database(TestDatabase.DSN), "X0000002", "2000-2005", null);
            string actualRow = Overlap.GenerateOverlapReportRow(howardPublications, oconnorPublications);
            Assert.AreEqual(expectedRow, actualRow);
        }

        [Test]
        public void HeijerickNordstrom()
        {
            string expectedRow = "X0000003,2002,X0000004,1992-1997,30,39,46,64,4,4";
            Publications heijerickPublications = new Publications(new Database(TestDatabase.DSN), "X0000003", "2002", null);
            Publications nordstromPublications = new Publications(new Database(TestDatabase.DSN), "X0000004", "1992-1997", null);
            string actualRow = Overlap.GenerateOverlapReportRow(heijerickPublications, nordstromPublications);
            Assert.AreEqual(expectedRow, actualRow);
        }

        [Test]
        public void HowardNordstrom()
        {
            string expectedRow = "X0000001,,X0000004,,106,217,72,116,1,1";
            Publications howardPublications = new Publications(new Database(TestDatabase.DSN), "X0000001", "", null);
            Publications nordstromPublications = new Publications(new Database(TestDatabase.DSN), "X0000004", "", null);
            string actualRow = Overlap.GenerateOverlapReportRow(howardPublications, nordstromPublications);
            Assert.AreEqual(expectedRow, actualRow);
        }

        [Test]
        public void EmptyPublications()
        {
            string expectedRow = "X0000002,1999,X0000003,2001,0,0,0,0,0,0";
            Publications oconnorPublications = new Publications(new Database(TestDatabase.DSN), "X0000002", "1999", null);
            Publications heijerickPublications = new Publications(new Database(TestDatabase.DSN), "X0000003", "2001", null);
            string actualRow = Overlap.GenerateOverlapReportRow(oconnorPublications, heijerickPublications);
            Assert.AreEqual(expectedRow, actualRow);
        }

        [Test]
        public void ExcludedSomePublications()
        {
            // Here's the expected row for X0000003,2002,X0000004,1992-1997 
            string expectedRow = "X0000003,2002,X0000004,,30,39,46,64,4,4";

            // We'll do all of X0000004, and remove the four publications outside of the range,
            // so here are the PMIDs of the publications outside of the range.
            List<int> pmidsToExclude = new List<int>() { 9713684, 11340600, 11564905, 12775905 };

            Publications heijerickPublications = new Publications(new Database(TestDatabase.DSN), "X0000003", "2002", null);
            Publications nordstromPublications = new Publications(new Database(TestDatabase.DSN), "X0000004", "", pmidsToExclude);
            string actualRow = Overlap.GenerateOverlapReportRow(heijerickPublications, nordstromPublications);
            Assert.AreEqual(expectedRow, actualRow);
        }

        [Test]
        public void ExcludedAllPublications()
        {
            // Here's the expected row for Howard and O'Connor
            string expectedRow = "X0000001,2000-2005,X0000002,2000-2005,0,0,0,0,0,0";

            List<int> pmidsToExcludeForHoward = new List<int>() { 1620193, 2626221, 2661289, 3158849,
                6446858, 6539643, 7432734, 7565486, 8818166, 9134263, 9368207, 10624722, 11707907, 12597089, 16173671 };

            List<int> pmidsToExcludeForOConnor = new List<int>() { 7789203, 10902695, 11099113, 14531577,
                14531585, 15055374, 15324991, 15901223, 16402868, 16671933, 17459175, 17492950, 17634075, 18049295 };

            Publications howardPublications = new Publications(new Database(TestDatabase.DSN), "X0000001", "2000-2005", pmidsToExcludeForHoward);
            Publications oconnorPublications = new Publications(new Database(TestDatabase.DSN), "X0000002", "2000-2005", pmidsToExcludeForOConnor);
            string actualRow = Overlap.GenerateOverlapReportRow(howardPublications, oconnorPublications);
            Assert.AreEqual(expectedRow, actualRow);
        }

        [Test]
        public void MissingSetnb()
        {
            string expectedRow = "X0000003,2002,Z1234567,,30,39,0,0,0,0";
            Publications oconnorPublications = new Publications(new Database(TestDatabase.DSN), "X0000003", "2002", null);
            Publications emptyPublications = new Publications(new Database(TestDatabase.DSN), "Z1234567", "", null);
            string actualRow = Overlap.GenerateOverlapReportRow(oconnorPublications, emptyPublications);
            Assert.AreEqual(expectedRow, actualRow);
            expectedRow = "Z1234567,,X0000003,2002,0,0,30,39,0,0";
            actualRow = Overlap.GenerateOverlapReportRow(emptyPublications, oconnorPublications);
            Assert.AreEqual(expectedRow, actualRow);
        }

        [Test]
        public void NullPublications()
        {
            Publications howardPublications = new Publications(new Database(TestDatabase.DSN), "X0000001", "", null);

            try
            {
                string result = Overlap.GenerateOverlapReportRow(howardPublications, null);
                Assert.Fail("Null Publication parameter should throw an error");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentNullException);
            }

            try
            {
                string result = Overlap.GenerateOverlapReportRow(null, howardPublications);
                Assert.Fail("Null Publication parameter should throw an error");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentNullException);
            }
        }

        [Test]
        public void InvalidWindows()
        {
            try
            {
                Publications howardPublications = new Publications(new Database(TestDatabase.DSN), "X0000001", null, null);
                Assert.Fail("Null window parameter should throw an error");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentNullException);
            }

            try
            {
                Publications howardPublications = new Publications(new Database(TestDatabase.DSN), "X0000001", "beer", null);
                Assert.Fail("Invalid window parameter should throw an error");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentException);
            }

            try
            {
                Publications howardPublications = new Publications(new Database(TestDatabase.DSN), "X0000001", "2005-1992", null);
                Assert.Fail("End date before start date should throw an error");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentException);
            }

            try
            {
                Publications howardPublications = new Publications(new Database(TestDatabase.DSN), "X0000001", "wrench-1992", null);
                Assert.Fail("Invalid start date should throw an error");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentException);
            }

            try
            {
                Publications howardPublications = new Publications(new Database(TestDatabase.DSN), "X0000001", "1992-wallet", null);
                Assert.Fail("Invalid end date should throw an error");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentException);
            }
        }

        [Test]
        public void NullSetnb()
        {
            try
            {
                Publications howardPublications = new Publications(new Database(TestDatabase.DSN), null, "", null);
                Assert.Fail("Null window parameter should throw an error");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentNullException);
            }
        }

    }
}
