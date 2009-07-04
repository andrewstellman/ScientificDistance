using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;

namespace ScientificDistance.Unit_Tests
{
    [TestFixture]
    public class GeneratorTest
    {

        public string[] input = new string[] 
        {
            "setnb1,range1,setnb2,range2",
            "X0000001,2000-2005,X0000002,2000-2005",
            "X0000003,2002,X0000004,1992-1997",
            "X0000005,,X0000006,",
            "X0000001,,X0000004,",
            "X0000003,2002,X0000004,1992-1997",
            "X0000002,1999,X0000003,2001",
            "X0000003,2002,Z1234567,",
        };

        public string[] output = new string[]
        {
            "setnb1,range1,setnb2,range2,nb_unq_keywords_1,nb_frq_keywords_1,nb_unq_keywords_2,nb_frq_keywords_2,nb_unq_keywords_ovrlp,nb_frq_keywords_ovrlp",
            "X0000001,2000-2005,X0000002,2000-2005,37,52,59,119,15,25",
            "X0000003,2002,X0000004,1992-1997,30,39,46,64,4,4",
            "X0000005,,X0000006,,36,47,28,34,14,19",
            "X0000001,,X0000004,,106,217,72,116,1,1",
            "X0000003,2002,X0000004,1992-1997,30,39,46,64,4,4",
            "X0000002,1999,X0000003,2001,0,0,0,0,0,0",
            "X0000003,2002,Z1234567,,30,39,0,0,0,0",
        };


        public string[] partialMeshReport = new string[] {
            "setnb,range,heading,count",
            "X0000001,2000-2005,Adaptation, Psychological,1",
            "X0000001,2000-2005,Adult,2",
            "X0000001,2000-2005,Arousal,1",
            "X0000001,2000-2005,Asian Continental Ancestry Group,1",
            "X0000001,2000-2005,Child,1",
            "X0000001,2000-2005,Child, Preschool,1",
            "X0000001,2000-2005,Cocaine-Related Disorders,1",
            "X0000001,2000-2005,Cross-Cultural Comparison,1",
            "X0000001,2000-2005,Defense Mechanisms,1",
            "X0000001,2000-2005,Female,3",
            "X0000001,2000-2005,Follow-Up Studies,2",
            "X0000001,2000-2005,\"Health Knowledge, Attitudes, Practice\",1",
            "X0000001,2000-2005,Humans,3",
            "X0000001,2000-2005,Infant,2",
            "X0000001,2000-2005,Infant Behavior,1",
            "X0000001,2000-2005,\"Infant, Newborn\",2",
            "X0000001,2000-2005,Intensive Care Units, Neonatal,1",
            "X0000001,2000-2005,Internal-External Control,1",
            "X0000001,2000-2005,Irritable Mood,1",
            "X0000001,2000-2005,Japan,1",
            "X0000001,2000-2005,Male,3",
            "X0000001,2000-2005,Maternal Behavior,1",
            "X0000001,2000-2005,Mother-Child Relations,2",
            "X0000001,2000-2005,Mothers,1",
            "X0000001,2000-2005,Neurologic Examination,1",
            "X0000001,2000-2005,Nursing Methodology Research,1",
            "X0000001,2000-2005,Object Attachment,3",
            "X0000001,2000-2005,Parent-Child Relations,1",
            "X0000001,2000-2005,Parenting,2",
            "X0000001,2000-2005,Parents,1",
            "X0000001,2000-2005,Personality Assessment,1",
            "X0000001,2000-2005,Pregnancy,1",
            "X0000001,2000-2005,Pregnancy Complications,1",
            "X0000001,2000-2005,Prognosis,1",
            "X0000001,2000-2005,Skin Pigmentation,1",
            "X0000001,2000-2005,Startle Reaction,1",
            "X0000001,2000-2005,\"Stress, Psychological\",2",
            "X0000002,2000-2005,Adaptation, Psychological,2",
            "X0000002,2000-2005,Adolescent,3",
            "X0000002,2000-2005,Adoption,1",
            "X0000002,2000-2005,Adult,4",
            "X0000002,2000-2005,Age Factors,1",
            "X0000002,2000-2005,Analysis of Variance,2",
            "X0000002,2000-2005,Anxiety Disorders,3",
            "X0000002,2000-2005,Child,6",
            "X0000002,2000-2005,Child Behavior,2",
            "X0000002,2000-2005,Child Development,2",
            "X0000002,2000-2005,Child of Impaired Parents,1",
            "X0000002,2000-2005,\"Child, Preschool\",4",
            "X0000002,2000-2005,Conflict (Psychology),2",
            "X0000002,2000-2005,Depression,1",
            "X0000002,2000-2005,Depressive Disorder,1",
            "X0000002,2000-2005,Divorce,3",
            "X0000002,2000-2005,Educational Status,1",
            "X0000002,2000-2005,Family,1",
            "X0000002,2000-2005,Family Relations,2",
            "X0000002,2000-2005,Father-Child Relations,1",
            "X0000002,2000-2005,Fathers,1",
            "X0000002,2000-2005,Female,7",
            "X0000002,2000-2005,Functional Laterality,1",
            "X0000002,2000-2005,Genetic Predisposition to Disease,2",
            "X0000002,2000-2005,Gestational Age,1",
            "X0000002,2000-2005,Great Britain,2",
            "X0000002,2000-2005,Humans,8",
            "X0000002,2000-2005,Individuality,1",
            "X0000002,2000-2005,\"Infant, Newborn\",1",
            "X0000002,2000-2005,Life Change Events,1",
            "X0000002,2000-2005,Longitudinal Studies,2",
            "X0000002,2000-2005,Male,7",
            "X0000002,2000-2005,Marriage,1",
            "X0000002,2000-2005,\"Models, Psychological\",1",
            "X0000002,2000-2005,Mother-Child Relations,3",
            "X0000002,2000-2005,Mothers,1",
            "X0000002,2000-2005,Negativism,1",
            "X0000002,2000-2005,Obsessive-Compulsive Disorder,1",
            "X0000002,2000-2005,Parent-Child Relations,2",
            "X0000002,2000-2005,Parenting,2",
            "X0000002,2000-2005,Phenotype,1",
            "X0000002,2000-2005,Pilot Projects,1",
            "X0000002,2000-2005,Population Surveillance,1",
            "X0000002,2000-2005,Pregnancy,2",
            "X0000002,2000-2005,Pregnancy Complications,2",
            "X0000002,2000-2005,Prenatal Exposure Delayed Effects,2",
            "X0000002,2000-2005,Prospective Studies,3",
            "X0000002,2000-2005,Psychiatric Status Rating Scales,1",
            "X0000002,2000-2005,Questionnaires,2",
            "X0000002,2000-2005,Residence Characteristics,1",
            "X0000002,2000-2005,Risk Factors,2",
            "X0000002,2000-2005,Sampling Studies,1",
            "X0000002,2000-2005,Self Concept,1",
            "X0000002,2000-2005,Siblings,1",
            "X0000002,2000-2005,Single Parent,1",
            "X0000002,2000-2005,Social Adjustment,2",
            "X0000002,2000-2005,Social Perception,1",
            "X0000002,2000-2005,Spouses,1",
            "X0000002,2000-2005,\"Stress, Psychological\",4",
            "X0000003,2002,Acute Toxicity Tests,1",
            "X0000003,2002,\"Algae, Green\",1",
            "X0000003,2002,\"Algae, Red\",1",
            "X0000003,2002,Animals,2",
            "X0000003,2002,Biological Assay,1",
            "X0000003,2002,Biological Availability,1",
            "X0000003,2002,Calcium,1",
            "X0000003,2002,Carbon,1",
            "X0000003,2002,Cations,1",
            "X0000003,2002,Cities,1",
            "X0000003,2002,Copper,1",
            "X0000003,2002,Cupriavidus necator,1",
            "X0000003,2002,Daphnia,2",
            "X0000003,2002,Environmental Monitoring,1",
            "X0000003,2002,Forecasting,2",
            "X0000003,2002,Housing,1",
            "X0000003,2002,Hydrogen-Ion Concentration,1",
            "X0000003,2002,Lethal Dose 50,1",
            "X0000003,2002,Ligands,3",
            "X0000003,2002,Manufactured Materials,1",
            "X0000003,2002,\"Models, Biological\",2",
            "X0000003,2002,\"Models, Theoretical\",1",
            "X0000003,2002,Predictive Value of Tests,1",
            "X0000003,2002,Rain,1",
            "X0000003,2002,Regression Analysis,1",
            "X0000003,2002,Solubility,1",
            "X0000003,2002,Sweden,1",
            "X0000003,2002,Water,2",
            "X0000003,2002,Water Pollutants,1",
            "X0000003,2002,Zinc,3",
            "X0000004,1992-1997,Aluminum Oxide,1",
            "X0000004,1992-1997,Aluminum Silicates,3",
            "X0000004,1992-1997,Animals,1",
            "X0000004,1992-1997,Biocompatible Materials,3",
            "X0000004,1992-1997,Biological Availability,1",
            "X0000004,1992-1997,Biomedical Engineering,1",
            "X0000004,1992-1997,Bone Regeneration,1",
            "X0000004,1992-1997,Calcium Phosphates,2",
            "X0000004,1992-1997,Carbonates,1",
            "X0000004,1992-1997,Ceramics,1",
            "X0000004,1992-1997,\"Chemistry, Physical\",1",
            "X0000004,1992-1997,Cold,1",
            "X0000004,1992-1997,Composite Resins,1",
            "X0000004,1992-1997,Corrosion,2",
            "X0000004,1992-1997,Desiccation,1",
            "X0000004,1992-1997,Drug Design,1",
            "X0000004,1992-1997,Durapatite,4",
            "X0000004,1992-1997,Elasticity,1",
            "X0000004,1992-1997,Evaluation Studies as Topic,1",
            "X0000004,1992-1997,Forecasting,1",
            "X0000004,1992-1997,Fractals,1",
            "X0000004,1992-1997,Gels,1",
            "X0000004,1992-1997,Heat,2",
            "X0000004,1992-1997,Hydroxyapatites,2",
            "X0000004,1992-1997,Ion Exchange,1",
            "X0000004,1992-1997,Materials Testing,3",
            "X0000004,1992-1997,\"Microscopy, Atomic Force\",1",
            "X0000004,1992-1997,\"Microscopy, Electron, Scanning\",1",
            "X0000004,1992-1997,Oxidation-Reduction,1",
            "X0000004,1992-1997,Porosity,2",
            "X0000004,1992-1997,Potassium,1",
            "X0000004,1992-1997,Potassium Chloride,1",
            "X0000004,1992-1997,Pressure,1",
            "X0000004,1992-1997,Prostheses and Implants,1",
            "X0000004,1992-1997,Rabbits,1",
            "X0000004,1992-1997,Silicon Dioxide,1",
            "X0000004,1992-1997,Solubility,2",
            "X0000004,1992-1997,Solutions,2",
            "X0000004,1992-1997,Spectrum Analysis,1",
            "X0000004,1992-1997,\"Stress, Mechanical\",1",
            "X0000004,1992-1997,Surface Properties,3",
            "X0000004,1992-1997,Tensile Strength,1",
            "X0000004,1992-1997,Thermogravimetry,1",
            "X0000004,1992-1997,Titanium,1",
            "X0000004,1992-1997,Tromethamine,1",
            "X0000004,1992-1997,X-Ray Diffraction,1",
            "X0000005,Adenocarcinoma,1",
            "X0000005,Adult,1",
            "X0000005,Aged,2",
        };


        public string[] rollingInput = new string[] 
        {
            "setnb1,range1,setnb2,range2",
            "X0000001,2000-2005,X0000002,2000-2005",
            "X0000005,,X0000004,",
            "X0000003,2002,X0000004,1992-1997",
        };

        public string[] rolling3Output = new string[]
        {
            "setnb1,range1,setnb2,range2,nb_unq_keywords_1,nb_frq_keywords_1,nb_unq_keywords_2,nb_frq_keywords_2,nb_unq_keywords_ovrlp,nb_frq_keywords_ovrlp",
            "X0000001,1993-1995,X0000002,1993-1995,19,19,15,15,3,3",
            "X0000001,1994-1996,X0000002,1994-1996,31,36,15,15,3,3",
            "X0000001,1995-1997,X0000002,1995-1997,43,66,15,15,3,3",
            "X0000001,1996-1998,X0000002,1996-1998,36,47,0,0,0,0",
            "X0000001,1997-1999,X0000002,1997-1999,28,45,0,0,0,0",
            "X0000001,1998-2000,X0000002,1998-2000,24,33,32,36,7,8",
            "X0000001,1999-2001,X0000002,1999-2001,24,33,32,36,7,8",
            "X0000001,2000-2002,X0000002,2000-2002,18,18,32,36,7,7",
            "X0000001,2001-2003,X0000002,2001-2003,13,13,20,26,5,5",
            "X0000001,2002-2004,X0000002,2002-2004,13,13,32,53,6,6",
            "X0000001,2003-2005,X0000002,2003-2005,28,34,46,83,11,15",
            "X0000001,2004-2006,X0000002,2004-2006,21,21,45,75,8,8",
            "X0000001,2005-2007,X0000002,2005-2007,21,21,54,95,8,8",
            "X0000003,2000-2002,X0000004,2000-2002,30,39,25,28,2,2",
            "X0000003,2001-2003,X0000004,2001-2003,41,83,30,40,3,3",
            "X0000003,2002-2004,X0000004,2002-2004,43,92,12,12,0,0",
            "X0000003,2003-2005,X0000004,2003-2005,43,79,12,12,0,0",
        };

        public string[] rolling5Output = new string[]
        {
            "setnb1,range1,setnb2,range2,nb_unq_keywords_1,nb_frq_keywords_1,nb_unq_keywords_2,nb_frq_keywords_2,nb_unq_keywords_ovrlp,nb_frq_keywords_ovrlp",
            "X0000001,1991-1995,X0000002,1991-1995,21,23,15,15,3,3",
            "X0000001,1992-1996,X0000002,1992-1996,32,40,15,15,3,3",
            "X0000001,1993-1997,X0000002,1993-1997,43,66,15,15,3,3",
            "X0000001,1994-1998,X0000002,1994-1998,43,66,15,15,3,3",
            "X0000001,1995-1999,X0000002,1995-1999,46,81,15,15,3,3",
            "X0000001,1996-2000,X0000002,1996-2000,47,80,32,36,8,9",
            "X0000001,1997-2001,X0000002,1997-2001,37,63,32,36,8,9",
            "X0000001,1998-2002,X0000002,1998-2002,24,33,32,36,7,8",
            "X0000001,1999-2003,X0000002,1999-2003,31,46,43,62,14,21",
            "X0000001,2000-2004,X0000002,2000-2004,26,31,52,89,14,18",
            "X0000001,2001-2005,X0000002,2001-2005,28,34,46,83,11,15",
            "X0000001,2002-2006,X0000002,2002-2006,28,34,50,101,11,15",
            "X0000001,2003-2007,X0000002,2003-2007,28,34,69,148,12,16",
            "X0000001,2004-2008,X0000002,2004-2008,21,21,65,122,9,9",
            "X0000001,2005-2009,X0000002,2005-2009,21,21,54,95,8,8",
            "X0000005,2003-2007,X0000004,2003-2007,36,47,12,12,0,0",
            "X0000003,1998-2002,X0000004,1998-2002,30,39,34,40,2,2",
            "X0000003,1999-2003,X0000004,1999-2003,41,83,30,40,3,3",
            "X0000003,2000-2004,X0000004,2000-2004,43,92,30,40,3,3",
            "X0000003,2001-2005,X0000004,2001-2005,53,118,30,40,3,3",
            "X0000003,2002-2006,X0000004,2002-2006,61,142,12,12,0,0",
            "X0000003,2003-2007,X0000004,2003-2007,51,103,12,12,0,0",
        };

        [SetUp]
        public void SetUp()
        {
            TestDatabase.Rebuild();

            File.Delete("input.csv");
            File.Delete("report.csv");
            File.Delete("mesh.csv");
            File.Delete("report.csv.bak");
            File.Delete("mesh.csv.bak");
            warnings = new List<string>();
            progresses = new List<ReportGenerator.ProgressEventArgs>();
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete("input.csv");
            File.Delete("report.csv");
            File.Delete("mesh.csv");
        }

        private List<string> warnings;
        private List<ReportGenerator.ProgressEventArgs> progresses;

        /// <summary>
        /// Event handler for the Warning event
        /// </summary>
        void generator_Warning(object sender, EventArgs e)
        {
            if (e is ReportGenerator.WarningEventArgs)
            {
                ReportGenerator.WarningEventArgs args = e as ReportGenerator.WarningEventArgs;
                warnings.Add(args.Message);
            }
            else
                Assert.Fail("Invalid EventArgs for Warning");
        }

        /// <summary>
        /// Event handler for Progress event
        /// </summary>
        void generator_Progress(object sender, EventArgs e)
        {
            if (e is ReportGenerator.ProgressEventArgs)
            {
                ReportGenerator.ProgressEventArgs args = e as ReportGenerator.ProgressEventArgs;
                progresses.Add(args);
            }
            else
                Assert.Fail("Invalid EventArgs for Progress");
        }

        /// <summary>
        /// Standard report - no fault tolerance
        /// </summary>
        [Test]
        public void GenerateReport()
        {
            File.WriteAllLines("input.csv", input);
            ReportGenerator generator = new ReportGenerator(TestDatabase.DSN, TestDatabase.DSN,
                "input.csv", "report.csv", "mesh.csv", ReportGenerator.RollingWindow.none, false, false);
            Assert.AreEqual(TestDatabase.DSN, generator.DSN1);
            Assert.AreEqual(TestDatabase.DSN, generator.DSN2);
            Assert.AreEqual("input.csv", generator.InputFilename);
            Assert.AreEqual("report.csv", generator.ReportFilename);
            Assert.AreEqual("mesh.csv", generator.MeshFilename);
            Assert.AreEqual(ReportGenerator.RollingWindow.none, generator.Window);
            Assert.IsFalse(generator.FaultTolerance);
            Assert.IsFalse(generator.RemoveCommonPublications);

            generator.Warning += new EventHandler(generator_Warning);
            generator.Progress += new EventHandler(generator_Progress);

            generator.Generate();
            
            Assert.AreEqual(0, warnings.Count);
            Assert.AreEqual(9, progresses.Count);

            CompareFiles("report.csv", output);
            CheckMeshReportAgainstReport("mesh.csv", output);
        }

        /// <summary>
        /// 3 year rolling window report
        /// </summary>
        [Test]
        public void Rolling3Year()
        {
            File.WriteAllLines("input.csv", rollingInput);
            ReportGenerator generator = new ReportGenerator(TestDatabase.DSN, TestDatabase.DSN,
                "input.csv", "report.csv", "mesh.csv", ReportGenerator.RollingWindow.threeYear, false, false);
            Assert.AreEqual(TestDatabase.DSN, generator.DSN1);
            Assert.AreEqual(TestDatabase.DSN, generator.DSN2);
            Assert.AreEqual("input.csv", generator.InputFilename);
            Assert.AreEqual("report.csv", generator.ReportFilename);
            Assert.AreEqual("mesh.csv", generator.MeshFilename);
            Assert.AreEqual(ReportGenerator.RollingWindow.threeYear, generator.Window);
            Assert.IsFalse(generator.FaultTolerance);
            Assert.IsFalse(generator.RemoveCommonPublications);

            generator.Warning += new EventHandler(generator_Warning);
            generator.Progress += new EventHandler(generator_Progress);

            generator.Generate();

            Assert.AreEqual(4, warnings.Count); // Four warnings that we're ignoring windows in the input file
            Assert.AreEqual(5, progresses.Count);

            CompareFiles("report.csv", rolling3Output);
            CheckMeshReportAgainstReport("mesh.csv", rolling3Output);
        }

        /// <summary>
        /// 5 year rolling window report
        /// </summary>
        [Test]
        public void Rolling5Year()
        {
            File.WriteAllLines("input.csv", rollingInput);
            ReportGenerator generator = new ReportGenerator(TestDatabase.DSN, TestDatabase.DSN,
                "input.csv", "report.csv", "mesh.csv", ReportGenerator.RollingWindow.fiveYear, false, false);
            Assert.AreEqual(TestDatabase.DSN, generator.DSN1);
            Assert.AreEqual(TestDatabase.DSN, generator.DSN2);
            Assert.AreEqual("input.csv", generator.InputFilename);
            Assert.AreEqual("report.csv", generator.ReportFilename);
            Assert.AreEqual("mesh.csv", generator.MeshFilename);
            Assert.AreEqual(ReportGenerator.RollingWindow.fiveYear, generator.Window);
            Assert.IsFalse(generator.FaultTolerance);
            Assert.IsFalse(generator.RemoveCommonPublications);

            generator.Warning += new EventHandler(generator_Warning);
            generator.Progress += new EventHandler(generator_Progress);

            generator.Generate();
            
            Assert.AreEqual(4, warnings.Count); // Four warnings that we're ignoring windows in the input file
            Assert.AreEqual(5, progresses.Count);

            CompareFiles("report.csv", rolling5Output);
            CheckMeshReportAgainstReport("mesh.csv", rolling5Output);
        }

        /// <summary>
        /// Standard report with fault tolerance
        /// </summary>
        [Test]
        public void FaultTolerance()
        {
            string[] partialOutput = new string[2];
            Array.Copy(output, partialOutput, 2);
            File.WriteAllLines("report.csv", partialOutput);
            File.WriteAllLines("mesh.csv", partialMeshReport);

            File.WriteAllLines("input.csv", input);
            ReportGenerator generator = new ReportGenerator(TestDatabase.DSN, TestDatabase.DSN,
                "input.csv", "report.csv", "mesh.csv", ReportGenerator.RollingWindow.none, true, false);
            Assert.AreEqual(TestDatabase.DSN, generator.DSN1);
            Assert.AreEqual(TestDatabase.DSN, generator.DSN2);
            Assert.AreEqual("input.csv", generator.InputFilename);
            Assert.AreEqual("report.csv", generator.ReportFilename);
            Assert.AreEqual("mesh.csv", generator.MeshFilename);
            Assert.AreEqual(ReportGenerator.RollingWindow.none, generator.Window);
            Assert.IsTrue(generator.FaultTolerance);
            Assert.IsFalse(generator.RemoveCommonPublications);

            generator.Warning += new EventHandler(generator_Warning);
            generator.Generate();
            Assert.AreEqual(1, warnings.Count); // Should generate one warning for fault tolerance
            CompareFiles("report.csv", output);
            CheckMeshReportAgainstReport("mesh.csv", output);

            Assert.IsTrue(File.Exists("report.csv.bak"));
            Assert.IsTrue(File.Exists("mesh.csv.bak"));
        }

        /// <summary>
        /// If there's no file to continue, fault tolerance should fail
        /// </summary>
        [Test]
        public void FaultToleranceWithoutFile()
        {
            try
            {
                File.WriteAllLines("input.csv", input);
                ReportGenerator generator = new ReportGenerator(TestDatabase.DSN, TestDatabase.DSN,
                    "input.csv", "report.csv", "mesh.csv", ReportGenerator.RollingWindow.none, true, false);
                Assert.Fail("new ReportGenerator() should have failed");
            }
            catch (Exception ex)
            {
                Assert.That(ex is OperationCanceledException);
                Assert.IsFalse(File.Exists("report.csv"));
                Assert.IsFalse(File.Exists("mesh.csv"));
                Assert.IsFalse(File.Exists("report.csv.bak"));
                Assert.IsFalse(File.Exists("mesh.csv.bak"));
            }

            // If there's a report file but no mesh report, it still shouldn't work
            try
            {
                File.WriteAllText("report.csv", "");
                File.WriteAllLines("input.csv", input);
                ReportGenerator generator = new ReportGenerator(TestDatabase.DSN, TestDatabase.DSN,
                    "input.csv", "report.csv", "mesh.csv", ReportGenerator.RollingWindow.none, true, false);
                Assert.Fail("new ReportGenerator() should have failed");
            }
            catch (Exception ex)
            {
                Assert.That(ex is OperationCanceledException);
                Assert.IsFalse(File.Exists("mesh.csv"));
                Assert.IsFalse(File.Exists("report.csv.bak"));
                Assert.IsFalse(File.Exists("mesh.csv.bak"));
            }

            // If there's a mesh report file but no normal report, it still shouldn't work
            try
            {
                File.Delete("report.csv");
                File.WriteAllLines("mesh.csv", partialMeshReport);
                File.WriteAllLines("input.csv", input);
                ReportGenerator generator = new ReportGenerator(TestDatabase.DSN, TestDatabase.DSN,
                    "input.csv", "report.csv", "mesh.csv", ReportGenerator.RollingWindow.none, true, false);
                Assert.Fail("new ReportGenerator() should have failed");
            }
            catch (Exception ex)
            {
                Assert.That(ex is OperationCanceledException);
                Assert.IsFalse(File.Exists("report.csv"));
                Assert.IsFalse(File.Exists("report.csv.bak"));
                Assert.IsFalse(File.Exists("mesh.csv.bak"));
            }
        }

        /// <summary>
        /// Fault tolerance should fail if the input file is invalid
        /// </summary>
        [Test]
        public void FaultToleranceWithInvalidFile()
        {
            try
            {
                string[] lines = new string[] {
                    "X0000001,2000-2005,X0000002,2000-2005,37,52,59,119,15,25",
                    "bad line",
                    "X0000003,2002,X0000004,1992-1997,30,39,46,64,4,4",
                    "another bad line",
                    "X0000005,,X0000006,,36,47,28,34,14,19",
                };
                File.WriteAllLines("report.csv", input);

                File.WriteAllLines("input.csv", input);
                ReportGenerator generator = new ReportGenerator(TestDatabase.DSN, TestDatabase.DSN,
                    "input.csv", "report.csv", "mesh.csv", ReportGenerator.RollingWindow.none, true, false);
                Assert.AreEqual(TestDatabase.DSN, generator.DSN1);
                Assert.AreEqual(TestDatabase.DSN, generator.DSN2);
                Assert.AreEqual("input.csv", generator.InputFilename);
                Assert.AreEqual("report.csv", generator.ReportFilename);
                Assert.AreEqual("mesh.csv", generator.MeshFilename);
                Assert.AreEqual(ReportGenerator.RollingWindow.none, generator.Window);
                Assert.IsTrue(generator.FaultTolerance);
                Assert.IsFalse(generator.RemoveCommonPublications);

            }
            catch (Exception ex)
            {
                Assert.That(ex is OperationCanceledException);
            }
        }

        /// <summary>
        /// Insert some common publications into the database, then re-run the
        /// standard generator test with removeCommonPublications turned on to make
        /// sure the output is the same as if the common publications aren't there.
        /// </summary>
        [Test]
        public void RemoveCommonPublications()
        {
            Database db1 = new Database(TestDatabase.DSN);
            db1.ExecuteNonQuery(
                @"INSERT INTO PeoplePublications (setnb, pmid, authorposition, positiontype)
                       VALUES 
                              ('X0000001', 17479405, 3, 2),
                              ('X0000002', 17479405, 4, 2),
                              ('X0000003', 17479405, 5, 2),
                              ('X0000004', 17479405, 6, 2),
                              ('X0000005', 12520393, 3, 2),
                              ('X0000006', 12520393, 4, 2),
                              ('X0000005', 8761517, 3, 2),
                              ('X0000006', 8761517, 4, 2),
                              ('X0000005', 1333868, 7, 2),
                              ('X0000006', 1333868, 4, 2),
                              ('X0000005', 7950878, 7, 2),
                              ('X0000006', 7950878, 4, 2)
                ");

            File.WriteAllLines("input.csv", input);
            ReportGenerator generator = new ReportGenerator(TestDatabase.DSN, TestDatabase.DSN,
                "input.csv", "report.csv", "mesh.csv", ReportGenerator.RollingWindow.none, false, true);
            Assert.AreEqual(TestDatabase.DSN, generator.DSN1);
            Assert.AreEqual(TestDatabase.DSN, generator.DSN2);
            Assert.AreEqual("input.csv", generator.InputFilename);
            Assert.AreEqual("report.csv", generator.ReportFilename);
            Assert.AreEqual("mesh.csv", generator.MeshFilename);
            Assert.AreEqual(ReportGenerator.RollingWindow.none, generator.Window);
            Assert.IsFalse(generator.FaultTolerance);
            Assert.IsTrue(generator.RemoveCommonPublications);

            generator.Warning += new EventHandler(generator_Warning);
            generator.Progress += new EventHandler(generator_Progress);

            generator.Generate();

            Assert.AreEqual(0, warnings.Count);
            Assert.AreEqual(9, progresses.Count);

            CompareFiles("report.csv", output);
            CheckMeshReportAgainstReport("mesh.csv", output);
        }


        /// <summary>
        /// Compare a file against a set of expected lines
        /// </summary>
        /// <param name="filename">File to check</param>
        /// <param name="expected">Lines to check the file against</param>
        private void CompareFiles(string filename, string[] expected)
        {
            Assert.IsTrue(File.Exists(filename));
            string[] actual = File.ReadAllLines(filename);
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
                Assert.AreEqual(expected[i], actual[i], "Line " + i + " of " + filename + " doesn't match");
        }

        /// <summary>
        /// Check a MeSH report against an expected normal report
        /// </summary>
        /// <param name="filename">MeSH report to check</param>
        /// <param name="report">Lines from the expected report</param>
        private void CheckMeshReportAgainstReport(string filename, string[] report)
        {
            // Check the header row, then copy the report to a new array without the header row
            Assert.AreEqual(Files.ReportHeader, report[0]);
            string[] reportWithoutHeader = new string[report.Length - 1];
            Array.Copy(report, 1, reportWithoutHeader, 0, report.Length - 1);

            Assert.IsTrue(File.Exists(filename));
            string[] lines = File.ReadAllLines(filename);

            // Check the header row, then copy the report to a new array without the header row
            Assert.AreEqual(Files.MeSHHeader, lines[0]);
            string[] linesWithoutHeader = new string[lines.Length - 1];
            Array.Copy(lines, 1, linesWithoutHeader, 0, lines.Length - 1);

            
            int row = 0;
            foreach (string line in reportWithoutHeader)
            {
                string[] columns = line.Split(new char[] { ',' });
                Assert.AreEqual(columns.Length, 10);
                row = CheckMeshReportLines(linesWithoutHeader, columns[0], columns[1], row, int.Parse(columns[4]));
                row = CheckMeshReportLines(linesWithoutHeader, columns[2], columns[3], row, int.Parse(columns[6]));
            }
            Assert.AreEqual(linesWithoutHeader.Length, row);
        }


        /// <summary>
        /// Check a set of rows from the MeSH report to make sure they've got the right scientist and window
        /// </summary>
        /// <returns>The row number of the next row</returns>
        private int CheckMeshReportLines(string[] linesFromMeshReport, string scientist, string window,
            int startingRow, int count)
        {
            if (count == 0)
                return startingRow;

            Assert.Less(startingRow, linesFromMeshReport.Length, String.Format("Scientist: {0}, window: {1}, startingRow: {2}, count: {3}", scientist, window, startingRow, count));
            Assert.LessOrEqual(startingRow + count, linesFromMeshReport.Length, String.Format("Scientist: {0}, window: {1}, startingRow: {2}, count: {3}", scientist, window, startingRow, count));

            int row;
            for (row = startingRow; row < startingRow + count; row++)
            {
                string[] columns = linesFromMeshReport[row].Split(new char[] { ',' });
                Assert.GreaterOrEqual(columns.Length, 4);
                Assert.AreEqual(scientist, columns[0], String.Format("Scientist: {0}, window: {1}, row: {2} {3}", scientist, window, row, linesFromMeshReport[row]));
                Assert.AreEqual(window, columns[1], String.Format("Scientist: {0}, window: {1}, row: {2} {3}", scientist, window, row, linesFromMeshReport[row]));
                int result;
                Assert.That(int.TryParse(columns[columns.Length - 1], out result), String.Format("Scientist: {0}, window: {1}, row: {2}", scientist, window, row));
            }

            return row;
        }
    }
}
