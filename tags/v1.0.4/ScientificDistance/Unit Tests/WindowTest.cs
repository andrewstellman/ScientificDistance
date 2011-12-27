using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ScientificDistance.Unit_Tests
{
    [TestFixture]
    public class WindowTest
    {
        Database db;

        [TestFixtureSetUp]
        public void CreateDatabase()
        {
            TestDatabase.Rebuild();
            db = new Database(TestDatabase.DSN);
        }

        [Test]
        public void NoOverlap()
        {
            string[] actual = Overlap.RollingWindows(db, "X0000004", db, "X0000005", 1);
            Assert.AreEqual(actual.Length, 0);
        }

        [Test]
        public void HeijerickNordstrom_3year()
        {
            string[] expected = new string[] {
                "2000-2002",
                "2001-2003",
                "2002-2004",
                "2003-2005",
            };
            string[] actual = Overlap.RollingWindows(db, "X0000003", db, "X0000004", 3);
            Assert.AreEqual(expected.Length, actual.Length);
            foreach (string window in expected)
                Assert.Contains(window, actual);
        }

        [Test]
        public void HeijerickNordstrom_5year()
        {
            string[] expected = new string[] {
                "1998-2002",
                "1999-2003",
                "2000-2004",
                "2001-2005",
                "2002-2006",
                "2003-2007",
            };
            string[] actual = Overlap.RollingWindows(db, "X0000003", db, "X0000004", 5);
            Assert.AreEqual(expected.Length, actual.Length);
            foreach (string window in expected)
                Assert.Contains(window, actual);
        }

        [Test]
        public void HowardNordstrom_3year()
        {
            string[] expected = new string[] {
                "1990-1992",
                "1991-1993",
                "1992-1994",
                "1993-1995",
                "1994-1996",
                "1995-1997",
                "1996-1998",
                "1997-1999",
                "1998-2000",
                "1999-2001",
                "2000-2002",
                "2001-2003",
                "2002-2004",
                "2003-2005",
            };
            string[] actual = Overlap.RollingWindows(db, "X0000001", db, "X0000004", 3);
            Assert.AreEqual(expected.Length, actual.Length);
            foreach (string window in expected)
                Assert.Contains(window, actual);
        }

        [Test]
        public void HowardNordstrom_5year()
        {
            string[] expected = new string[] {
                "1988-1992",
                "1989-1993",
                "1990-1994",
                "1991-1995",
                "1992-1996",
                "1993-1997",
                "1994-1998",
                "1995-1999",
                "1996-2000",
                "1997-2001",
                "1998-2002",
                "1999-2003",
                "2000-2004",
                "2001-2005",
                "2002-2006",
                "2003-2007",
            };
            string[] actual = Overlap.RollingWindows(db, "X0000001", db, "X0000004", 5);
            Assert.AreEqual(expected.Length, actual.Length);
            foreach (string window in expected)
                Assert.Contains(window, actual);
        }

        [Test]
        public void NullSetnb1()
        {
            try
            {
                string[] actual = Overlap.RollingWindows(db, null, db, "X0000004", 5);
                Assert.Fail("Null setnb parameter should throw an error");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentException);
            }
        }

        [Test]
        public void NullSetnb2()
        {
            try
            {
                string[] actual = Overlap.RollingWindows(db, "X0000004", db, null, 5);
                Assert.Fail("Null setnb parameter should throw an error");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentException);
            }
        }

        [Test]
        public void EmptySetnb2()
        {
            try
            {
                string[] actual = Overlap.RollingWindows(db, "X0000004", db, "", 5);
                Assert.Fail("Null setnb parameter should throw an error");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentException);
            }
        }

        [Test]
        public void EmptyDb1()
        {
            try
            {
                string[] actual = Overlap.RollingWindows(null, "X0000004", db, "", 5);
                Assert.Fail("Null db parameter should throw an error");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentException);
            }
        }

        [Test]
        public void NullDb1()
        {
            try
            {
                string[] actual = Overlap.RollingWindows(null, "X0000004", db, "X000003", 5);
                Assert.Fail("Null db parameter should throw an error");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentNullException);
            }
        }

        [Test]
        public void NullDb2()
        {
            try
            {
                string[] actual = Overlap.RollingWindows(db, "X0000004", null, "X000003", 5);
                Assert.Fail("Null db parameter should throw an error");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentNullException);
            }
        }

        [Test]
        public void ZeroWindow()
        {
            try
            {
                string[] actual = Overlap.RollingWindows(db, "X0000004", db, "X000003", 0);
                Assert.Fail("Zero window parameter should throw an error");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentException);
            }
        }

        [Test]
        public void NegativeWindow()
        {
            try
            {
                string[] actual = Overlap.RollingWindows(db, "X0000004", db, "X000003", -3);
                Assert.Fail("Negative window parameter should throw an error");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentException);
            }
        }
    }
}
