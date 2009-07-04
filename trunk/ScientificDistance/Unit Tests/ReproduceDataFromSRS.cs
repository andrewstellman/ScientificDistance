using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ScientificDistance.Unit_Tests
{
    /// <summary>
    /// Secion 2.1.3 of the SRS contains an example of the calculation.
    /// This unit test reproduces that calculation by retrieving the 
    /// publications from the database, checking the keyword lists, 
    /// finding the overlap, and generating the output row.
    /// </summary>
    [TestFixture]
    public class ReproduceDataFromSRS
    {
        private Publications x0000005pubs;
        private Publications x0000006pubs;

        [TestFixtureSetUp]
        public void CreateData()
        {
            TestDatabase.Rebuild();
            x0000005pubs = new Publications(new Database(TestDatabase.DSN), "X0000005", "2007", null);
            x0000006pubs = new Publications(new Database(TestDatabase.DSN), "X0000006", "2001-2006", null);
        }

        [Test]
        public void CheckPublicationsForX0000005()
        {
            Assert.AreEqual(x0000005pubs.PMIDs.Length, 3);
            Assert.Contains(17479405, x0000005pubs.PMIDs);
            Assert.Contains(17507623, x0000005pubs.PMIDs);
            Assert.Contains(17653897, x0000005pubs.PMIDs);
            Assert.AreEqual("2007", x0000005pubs.Window);
            Assert.AreEqual("X0000005", x0000005pubs.Setnb);
        }

        [Test]
        public void CheckPublicationsForX0000006()
        {
            Assert.AreEqual(x0000006pubs.PMIDs.Length, 2);
            Assert.Contains(16287156, x0000006pubs.PMIDs);
            Assert.Contains(11137774, x0000006pubs.PMIDs);
            Assert.AreEqual("2001-2006", x0000006pubs.Window);
            Assert.AreEqual("X0000006", x0000006pubs.Setnb);
        }

        [Test]
        public void CheckHeadingsFor17479405()
        {
            List<string> headings = new List<string>()
            {
                "Female",
                "Humans",
                "Middle Aged",
                "Logistic Models",
                "Genotype",
                "Risk Factors",
                "Genetic Predisposition to Disease",
                "Membrane Proteins",
                "China",
                "DNA Fingerprinting",
                "Polymorphism, Single Nucleotide",
                "Menopause",
                "Adenocarcinoma",
                "Asian Continental Ancestry Group",
                "Breast Neoplasms",
                "Cyclooxygenase 2",
            };
            Assert.AreEqual(headings.Count(), x0000005pubs.Headings(17479405).Count());
            foreach (string heading in headings)
                Assert.Contains(heading, x0000005pubs.Headings(17479405));
        }

        [Test]
        public void CheckHeadingsFor17507623()
        {
            List<string> headings = new List<string>()
            {
                "Humans",
                "Male",
                "Middle Aged",
                "Logistic Models",
                "Prospective Studies",
                "Case-Control Studies",
                "Aged",
                "Odds Ratio",
                "Questionnaires",
                "Linear Models",
                "Mass Screening",
                "Anticarcinogenic Agents",
                "Carotenoids",
                "Confidence Intervals",
                "Prostatic Neoplasms",
                "Risk Assessment",
                "beta Carotene",
            };
            Assert.AreEqual(headings.Count(), x0000005pubs.Headings(17507623).Count());
            foreach (string heading in headings)
                Assert.Contains(heading, x0000005pubs.Headings(17507623));
        }

        [Test]
        public void CheckHeadingsFor17653897()
        {
            List<string> headings = new List<string>()
            {
                "Adult",
                "Aged",
                "China",
                "Colorectal Neoplasms",
                "Epidemiologic Studies",
                "Female",
                "Genes, APC",
                "Humans",
                "Male",
                "Mass Screening",
                "Middle Aged",
                "Molecular Diagnostic Techniques",
                "Mutation",
                "Risk Factors",
            };
            Assert.AreEqual(headings.Count(), x0000005pubs.Headings(17653897).Count());
            foreach (string heading in headings)
                Assert.Contains(heading, x0000005pubs.Headings(17653897));
        }

        [Test]
        public void CheckHeadingsFor16287156()
        {
            List<string> headings = new List<string>()
            {
                "Female",
                "Humans",
                "Male",
                "Middle Aged",
                "Case-Control Studies",
                "Genotype",
                "Aged",
                "DNA Mutational Analysis",
                "Risk Factors",
                "Genetic Predisposition to Disease",
                "Polymorphism, Genetic",
                "Alleles",
                "Lung Neoplasms",
                "Genes, cdc",
                "Proto-Oncogene Proteins c-mdm2",
                "Smoking",
                "Tumor Suppressor Protein p53",
            };
            Assert.AreEqual(headings.Count(), x0000006pubs.Headings(16287156).Count());
            foreach (string heading in headings)
                Assert.Contains(heading, x0000006pubs.Headings(16287156));
        }

        [Test]
        public void CheckHeadingsFor11137774()
        {
            List<string> headings = new List<string>()
            {
                "Adult",
                "Aged",
                "Confidence Intervals",
                "Coronary Disease",
                "Female",
                "Genetic Predisposition to Disease",
                "Genetic Screening",
                "Humans",
                "Male",
                "Mass Screening",
                "Middle Aged",
                "Odds Ratio",
                "Pedigree",
                "Population Surveillance",
                "Risk Assessment",
                "Survival Analysis",
                "United States",
            };
            Assert.AreEqual(headings.Count(), x0000006pubs.Headings(11137774).Count());
            foreach (string heading in headings)
                Assert.Contains(heading, x0000006pubs.Headings(11137774));
        }

        [Test]
        public void CheckKeywordsForX0000005()
        {
            Dictionary<string, int> counts = Overlap.KeywordCounts(x0000005pubs);
            Dictionary<string, int> countsFromSRS = new Dictionary<string, int>() {
                {"Adenocarcinoma", 1},
                {"Adult", 1},
                {"Aged", 2},
                {"Anticarcinogenic Agents", 1},
                {"Asian Continental Ancestry Group", 1},
                {"Breast Neoplasms", 1},
                {"Carotenoids", 1},
                {"Case-Control Studies", 1},
                {"China", 2},
                {"Colorectal Neoplasms", 1},
                {"Confidence Intervals", 1},
                {"Cyclooxygenase 2", 1},
                {"DNA Fingerprinting", 1},
                {"Epidemiologic Studies", 1},
                {"Female", 2},
                {"Genes, APC", 1},
                {"Genetic Predisposition to Disease", 1},
                {"Genotype", 1},
                {"Humans", 3},
                {"Linear Models", 1},
                {"Logistic Models", 2},
                {"Male", 2},
                {"Mass Screening", 2},
                {"Membrane Proteins", 1},
                {"Menopause", 1},
                {"Middle Aged", 3},
                {"Molecular Diagnostic Techniques", 1},
                {"Mutation", 1},
                {"Odds Ratio", 1},
                {"Polymorphism, Single Nucleotide", 1},
                {"Prospective Studies", 1},
                {"Prostatic Neoplasms", 1},
                {"Questionnaires", 1},
                {"Risk Assessment", 1},
                {"Risk Factors", 2},
                {"beta Carotene", 1},
            };

            string[] keywords = counts.Keys.ToArray();
            string[] keywordsFromSRS = countsFromSRS.Keys.ToArray();
            Assert.AreEqual(keywords.Count(), keywordsFromSRS.Count());
            foreach (string keyword in keywords)
            {
                Assert.Contains(keyword, keywordsFromSRS);
                Assert.AreEqual(counts[keyword], countsFromSRS[keyword]);
            }

        }

        [Test]
        public void CheckKeywordsForX0000006()
        {
            Dictionary<string, int> counts = Overlap.KeywordCounts(x0000006pubs);
            Dictionary<string, int> countsFromSRS = new Dictionary<string, int>() {
                {"Adult",1},
                {"Aged",2},
                {"Alleles",1},
                {"Case-Control Studies",1},
                {"Confidence Intervals",1},
                {"Coronary Disease",1},
                {"DNA Mutational Analysis",1},
                {"Female",2},
                {"Genes, cdc",1},
                {"Genetic Predisposition to Disease",2},
                {"Genetic Screening",1},
                {"Genotype",1},
                {"Humans",2},
                {"Lung Neoplasms",1},
                {"Male",2},
                {"Mass Screening",1},
                {"Middle Aged",2},
                {"Odds Ratio",1},
                {"Pedigree",1},
                {"Polymorphism, Genetic",1},
                {"Population Surveillance",1},
                {"Proto-Oncogene Proteins c-mdm2",1},
                {"Risk Assessment",1},
                {"Risk Factors",1},
                {"Smoking",1},
                {"Survival Analysis",1},
                {"Tumor Suppressor Protein p53",1},
                {"United States",1},
            };

            string[] keywords = counts.Keys.ToArray();
            string[] keywordsFromSRS = countsFromSRS.Keys.ToArray();
            Assert.AreEqual(keywords.Count(), keywordsFromSRS.Count());
            foreach (string keyword in keywords)
            {
                Assert.Contains(keyword, keywordsFromSRS);
                Assert.AreEqual(counts[keyword], countsFromSRS[keyword]);
            }

        }

        [Test]
        public void CheckIntersection()
        {
            Dictionary<string, int> counts1 = Overlap.KeywordCounts(x0000005pubs);
            Dictionary<string, int> counts2 = Overlap.KeywordCounts(x0000006pubs);
            Dictionary<string, int> intersection = Overlap.Intersection(counts1, counts2);

            Dictionary<string, int> intersectionFromSRS = new Dictionary<string, int>() {
                {"Adult",1},
                {"Aged",2},
                {"Case-Control Studies",1},
                {"Confidence Intervals",1},
                {"Female",2},
                {"Genetic Predisposition to Disease",1},
                {"Genotype",1},
                {"Humans",2},
                {"Male",2},
                {"Mass Screening",1},
                {"Middle Aged",2},
                {"Odds Ratio",1},
                {"Risk Assessment",1},
                {"Risk Factors",1},
            };

            string[] keywords = intersection.Keys.ToArray();
            string[] keywordsFromSRS = intersectionFromSRS.Keys.ToArray();
            Assert.AreEqual(keywords.Count(), keywordsFromSRS.Count());
            foreach (string keyword in keywords)
            {
                Assert.Contains(keyword, keywordsFromSRS);
                Assert.AreEqual(intersection[keyword], intersectionFromSRS[keyword]);
            }
        }

        [Test]
        public void CheckReportRow()
        {
            string expectedOutput = "X0000005,2007,X0000006,2001-2006,36,47,28,34,14,19";
            string actualOutput = Overlap.GenerateOverlapReportRow(x0000005pubs, x0000006pubs);
            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}