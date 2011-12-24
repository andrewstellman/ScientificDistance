using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScientificDistance
{
    /// <summary>
    /// MeSH heading stripping options for limiting the calculation distance
    /// </summary>
    public enum MeshStrippingOption
    {

        /// <summary>
        /// Stripped mesh terms, all terms (default)
        /// </summary>
        StrippedMeshTermsAllTerms,

        /// <summary>
        /// Unstripped mesh terms, all terms
        /// </summary>
        UnstrippedMeshAllTerms,

        /// <summary>
        /// Stripped mesh terms, main terms only
        /// </summary>
        StrippedMeshTermsMainTermsOnly,

        /// <summary>
        /// Unstripped mesh terms, main terms only
        /// </summary>
        UnstrippedMeshTermsMainTermsOnly,
    }

    public static class MeshStrippingOptionUtilities
    {
        public static string PrettyPrint(this MeshStrippingOption value)
        {
            switch (value)
            {
                case MeshStrippingOption.StrippedMeshTermsAllTerms:
                default:
                    return "Stripped MeSH Terms (all terms)";

                case MeshStrippingOption.UnstrippedMeshAllTerms:
                    return "Unstripped MeSH Terms (all terms)";

                case MeshStrippingOption.StrippedMeshTermsMainTermsOnly:
                    return "Stripped MeSH Terms (main terms only)";

                case MeshStrippingOption.UnstrippedMeshTermsMainTermsOnly:
                    return "Unstripped MeSH Terms (main terms only)";
            }
        }

        public static IEnumerable<string> ListOptions()
        {
            Array options = Enum.GetValues(typeof(MeshStrippingOption));
            foreach (MeshStrippingOption option in options)
            {
                yield return option.PrettyPrint();
            }
        }
    }
}
