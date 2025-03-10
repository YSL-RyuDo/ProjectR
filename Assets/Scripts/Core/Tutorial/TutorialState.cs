using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Tutorial
{
    public interface TutorialState
    {
        void EnterState(TutorialManager tutorial);
        void UpdateState(TutorialManager tutorial);
    }

}


