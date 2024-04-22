using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoalDrivenBehaviour{
    public class Nurse : GAgent
    {
        protected override void Start()
        {
            base.Start();
            SubGoal s1 = new SubGoal("treatPatient", 1,false);
            goals.Add(s1,3);

            SubGoal s2 = new SubGoal("Rested", 1, false);
            goals.Add(s2,5);

            Invoke("GetTired", Random.Range(10,20));
        }

        private void GetTired(){
            beliefs.ModifyState("exhausted",0);
            Invoke("GetTired", Random.Range(10,20));
        }

    }
}
