using System.Collections;
using System.Collections.Generic;
using GoalDrivenBehaviour;
using UnityEngine;

namespace GoalDrivenBehaviour{

    public class GoToWaitingRoom : GAction
    {
        public override bool PrePerform()
        {
            return true;
        }
        public override bool PostPerform()
        {
            GWorld.Instance.GetWorld().ModifyState("atHospital",1);
            GWorld.Instance.AddPatient(gameObject);
            beliefs.ModifyState("isWaiting", 1);
            return true;
        }
        
    }
}
