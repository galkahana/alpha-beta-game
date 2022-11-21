using System;
using System.Collections.Generic;

namespace XBLIP.AplhaBetaGame
{
    public interface IAssignableObject<TStage>
    {
        void Assign(TStage inStage);
    }
}
