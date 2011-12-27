using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ScientificDistance.Unit_Tests
{
    [TestFixture]
    class MeSHStrippingTest
    {

        /// <summary>
        /// Each test starts with these headings, strips each of them, and stores the stripped headings
        /// in another list to compare against.
            /// </summary>
        List<string> headings = new List<string>() {       
            "*Mother-Child Relations",
            "*Object Attachment",
            "Adult",
            "Child",
            "Child, Preschool",
            "Cocaine-Related Disorders/*psychology",
            "Defense Mechanisms",
            "Female",
            "Follow-Up Studies",
            "Humans",
            "Infant",
            "Internal-External Control",
            "Male",
            "Maternal Behavior",
            "Parenting/*psychology",
            "Personality Assessment",
            "Pregnancy",
            "Pregnancy Complications/*psychology",
            "*Adaptation, Psychological",
            "*Health Knowledge, Attitudes, Practice",
            "*Intensive Care Units, Neonatal",
            "*Parent-Child Relations",
            "*Parents/education/psychology",
            "Infant, Newborn",
            "Nursing Methodology Research",
            "Stress, Psychological/psychology",
            "*Arousal",
            "*Infant Behavior",
            "*Neurologic Examination",
            "Asian Continental Ancestry Group/*psychology",
            "Cross-Cultural Comparison",
            "Infant, Newborn/*psychology",
            "Irritable Mood",
            "Japan",
            "Mothers/psychology",
            "Object Attachment",
            "Parenting/psychology",
            "Prognosis",
            "Skin Pigmentation",
            "Startle Reaction",
            "Stress, Psychological/complications",
        };


        /// <summary>
        /// Reset Publications.MeshStrippingOption on teardown to avoid interfering with other unit tests
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            Publications.MeshStrippingOption = MeshStrippingOption.StrippedMeshTermsAllTerms;
        }


        /// <summary>
        /// Strip all of the headings
        /// </summary>
        /// <param name="meshStrippingOption">MeSH stripping option to use</param>
        /// <returns>Collection of stripped headings</returns>
        private List<string> StripHeadings(MeshStrippingOption meshStrippingOption)
        {
            // Set the MeSH stripping option
            Publications.MeshStrippingOption = meshStrippingOption;

            // Strip the terms
            List<string> strippedHeadings = new List<string>(headings);
            for (int i = 0; i < strippedHeadings.Count; i++)
                strippedHeadings[i] = Publications.StripHeading(strippedHeadings[i]);
            return strippedHeadings;
        }


        [Test]
        public void TestStrippedMeshTermsAllTerms()
        {
            List<string> strippedHeadings = StripHeadings(MeshStrippingOption.StrippedMeshTermsAllTerms);

            List<string> compare = new List<string>() {       
                "Mother-Child Relations",
                "Object Attachment",
                "Adult",
                "Child",
                "Child, Preschool",
                "Cocaine-Related Disorders",
                "Defense Mechanisms",
                "Female",
                "Follow-Up Studies",
                "Humans",
                "Infant",
                "Internal-External Control",
                "Male",
                "Maternal Behavior",
                "Parenting",
                "Personality Assessment",
                "Pregnancy",
                "Pregnancy Complications",
                "Adaptation, Psychological",
                "Health Knowledge, Attitudes, Practice",
                "Intensive Care Units, Neonatal",
                "Parent-Child Relations",
                "Parents",
                "Infant, Newborn",
                "Nursing Methodology Research",
                "Stress, Psychological",
                "Arousal",
                "Infant Behavior",
                "Neurologic Examination",
                "Asian Continental Ancestry Group",
                "Cross-Cultural Comparison",
                "Infant, Newborn",
                "Irritable Mood",
                "Japan",
                "Mothers",
                "Object Attachment",
                "Parenting",
                "Prognosis",
                "Skin Pigmentation",
                "Startle Reaction",
                "Stress, Psychological", 
            };

            for (int i = 0; i < headings.Count; i++)
            {
                Assert.AreEqual(strippedHeadings[i], compare[i], String.Format("Stripped heading {0} compare failed", i));
            }
        }

        [Test]
        public void TestUnstrippedMeshTermsAllTerms()
        {
            List<string> strippedHeadings = StripHeadings(MeshStrippingOption.UnstrippedMeshTermsAllTerms);

            List<string> compare = new List<string>() {
                "Mother-Child Relations",
                "Object Attachment",
                "Adult",
                "Child",
                "Child, Preschool",
                "Cocaine-Related Disorders/psychology",
                "Defense Mechanisms",
                "Female",
                "Follow-Up Studies",
                "Humans",
                "Infant",
                "Internal-External Control",
                "Male",
                "Maternal Behavior",
                "Parenting/psychology",
                "Personality Assessment",
                "Pregnancy",
                "Pregnancy Complications/psychology",
                "Adaptation, Psychological",
                "Health Knowledge, Attitudes, Practice",
                "Intensive Care Units, Neonatal",
                "Parent-Child Relations",
                "Parents/education/psychology",
                "Infant, Newborn",
                "Nursing Methodology Research",
                "Stress, Psychological/psychology",
                "Arousal",
                "Infant Behavior",
                "Neurologic Examination",
                "Asian Continental Ancestry Group/psychology",
                "Cross-Cultural Comparison",
                "Infant, Newborn/psychology",
                "Irritable Mood",
                "Japan",
                "Mothers/psychology",
                "Object Attachment",
                "Parenting/psychology",
                "Prognosis",
                "Skin Pigmentation",
                "Startle Reaction",
                "Stress, Psychological/complications",
            };

            for (int i = 0; i < headings.Count; i++)
            {
                Assert.AreEqual(strippedHeadings[i], compare[i], String.Format("Stripped heading {0} compare failed", i));
            }
        }

        [Test]
        public void TestStrippedMeshTermsMainTermsOnly()
        {
            List<string> strippedHeadings = StripHeadings(MeshStrippingOption.StrippedMeshTermsMainTermsOnly);

            List<string> compare = new List<string>()
            {
                "Mother-Child Relations",
                "Object Attachment",
                null,
                null,
                null,
                "Cocaine-Related Disorders",
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                "Parenting",
                null,
                null,
                "Pregnancy Complications",
                "Adaptation, Psychological",
                "Health Knowledge, Attitudes, Practice",
                "Intensive Care Units, Neonatal",
                "Parent-Child Relations",
                "Parents",
                null,
                null,
                null,
                "Arousal",
                "Infant Behavior",
                "Neurologic Examination",
                "Asian Continental Ancestry Group",
                null,
                "Infant, Newborn",
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,            
            };

            for (int i = 0; i < headings.Count; i++)
            {
                Assert.AreEqual(strippedHeadings[i], compare[i], String.Format("Stripped heading {0} compare failed", i));
            }
        }

        [Test]
        public void TestUnstrippedMeshTermsMainTermsOnly()
        {
            List<string> strippedHeadings = StripHeadings(MeshStrippingOption.UnstrippedMeshTermsMainTermsOnly);

            List<string> compare = new List<string>()
            {
                "Mother-Child Relations",
                "Object Attachment",
                null,
                null,
                null,
                "Cocaine-Related Disorders/psychology",
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                "Parenting/psychology",
                null,
                null,
                "Pregnancy Complications/psychology",
                "Adaptation, Psychological",
                "Health Knowledge, Attitudes, Practice",
                "Intensive Care Units, Neonatal",
                "Parent-Child Relations",
                "Parents/education/psychology",
                null,
                null,
                null,
                "Arousal",
                "Infant Behavior",
                "Neurologic Examination",
                "Asian Continental Ancestry Group/psychology",
                null,
                "Infant, Newborn/psychology",
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
            };

            for (int i = 0; i < headings.Count; i++)
            {
                Assert.AreEqual(strippedHeadings[i], compare[i], String.Format("Stripped heading {0} compare failed", i));
            }
        }
    }
}
