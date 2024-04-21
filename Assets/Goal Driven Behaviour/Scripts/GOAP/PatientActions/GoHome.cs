using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoalDrivenBehaviour{

    public class GoHome : GAction
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
