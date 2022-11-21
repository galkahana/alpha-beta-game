using System;
using System.Collections.Generic;
using System.Text;

namespace MatixGameForm
{
    struct Pair<T,U> : IEquatable< Pair<T,U> >
    {
        public T first;
        public U second;

        public Pair(T inFirst,U inSecond)
        {
            first = inFirst;
            second = inSecond;
        }


        #region IEquatable<Pair<T,U>> Members

        public bool Equals(Pair<T, U> other)
        {
            return (first.Equals(other.first)) && (second.Equals(other.second));
        }

        #endregion
    }
}
