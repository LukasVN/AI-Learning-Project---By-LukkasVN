using System.Collections;
using System.Collections.Generic;
using GoalDrivenBehaviour;
using UnityEngine;

namespace GoalDrivenBehaviour{
    public class Patient : GAgent
    {
        protected override void Start()
        {
            base.Start();
            SubGoal s1 = new SubGoal("isWaiting", 1,true);
            goals.Add(s1,3);

            SubGoal s2 = new SubGoal("isTreated", 1,true);
            goals.Add(s2,5);
        }

    }
}