using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScientificDistance
{
    /// <summary>
    /// An InputRow object represents one row from the input file
    /// </summary>
    public class InputRow : IComparable
    {
        public string Scientist1 { get; set; }
        public string Window1 { get; set; }
        public string Scientist2 { get; set; }
        public string Window2 { get; set; }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (obj is InputRow)
            {
                InputRow compare = obj as InputRow;
                if (compare.Scientist1.CompareTo(Scientist1) != 0)
                    return compare.Scientist1.CompareTo(Scientist1);
                else if (compare.Window1.CompareTo(Window1) != 0)
                    return compare.Window1.CompareTo(Window1);
                if (compare.Scientist2.CompareTo(Scientist2) != 0)
                    return compare.Scientist2.CompareTo(Scientist2);
                else if (compare.Window2.CompareTo(Window2) != 0)
                    return compare.Window2.CompareTo(Window2);
                else
                    return 0;
            }
            else
                return 0;
        }

        #endregion

        public override string ToString()
        {
            return String.Format("{0},{1},{2},{3}", Scientist1, Window1, Scientist2, Window2);
        }
    }
}
