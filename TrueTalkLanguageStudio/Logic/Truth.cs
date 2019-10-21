
namespace TrueTalk.Logic
{
    using System;

    public class Truth
    {
        [Flags]
        public enum State
        {
            Unknown = -1,
            False   =  0,
            True    =  1,
        }

        public static readonly Atom Unknown = new Atom( State.Unknown );
        public static readonly Atom True    = new Atom( State.True    );
        public static readonly Atom False   = new Atom( State.False   );

        public static bool Negate( ref Truth.State truth )
        {
            if(truth != State.Unknown)
            {
                truth = truth == State.True ? State.False : State.True;

                return true;
            }

            return false;
        }
    }
}
