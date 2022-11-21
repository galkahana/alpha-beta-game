using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MatixGameForm
{
    [DataContract]
    public class MatixMove : ICloneable
    {
        public MatixMove()
        {
            mSelectionIndex = 0;
        }

        public MatixMove(int inSelectionIndex)
        {
            mSelectionIndex = inSelectionIndex;
        }

        private int mSelectionIndex;

        [DataMember]
        public int SelectionIndex
        {
            get
            {
                return mSelectionIndex;
            }

            set
            {
                mSelectionIndex = value;
            }
        }

        #region ICloneable Members

        public object Clone()
        {
            return new MatixMove(mSelectionIndex);
        }

        #endregion

        public override bool Equals(Object inObject)
        {
            if (inObject == null || GetType() != inObject.GetType()) return false;
            MatixMove matixMove = (MatixMove)inObject;
            return matixMove.SelectionIndex == SelectionIndex;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


    }
}
