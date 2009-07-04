using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NUnit.Framework;

namespace ScientificDistance.Unit_Tests
{
    [TestFixture]
    public class FaultToleranceTest
    {
        [TearDown]
        public void TearDown()
        {
            DeleteFiles();
        }

        [SetUp]
        public void SetUp()
        {
            DeleteFiles();
        }

        public void DeleteFiles()
        {
            File.Delete("report.csv");
            File.Delete("report.csv.bak");
            File.Delete("mesh.csv");
            File.Delete("mesh.csv.bak");
        }



        private static void WriteFiles(string[] reportLines, string[] meshLines)
        {
            File.WriteAllLines("report.csv", reportLines);
            File.WriteAllLines("mesh.csv", meshLines);
        }

        
        List<InputRow> normalInput = new List<InputRow>()
        {
            new InputRow() { Scientist1 = "X0000003", Window1 = "2002", Scientist2 = "X0000004", Window2 = "1992-1997" },
            new InputRow() { Scientist1 = "X0000001", Window1 = "", Scientist2 = "X0000004", Window2 = "" },
            new InputRow() { Scientist1 = "X0000006", Window1 = "1999", Scientist2 = "X0000003", Window2 = "2001" },
            new InputRow() { Scientist1 = "X0000002", Window1 = "1999", Scientist2 = "X0000003", Window2 = "2001" },
            new InputRow() { Scientist1 = "X0000003", Window1 = "2002", Scientist2 = "Z1234567", Window2 = "" },
            new InputRow() { Scientist1 = "X0000004", Window1 = "2002", Scientist2 = "X0000004", Window2 = "1996-2002" },
            new InputRow() { Scientist1 = "X0000005", Window1 = "2002", Scientist2 = "X0000006", Window2 = "1993" },
            new InputRow() { Scientist1 = "X0000001", Window1 = "1999", Scientist2 = "X0000002", Window2 = "1997" },
        };

        string[] normalFile = new string[] {
            "setnb1,range1,setnb2,range2,nb_unq_keywords_1,nb_frq_keywords_1,nb_unq_keywords_2,nb_frq_keywords_2,nb_unq_keywords_ovrlp,nb_frq_keywords_ovrlp",
            "X0000003,2002,X0000004,1992-1997,7,12,4,6,4,4",
            "X0000001,,X0000004,,8,9,8,10,1,1",
            "X0000006,1999,X0000003,2001,0,0,0,0,0,0",
            "X0000002,1999,X0000003,2001,10,15,8,10,7,12",
            "X0000003,2002,Z1234567,,30,39,0,0,0,0",
            "X0000004,1991-1996,X0000004,1996-2002,36,90,15,25,7,11",
            "X0000005,1993,X0000006,1993,12,15,14,16,15,15",
            "X0000001,199",
        };

        string[] normalMesh = new string[] { 
            "setnb,range,heading,count",
            "X0000003,2002,Humans,3",
            "X0000003,2002,Linear Models,1",
            "X0000003,2002,Logistic Models,2",
            "X0000003,2002,Male,2",
            "X0000003,2002,Mass Screening,2",
            "X0000003,2002,Membrane Proteins,1",
            "X0000003,2002,Menopause,1",
            "X0000004,1992-1997,Menopause,1",
            "X0000004,1992-1997,Middle Aged,3",
            "X0000004,1992-1997,Molecular Diagnostic Techniques,1",
            "X0000004,1992-1997,Mutation,1",
            "X0000001,,Adenocarcinoma,1",
            "X0000001,,Adult,1",
            "X0000001,,Aged,2",
            "X0000001,,Anticarcinogenic Agents,1",
            "X0000001,,Asian Continental Ancestry Group,1",
            "X0000001,,Breast Neoplasms,1",
            "X0000001,,Carotenoids,1",
            "X0000001,,Case-Control Studies,1",
            "X0000004,,Case-Control Studies,1",
            "X0000004,,China,2",
            "X0000004,,Colorectal Neoplasms,1",
            "X0000004,,Confidence Intervals,1",
            "X0000004,,Cyclooxygenase 2,1",
            "X0000004,,DNA Fingerprinting,1",
            "X0000004,,Epidemiologic Studies,1",
            "X0000004,,Female,2",
            "X0000002,1999,Genes APC,1",
            "X0000002,1999,Genetic Predisposition to Disease,1",
            "X0000002,1999,Genotype,1",
            "X0000002,1999,Humans,3",
            "X0000002,1999,Linear Models,1",
            "X0000002,1999,Logistic Models,2",
            "X0000002,1999,Male,2",
            "X0000002,1999,Mass Screening,2",
            "X0000002,1999,Membrane Proteins,1",
            "X0000002,1999,Menopause,1",
            "X0000003,2001,Menopause,1",
            "X0000003,2001,Middle Aged,3",
            "X0000003,2001,Molecular Diagnostic Techniques,1",
            "X0000003,2001,Mutation,1",
            "X0000003,2001,Odds Ratio,1",
            "X0000003,2001,Polymorphism Single Nucleotide,1",
            "X0000003,2001,Prospective Studies,1",
            "X0000003,2001,Prostatic Neoplasms,1",
            "X0000003,2002,Questionnaires,1",
            "X0000003,2002,Risk Assessment,1",
            "X0000003,2",
        };



        List<InputRow> biggerInput = new List<InputRow>()
        {
            new InputRow() { Scientist1 = "X0000003", Window1 = "1992-1994", Scientist2 = "X0000004", Window2 = "1992-1994" },
            new InputRow() { Scientist1 = "X0000003", Window1 = "1993-1995", Scientist2 = "X0000004", Window2 = "1993-1995" },
            new InputRow() { Scientist1 = "X0000003", Window1 = "1994-1996", Scientist2 = "X0000004", Window2 = "1994-1996" },
            new InputRow() { Scientist1 = "X0000001", Window1 = "1999-2001", Scientist2 = "X0000004", Window2 = "1999-2001" },
            new InputRow() { Scientist1 = "X0000001", Window1 = "2000-2002", Scientist2 = "X0000004", Window2 = "2000-2002" },
            new InputRow() { Scientist1 = "X0000001", Window1 = "2001-2003", Scientist2 = "X0000004", Window2 = "2001-2003" },
            new InputRow() { Scientist1 = "X0000001", Window1 = "2002-2004", Scientist2 = "X0000004", Window2 = "2002-2004" },
            new InputRow() { Scientist1 = "X0000002", Window1 = "1998-2000", Scientist2 = "X0000003", Window2 = "1998-2000" },
            new InputRow() { Scientist1 = "X0000002", Window1 = "1999-2001", Scientist2 = "X0000003", Window2 = "1999-2001" },
            new InputRow() { Scientist1 = "X0000002", Window1 = "2000-2002", Scientist2 = "X0000003", Window2 = "2000-2002" },
            new InputRow() { Scientist1 = "X0000002", Window1 = "2001-2003", Scientist2 = "X0000003", Window2 = "2001-2003" },
            new InputRow() { Scientist1 = "X0000003", Window1 = "2002", Scientist2 = "Z1234567", Window2 = "" },
            new InputRow() { Scientist1 = "X0000004", Window1 = "2002", Scientist2 = "X0000004", Window2 = "1996-2002" },
            new InputRow() { Scientist1 = "X0000005", Window1 = "2002", Scientist2 = "X0000006", Window2 = "1993" },
            new InputRow() { Scientist1 = "X0000001", Window1 = "1999", Scientist2 = "X0000002", Window2 = "1997" },
        };

        private string[] biggerReport = new string[] {
            "setnb1,range1,setnb2,range2,nb_unq_keywords_1,nb_frq_keywords_1,nb_unq_keywords_2,nb_frq_keywords_2,nb_unq_keywords_ovrlp,nb_frq_keywords_ovrlp",
            "X0000003,1992-1994,X0000004,1992-1994,7,12,4,6,4,4",
            "X0000003,1993-1995,X0000004,1993-1995,5,10,6,8,6,4",
            "X0000003,1994-1996,X0000004,1994-1996,5,10,6,8,2,3",
            "X0000001,1999-2001,X0000004,1999-2001,8,9,8,10,1,1",
            "X0000001,2000-2002,X0000004,2000-2002,8,9,8,10,4,1",
            "X0000001,2001-2003,X0000004,2001-2003,0,0,8,10,0,0",
            "X0000001,2002-2004,X0000004,2002-2004,8,9,0,0,0,0",
            "X0000002,1998-2000,X0000003,1998-2000,10,15,10,12,2,1",
            "X0000002,1999-2001,X0000003,1999-2001,11,19,3,2,3,1",
            "X0000002,2000-2002,X0000003,2000-2002,4,3,4,1,0,0",
            "X0000002,2001-2003,X0000",
        };

        string[] biggerMesh = new string[] { 
            "setnb,range,heading,count",
            "X0000003,1992-1994,Humans,3",
            "X0000003,1992-1994,Linear Models,1",
            "X0000003,1992-1994,Logistic Models,2",
            "X0000003,1992-1994,Male,2",
            "X0000003,1992-1994,Mass Screening,2",
            "X0000003,1992-1994,Membrane Proteins,1",
            "X0000003,1992-1994,Menopause,1",
            "X0000004,1992-1994,Menopause,1",
            "X0000004,1992-1994,Middle Aged,3",
            "X0000004,1992-1994,Molecular Diagnostic Techniques,1",
            "X0000004,1992-1994,Mutation,1",
            "X0000003,1993-1995,Humans,3",
            "X0000003,1993-1995,Linear Models,1",
            "X0000003,1993-1995,Logistic Models,2",
            "X0000003,1993-1995,Male,2",
            "X0000003,1993-1995,Mass Screening,2",
            "X0000004,1993-1995,Membrane Proteins,1",
            "X0000004,1993-1995,Menopause,1",
            "X0000004,1993-1995,Menopause,1",
            "X0000004,1993-1995,Middle Aged,3",
            "X0000004,1993-1995,Molecular Diagnostic Techniques,1",
            "X0000004,1993-1995,Mutation,1",
            "X0000003,1994-1996,Humans,3",
            "X0000003,1994-1996,Linear Models,1",
            "X0000003,1994-1996,Logistic Models,2",
            "X0000003,1994-1996,Male,2",
            "X0000003,1994-1996,Mass Screening,2",
            "X0000004,1994-1996,Membrane Proteins,1",
            "X0000004,1994-1996,Menopause,1",
            "X0000004,1994-1996,Menopause,1",
            "X0000004,1994-1996,Middle Aged,3",
            "X0000004,1994-1996,Molecular Diagnostic Techniques,1",
            "X0000004,1994-1996,Mutation,1",
            "X0000001,1999-2001,Adenocarcinoma,1",
            "X0000001,1999-2001,Adult,1",
            "X0000001,1999-2001,Aged,2",
            "X0000001,1999-2001,Anticarcinogenic Agents,1",
            "X0000001,1999-2001,Asian Continental Ancestry Group,1",
            "X0000001,1999-2001,Breast Neoplasms,1",
            "X0000001,1999-2001,Carotenoids,1",
            "X0000001,1999-2001,Case-Control Studies,1",
            "X0000004,1999-2001,Case-Control Studies,1",
            "X0000004,1999-2001,China,2",
            "X0000004,1999-2001,Colorectal Neoplasms,1",
            "X0000004,1999-2001,Confidence Intervals,1",
            "X0000004,1999-2001,Cyclooxygenase 2,1",
            "X0000004,1999-2001,DNA Fingerprinting,1",
            "X0000004,1999-2001,Epidemiologic Studies,1",
            "X0000004,1999-2001,Female,2",
            "X0000001,2000-2002,Adenocarcinoma,1",
            "X0000001,2000-2002,Adult,1",
            "X0000001,2000-2002,Aged,2",
            "X0000001,2000-2002,Anticarcinogenic Agents,1",
            "X0000001,2000-2002,Asian Continental Ancestry Group,1",
            "X0000001,2000-2002,Breast Neoplasms,1",
            "X0000001,2000-2002,Carotenoids,1",
            "X0000001,2000-2002,Case-Control Studies,1",
            "X0000004,2000-2002,Case-Control Studies,1",
            "X0000004,2000-2002,China,2",
            "X0000004,2000-2002,Colorectal Neoplasms,1",
            "X0000004,2000-2002,Confidence Intervals,1",
            "X0000004,2000-2002,Cyclooxygenase 2,1",
            "X0000004,2000-2002,DNA Fingerprinting,1",
            "X0000004,2000-2002,Epidemiologic Studies,1",
            "X0000004,2000-2002,Female,2",
            "X0000004,2001-2003,Case-Control Studies,1",
            "X0000004,2001-2003,China,2",
            "X0000004,2001-2003,Colorectal Neoplasms,1",
            "X0000004,2001-2003,Confidence Intervals,1",
            "X0000004,2001-2003,Cyclooxygenase 2,1",
            "X0000004,2001-2003,DNA Fingerprinting,1",
            "X0000004,2001-2003,Epidemiologic Studies,1",
            "X0000004,2001-2003,Female,2",
            "X0000001,2002-2004,Adenocarcinoma,1",
            "X0000001,2002-2004,Adult,1",
            "X0000001,2002-2004,Aged,2",
            "X0000001,2002-2004,Anticarcinogenic Agents,1",
            "X0000001,2002-2004,Asian Continental Ancestry Group,1",
            "X0000001,2002-2004,Breast Neoplasms,1",
            "X0000001,2002-2004,Carotenoids,1",
            "X0000001,2002-2004,Case-Control Studies,1",
            "X0000002,1998-2000,Genes APC,1",
            "X0000002,1998-2000,Genetic Predisposition to Disease,1",
            "X0000002,1998-2000,Genotype,1",
            "X0000002,1998-2000,Humans,3",
            "X0000002,1998-2000,Linear Models,1",
            "X0000002,1998-2000,Logistic Models,2",
            "X0000002,1998-2000,Male,2",
            "X0000002,1998-2000,Mass Screening,2",
            "X0000002,1998-2000,Membrane Proteins,1",
            "X0000002,1998-2000,Menopause,1",
            "X0000003,1998-2000,Menopause,1",
            "X0000003,1998-2000,Middle Aged,3",
            "X0000003,1998-2000,Molecular Diagnostic Techniques,1",
            "X0000003,1998-2000,Mutation,1",
            "X0000003,1998-2000,Odds Ratio,1",
            "X0000003,1998-2000,Polymorphism Single Nucleotide,1",
            "X0000003,1998-2000,Prospective Studies,1",
            "X0000003,1998-2000,Prostatic Neoplasms,1",
            "X0000003,1998-2000,Questionnaires,1",
            "X0000003,1998-2000,Risk Assessment,1",
            "X0000002,1999-2001,Logistic Models,2",
            "X0000002,1999-2001,Male,2",
            "X0000002,1999-2001,Mass Screening,2",
            "X0000002,1999-2001,Membrane Proteins,1",
            "X0000002,1999-2001,Menopause,1",
            "X0000003,1999-2001,Menopause,1",
            "X0000003,1999-2001,Middle Aged,3",
            "X0000003,1999-2001,Molecular Diagnostic Techniques,1",
            "X0000003,1",
        };



        [Test]
        public void NormalResume()
        {
            WriteFiles(normalFile, normalMesh);

            List<InputRow> expected = new List<InputRow>()
            {
                new InputRow() { Scientist1 = "X0000003", Window1 = "2002", Scientist2 = "Z1234567", Window2 = "" },
                new InputRow() { Scientist1 = "X0000004", Window1 = "2002", Scientist2 = "X0000004", Window2 = "1996-2002" },
                new InputRow() { Scientist1 = "X0000005", Window1 = "2002", Scientist2 = "X0000006", Window2 = "1993" },
                new InputRow() { Scientist1 = "X0000001", Window1 = "1999", Scientist2 = "X0000002", Window2 = "1997" },
            };

            List<InputRow> actual = Files.FaultTolerantCopy("report.csv", "mesh.csv", false, normalInput);

            // FaultTolerantCopy() should remove the first three rows from inputRows
            CompareExpectedAndActual(expected, actual, "NormalResume()");

            CompareFile("report.csv.bak", normalFile, normalFile.Length);
            CompareFile("mesh.csv.bak", normalMesh, normalMesh.Length);

            CompareFile("report.csv", normalFile, 5); // the header row plus the first 4 rows should have been copied
            CompareFile("mesh.csv", normalMesh, normalMesh.Length - 3); // the last 3 lines of normalMesh should be cut off
        }

        private static void CompareExpectedAndActual(List<InputRow> expected, List<InputRow> actual, string message)
        {
            Assert.AreEqual(expected.Count(), actual.Count(), message);
            for (int i = 0; i < expected.Count(); i++)
            {
                Assert.That(expected[i].CompareTo(actual[i]) == 0,
                    "ComparExpectedAndActual / " + message + ": row " + i + " doesn't match");
            }

        }


        private static void CompareFile(string filename, string[] lines, int length)
        {
            Assert.IsTrue(File.Exists(filename));
            string[] report = File.ReadAllLines(filename);
            Assert.AreEqual(report.Length, length, filename + " has the wrong length");
            for (int i = 0; i < length; i++)
            {
                Assert.AreEqual(report[i], lines[i], filename + " line " + i + " doesn't match");
            }
        }


        [Test]
        public void RollingWindowFails()
        {
            WriteFiles(biggerReport, biggerMesh);

            List<InputRow> expected = new List<InputRow>()
            {
                new InputRow() { Scientist1 = "X0000002", Window1 = "1999", Scientist2 = "X0000003", Window2 = "2001" },
                new InputRow() { Scientist1 = "X0000003", Window1 = "2002", Scientist2 = "Z1234567", Window2 = "" },
                new InputRow() { Scientist1 = "X0000004", Window1 = "2002", Scientist2 = "X0000004", Window2 = "1996-2002" },
                new InputRow() { Scientist1 = "X0000005", Window1 = "2002", Scientist2 = "X0000006", Window2 = "1993" },
                new InputRow() { Scientist1 = "X0000001", Window1 = "1999", Scientist2 = "X0000002", Window2 = "1997" },
            };

            try
            {
                List<InputRow> actual = Files.FaultTolerantCopy("report.csv", "mesh.csv", true, biggerInput);
                Assert.Fail("FaultTolerantCopy() should fail with a rolling window");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is OperationCanceledException);
            }

        }


        [Test]
        public void BiggerFileResume()
        {
            WriteFiles(biggerReport, biggerMesh);

            List<InputRow> expected = new List<InputRow>()
            {
                new InputRow() { Scientist1 = "X0000002", Window1 = "1999-2001", Scientist2 = "X0000003", Window2 = "1999-2001" },
                new InputRow() { Scientist1 = "X0000002", Window1 = "2000-2002", Scientist2 = "X0000003", Window2 = "2000-2002" },
                new InputRow() { Scientist1 = "X0000002", Window1 = "2001-2003", Scientist2 = "X0000003", Window2 = "2001-2003" },
                new InputRow() { Scientist1 = "X0000003", Window1 = "2002", Scientist2 = "Z1234567", Window2 = "" },
                new InputRow() { Scientist1 = "X0000004", Window1 = "2002", Scientist2 = "X0000004", Window2 = "1996-2002" },
                new InputRow() { Scientist1 = "X0000005", Window1 = "2002", Scientist2 = "X0000006", Window2 = "1993" },
                new InputRow() { Scientist1 = "X0000001", Window1 = "1999", Scientist2 = "X0000002", Window2 = "1997" },
            };

            List<InputRow> actual = Files.FaultTolerantCopy("report.csv", "mesh.csv", false, biggerInput);

            CompareExpectedAndActual(expected, actual, "BiggerFileResume()");

            CompareFile("report.csv.bak", biggerReport, biggerReport.Length);
            CompareFile("mesh.csv.bak", biggerMesh, biggerMesh.Length);

            CompareFile("report.csv", biggerReport, 9); // the first 8 rows plus the header row should have been copied
            CompareFile("mesh.csv", biggerMesh, biggerMesh.Length - 9); // the last 9 lines of the mesh file should be cut off
        }


        [Test]
        public void MismatchedInputRows()
        {
            File.Delete("report.csv");
            File.Delete("report.csv.bak");
            File.Delete("mesh.csv");
            File.Delete("mesh.csv.bak");

            File.WriteAllLines("report.csv", biggerReport);
            File.WriteAllLines("mesh.csv", normalMesh);

            List<InputRow> differentInput = new List<InputRow>()
            {
                new InputRow() { Scientist1 = "Z0000005", Window1 = "2002", Scientist2 = "X0000006", Window2 = "1993" },
                new InputRow() { Scientist1 = "Z0000004", Window1 = "2002", Scientist2 = "X0000004", Window2 = "1996-2002" },
                new InputRow() { Scientist1 = "Z0000003", Window1 = "2002", Scientist2 = "Z1234567", Window2 = "" },
                new InputRow() { Scientist1 = "Z0000001", Window1 = "1999", Scientist2 = "X0000002", Window2 = "1997" },
                new InputRow() { Scientist1 = "Z0000002", Window1 = "2002", Scientist2 = "X0000003", Window2 = "2001" },
            };

            try
            {
                List<InputRow> actual = Files.FaultTolerantCopy("report.csv", "mesh.csv", false, normalInput);
                Assert.Fail("FaultTolerantCopy() should have thrown an exception");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is OperationCanceledException);
            }

            File.Delete("report.csv");
            File.Delete("report.csv.bak");
            File.Delete("mesh.csv");
            File.Delete("mesh.csv.bak");
        }




        [Test]
        public void GeneratedFileTest()
        {
            List<InputRow> inputRows = new List<InputRow>()
            {
                new InputRow() { Scientist1 = "ABC", Window1 = "1992-1996", Scientist2 = "Q", Window2 = "1992-1996" },
                new InputRow() { Scientist1 = "DEF", Window1 = "", Scientist2 = "W", Window2 = "" },
                new InputRow() { Scientist1 = "ABC", Window1 = "1994-1998", Scientist2 = "Q", Window2 = "1994-1998" },
                new InputRow() { Scientist1 = "QABC", Window1 = "1995-1999", Scientist2 = "Q", Window2 = "2005" },
                new InputRow() { Scientist1 = "GHI", Window1 = "1982-1986", Scientist2 = "GHI", Window2 = "1987-1994" },
                new InputRow() { Scientist1 = "DEF", Window1 = "2000-2004", Scientist2 = "W", Window2 = "2000-2004" },
                new InputRow() { Scientist1 = "ABC", Window1 = "", Scientist2 = "QQQ", Window2 = "1993-1997" },
                new InputRow() { Scientist1 = "DEF", Window1 = "2002-2006", Scientist2 = "W", Window2 = "2002-2006" },
                new InputRow() { Scientist1 = "DEF", Window1 = "", Scientist2 = "W", Window2 = "1995" },
                new InputRow() { Scientist1 = "DEF", Window1 = "2004-2008", Scientist2 = "W", Window2 = "2004-2008" },
                new InputRow() { Scientist1 = "GHI", Window1 = "1985-1989", Scientist2 = "E", Window2 = "1989-1992" },
                new InputRow() { Scientist1 = "JKL", Window1 = "2000-2002", Scientist2 = "X0000003", Window2 = "2000-2002" },
                new InputRow() { Scientist1 = "GHI", Window1 = "1985-1989", Scientist2 = "E", Window2 = "2000-2002" },
                new InputRow() { Scientist1 = "JKL", Window1 = "2001-2003", Scientist2 = "X0000003", Window2 = "1999" },
                new InputRow() { Scientist1 = "X0000001", Window1 = "2000-2002", Scientist2 = "X0000003", Window2 = "2000-2002" },
                new InputRow() { Scientist1 = "X0000002", Window1 = "2000-2002", Scientist2 = "X0000003", Window2 = "2000-2002" },
            };

            
            string[] report = new string[] {
                "setnb1,range1,setnb2,range2,nb_unq_keywords_1,nb_frq_keywords_1,nb_unq_keywords_2,nb_frq_keywords_2,nb_unq_keywords_ovrlp,nb_frq_keywords_ovrlp",
                "ABC,1992-1996,Q,1992-1996,0,0,46,64,4,4",
                "DEF,,W,,115,227,37,146,8,2",
                "ABC,1994-1998,Q,1994-1998,10,33,0,0,2,3",
                "QABC,1995-1999,Q,2005,106,217,72,116,1,1",
                "GHI,1982-1986,GHI,1987-1994,30,39,46,64,4,4",
                "DEF,2000-2004,W,2000-2004,0,0,0,0,4,1",
                "ABC,,QQQ,1993-1997,40,37,43,64,6,4",
                "DEF,2002-2006,W,2002-2006,1,1,1,1,3,8",
                "DEF,,W,1995,3,7,8,3,2,1",
                "DEF,2004-2008,W,2004-2008,7,9,3,2,0,0",
                "GHI,1985-1989,E,1989-1992,2,2,0,0,1,1",
                "JKL,2000-2002,X0000003,2000-2002,0,0,0,0,0,0",
                "GHI,1985-1989,E,1989-1990,1,1,0,0,1,1",
                "JKL,2001-2003,X0000003,1999,6",
            };


            // Build a big MeSH file -- we'll create a very long mesh report that matches the
            // report file above using randomly selected words from the following list.
            string[] words = new string[] { 
                "Female", "Humans", "Middle Aged", "Logistic Models", "Genotype", "Risk Factors", 
                "Genetic Predisposition to Disease", "Membrane Proteins", "China", "DNA Fingerprinting",
                "Polymorphism Single Nucleotide", "Menopause", "Adenocarcinoma", "Asian Continental Ancestry Group", 
                "Breast Neoplasms", "Cyclooxygenase 2", "Humans", "Male", "Middle Aged", 
                "Logistic Models", "Prospective Studies", "Case-Control Studies", "Aged", 
                "Odds Ratio", "Questionnaires", "Linear Models", "Mass Screening", 
                "Anticarcinogenic Agents", "Carotenoids", "Confidence Intervals", "Prostatic Neoplasms",
                "Risk Assessment", "beta Carotene", };


            string[] mesh = new string[1];
            mesh[0] = "setnb,range,heading,count";

            bool firstScientist = true;
            int reportRow = 1; // skip the header row
            while (reportRow < report.Length - 2) {
                string[] columns = report[reportRow].Split(new char[] { ',' });

                string scientist;
                string window;
                int count;
                if (firstScientist)
                {
                    scientist = columns[0];
                    window = columns[1];
                    count = int.Parse(columns[4]);
                }
                else
                {
                    scientist = columns[2];
                    window = columns[3];
                    count = int.Parse(columns[6]);
                }

                Random random = new Random();

                for (int word = 0; word < count; word++)
                {
                    Array.Resize(ref mesh, mesh.Length + 1);
                    mesh[mesh.Length - 1] = String.Format("{0},{1},{2},{3}", scientist, window,
                        words[random.Next(words.Length)], random.Next(2, 12));
                }

                if (!firstScientist)
                    reportRow++;
                firstScientist = !firstScientist;
            }


            List<InputRow> expected = new List<InputRow>()
            {
                new InputRow() { Scientist1 = "GHI", Window1 = "1985-1989", Scientist2 = "E", Window2 = "1989-1992" },
                new InputRow() { Scientist1 = "JKL", Window1 = "2000-2002", Scientist2 = "X0000003", Window2 = "2000-2002" },
                new InputRow() { Scientist1 = "GHI", Window1 = "1985-1989", Scientist2 = "E", Window2 = "2000-2002" },
                new InputRow() { Scientist1 = "JKL", Window1 = "2001-2003", Scientist2 = "X0000003", Window2 = "1999" },
                new InputRow() { Scientist1 = "X0000001", Window1 = "2000-2002", Scientist2 = "X0000003", Window2 = "2000-2002" },
                new InputRow() { Scientist1 = "X0000002", Window1 = "2000-2002", Scientist2 = "X0000003", Window2 = "2000-2002" },
            };

            

            WriteFiles(report, mesh);


            List<InputRow> actual = Files.FaultTolerantCopy("report.csv", "mesh.csv", false, inputRows);

            CompareExpectedAndActual(expected, actual, "GeneratedFileTest()");

            CompareFile("report.csv.bak", report, report.Length);
            CompareFile("mesh.csv.bak", mesh, mesh.Length);

            CompareFile("report.csv", report, report.Length - 4);
            CompareFile("mesh.csv", mesh, mesh.Length - 2);
        }


    }
}
