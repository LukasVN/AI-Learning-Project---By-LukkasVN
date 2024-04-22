using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoalDrivenBehaviour{

    public class GetRest : GAction
    {
        public override bool PrePerform(){
            return true;
        }
        public override bool PostPerform(){
            beliefs.RemoveState("exhausted");
            return true;
        }        
        
    }
}
