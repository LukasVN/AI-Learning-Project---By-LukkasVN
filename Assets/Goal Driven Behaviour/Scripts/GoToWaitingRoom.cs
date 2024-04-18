using System.Collections;
using System.Collections.Generic;
using GoalDrivenBehaviour;
using UnityEngine;

namespace GoalDrivenBehaviour{

    public class GoToWaitingRoom : GAction
    {
        public override bool PostPerform()
        {
            return true;
        }

        public override bool PrePerform()
        {
            return true;
        }

        
    }
}
