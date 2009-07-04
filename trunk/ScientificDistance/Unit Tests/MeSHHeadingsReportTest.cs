using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ScientificDistance.Unit_Tests
{
    [TestFixture]
    public class MeSHHeadingsReportTest
    {
        Publications x0000005pubs;
        Publications x0000006pubs;

        [TestFixtureSetUp]
        public void CreateData()
        {
            TestDatabase.Rebuild();
            x0000005pubs = new Publications(new Database(TestDatabase.DSN), "X0000005", "2007", null);
            x0000006pubs = new Publications(new Database(TestDatabase.DSN), "X0000006", "2001-2006", null);
        }

        [Test]
        public void x0000005rows()
        {
            string[] expected = new string[] {
                "Adenocarcinoma,1",
                "Adult,1",
                "Aged,2",
                "Anticarcinogenic Agents,1",
                "Asian Continental Ancestry Group,1",
                "Breast Neoplasms,1",
                "Carotenoids,1",
                "Case-Control Studies,1",
                "China,2",
                "Colorectal Neoplasms,1",
                "Confidence Intervals,1",
                "Cyclooxygenase 2,1",
                "DNA Fingerprinting,1",
                "Epidemiologic Studies,1",
                "Female,2",
                "\"Genes, APC\",1",
                "Genetic Predisposition to Disease,1",
                "Genotype,1",
                "Humans,3",
                "Linear Models,1",
                "Logistic Models,2",
                "Male,2",
                "Mass Screening,2",
                "Membrane Proteins,1",
                "Menopause,1",
                "Middle Aged,3",
                "Molecular Diagnostic Techniques,1",
                "Mutation,1",
                "Odds Ratio,1",
                "\"Polymorphism, Single Nucleotide\",1",
                "Prospective Studies,1",
                "Prostatic Neoplasms,1",
                "Questionnaires,1",
                "Risk Assessment,1",
                "Risk Factors,2",
                "beta Carotene,1",
            };

            string[] actual = Overlap.GenerateMeSHHeadingsReportRows(x0000005pubs.Setnb, x0000005pubs.Window, Overlap.KeywordCounts(x0000005pubs));

            Assert.AreEqual(expected.Length, actual.Length, "Unexpected report length");

            foreach (string row in expected)
                Assert.Contains("X0000005,2007," + row, actual);
        }


        [Test]
        public void x0000006rows()
        {
            string[] expected = new string[] {
                "Adult,1",
                "Aged,2",
                "Alleles,1",
                "Case-Control Studies,1",
                "Confidence Intervals,1",
                "Coronary Disease,1",
                "DNA Mutational Analysis,1",
                "Female,2",
                "\"Genes, cdc\",1",
                "Genetic Predisposition to Disease,2",
                "Genetic Screening,1",
                "Genotype,1",
                "Humans,2",
                "Lung Neoplasms,1",
                "Male,2",
                "Mass Screening,1",
                "Middle Aged,2",
                "Odds Ratio,1",
                "Pedigree,1",
                "\"Polymorphism, Genetic\",1",
                "Population Surveillance,1",
                "Proto-Oncogene Proteins c-mdm2,1",
                "Risk Assessment,1",
                "Risk Factors,1",
                "Smoking,1",
                "Survival Analysis,1",
                "Tumor Suppressor Protein p53,1",
                "United States,1",
            };

            string[] actual = Overlap.GenerateMeSHHeadingsReportRows(x0000006pubs.Setnb, x0000006pubs.Window, Overlap.KeywordCounts(x0000006pubs));

            Assert.AreEqual(expected.Length, actual.Length, "Unexpected report length");

            foreach (string row in expected)
                Assert.Contains("X0000006,2001-2006," + row, actual);
        }


        [Test]
        public void NullSetnb()
        {
            try
            {
                string[] actual = Overlap.GenerateMeSHHeadingsReportRows(null, x0000006pubs.Window, Overlap.KeywordCounts(x0000006pubs));
                Assert.Fail("Null setnb parameter should throw an error");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentNullException);
            }
        }


        [Test]
        public void NullWindow()
        {
            try
            {
                string[] actual = Overlap.GenerateMeSHHeadingsReportRows(x0000006pubs.Setnb, null, Overlap.KeywordCounts(x0000006pubs));
                Assert.Fail("Null setnb parameter should throw an error");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentNullException);
            }
        }


        [Test]
        public void NullCounts()
        {
            try
            {
                string[] actual = Overlap.GenerateMeSHHeadingsReportRows(x0000006pubs.Setnb, x0000006pubs.Window, null);
                Assert.Fail("Null setnb parameter should throw an error");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentNullException);
            }
        }


        [Test]
        public void UnexpectedDictionaryEntries()
        {
            Dictionary<string, int> counts = new Dictionary<string, int>() {
                {"one", -46},
                {"two", 419381851},
                {"three", 0},
                {"", 41},
                {"this is a very long keyword this is a very long keyword this is a very long keyword this is a very long keyword this is a very long keyword this is a very long keyword this is a very long keyword", 3},
                {"some \"quotes\"\" and ,,,, commas and \",\"\" more quotes", 6},
            };

            string[] actual = Overlap.GenerateMeSHHeadingsReportRows("setnb", "", counts);

            Assert.AreEqual(actual.Length, 6);
            Assert.Contains("setnb,,one,-46", actual);
            Assert.Contains("setnb,,two,419381851", actual);
            Assert.Contains("setnb,,three,0", actual);
            Assert.Contains("setnb,,,41", actual);
            Assert.Contains("setnb,,this is a very long keyword this is a very long keyword this is a very long keyword this is a very long keyword this is a very long keyword this is a very long keyword this is a very long keyword,3", actual);
            Assert.Contains("setnb,,\"some \"\"quotes\"\"\"\" and ,,,, commas and \"\",\"\"\"\" more quotes\",6", actual);
        }


    }
}
