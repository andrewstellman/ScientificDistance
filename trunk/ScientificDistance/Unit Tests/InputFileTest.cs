using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ScientificDistance.Unit_Tests
{
    [TestFixture]
    public class InputFileTest
    {
        [TestFixtureSetUp]
        public void CreateDatabase()
        {
            TestDatabase.Rebuild();
        }

        [Test]
        public void TestBasicInputFile()
        {
            string[] inputFile = new string[] {
                "setnb1,range1,setnb2,range2",
                "X0000001,2000-2005,X0000002,2000-2005",
                "X0000003,2002,X0000004,1992-2007",
                "X0000005,2007,X0000006,2001-2006",
                "X0000001,,X0000004,",
                "X0000002,1999,X0000003,2001",
            };

            List<InputRow> expected = new List<InputRow>() {
                new InputRow() { Scientist1="X0000001", Window1="2000-2005", Scientist2="X0000002", Window2="2000-2005" },
                new InputRow() { Scientist1="X0000003", Window1="2002", Scientist2="X0000004", Window2="1992-2007" },
                new InputRow() { Scientist1="X0000005", Window1="2007", Scientist2="X0000006", Window2="2001-2006" },
                new InputRow() { Scientist1="X0000001", Window1="", Scientist2="X0000004", Window2="" },
                new InputRow() { Scientist1="X0000002", Window1="1999", Scientist2="X0000003", Window2="2001" },
            };

            List<InputRow> actual = Files.ReadInput(inputFile);

            Assert.AreEqual(expected.Count(), actual.Count());
            for (int i = 0; i < expected.Count(); i++)
            {
                Assert.AreEqual(expected[i].Scientist1, actual[i].Scientist1);
                Assert.AreEqual(expected[i].Window1, actual[i].Window1);
                Assert.AreEqual(expected[i].Scientist2, actual[i].Scientist2);
                Assert.AreEqual(expected[i].Window2, actual[i].Window2);
            }
        }



        [Test]
        public void TestQuotedInputFile()
        {
            string[] inputFile = new string[] {
                "setnb1,range1,setnb2,range2",
                "\"X0000001\",\"2000-2005\",\"X0000002\",\"2000-2005\"",
                "\"X00\"\"00\"\"003\",2002,X0000004,1992-2007",
                "\"X0000005\",2007,X0000006,\"2001-2006\"",
                "X0000001,\"\",X0000004,",
                "\"X0000002\",1999,\"X0000003\",\"2001\"",
            };

            List<InputRow> expected = new List<InputRow>() {
                new InputRow() { Scientist1="X0000001", Window1="2000-2005", Scientist2="X0000002", Window2="2000-2005" },
                new InputRow() { Scientist1="X00\"00\"003", Window1="2002", Scientist2="X0000004", Window2="1992-2007" },
                new InputRow() { Scientist1="X0000005", Window1="2007", Scientist2="X0000006", Window2="2001-2006" },
                new InputRow() { Scientist1="X0000001", Window1="", Scientist2="X0000004", Window2="" },
                new InputRow() { Scientist1="X0000002", Window1="1999", Scientist2="X0000003", Window2="2001" },
            };

            List<InputRow> actual = Files.ReadInput(inputFile);

            Assert.AreEqual(expected.Count(), actual.Count());
            for (int i = 0; i < expected.Count(); i++) {
                Assert.AreEqual(expected[i].Scientist1, actual[i].Scientist1);
                Assert.AreEqual(expected[i].Window1, actual[i].Window1);
                Assert.AreEqual(expected[i].Scientist2, actual[i].Scientist2);
                Assert.AreEqual(expected[i].Window2, actual[i].Window2);
            }
        }


        [Test]
        public void TooManyCommas()
        {
            string[] inputFile = new string[] {
                "setnb1,range1,setnb2,range2",
                "X0000001,2000-2005,X0000002,2000-2005",
                "X0000003,2002,X0000004,1992-2007",
                "this,line,has,too,many,commas",
                "X0000005,2007,X0000006,2001-2006",
                "X0000001,,X0000004,",
                "X0000002,1999,X0000003,2001",
            };

            try
            {
                List<InputRow> actual = Files.ReadInput(inputFile);
                Assert.Fail("InputFile.Read() should have failed");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is FormatException);
            }
        }

        [Test]
        public void NotEnoughCommas()
        {
            string[] inputFile = new string[] {
                "setnb1,range1,setnb2,range2",
                "X0000001,2000-2005,X0000002,2000-2005",
                "X0000003,2002,X0000004,1992-2007",
                "not,enough,commas",
                "X0000005,2007,X0000006,2001-2006",
                "X0000001,,X0000004,",
                "X0000002,1999,X0000003,2001",
            };

            try
            {
                List<InputRow> actual = Files.ReadInput(inputFile);
                Assert.Fail("InputFile.Read() should have failed");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is FormatException);
            }
        }


        [Test]
        public void UnclosedQuote()
        {
            string[] inputFile = new string[] {
                "setnb1,range1,setnb2,range2",
                "X0000001,2000-2005,X0000002,2000-2005",
                "\"X0000003,2002,X0000004,1992-2007",
                "X0000005,2007,X0000006,2001-2006",
                "X0000001,,X0000004,",
                "X0000002,1999,X0000003,2001",
            };

            try
            {
                List<InputRow> actual = Files.ReadInput(inputFile);
                Assert.Fail("InputFile.Read() should have failed");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is FormatException);
            }
        }

        [Test]
        public void MissingSetnb1()
        {
            string[] inputFile = new string[] {
                "setnb1,range1,setnb2,range2",
                "X0000001,2000-2005,X0000002,2000-2005",
                "X0000003,2002,X0000004,1992-2007",
                ",2007,X0000006,2001-2006",
                "X0000001,,X0000004,",
                "X0000002,1999,X0000003,2001",
            };

            try
            {
                List<InputRow> actual = Files.ReadInput(inputFile);
                Assert.Fail("InputFile.Read() should have failed");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is FormatException);
            }
        }

        [Test]
        public void MissingSetnb2()
        {
            string[] inputFile = new string[] {
                "setnb1,range1,setnb2,range2",
                "X0000001,2000-2005,X0000002,2000-2005",
                "X0000003,2002,X0000004,1992-2007",
                "X0000005,2007,,2001-2006",
                "X0000001,,X0000004,",
                "X0000002,1999,X0000003,2001",
            };

            try
            {
                List<InputRow> actual = Files.ReadInput(inputFile);
                Assert.Fail("InputFile.Read() should have failed");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is FormatException);
            }
        }
    }
}
