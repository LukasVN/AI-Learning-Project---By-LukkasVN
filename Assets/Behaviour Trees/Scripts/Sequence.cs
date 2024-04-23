using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviourTrees
{
    public class Sequence : Node
    {
        public Sequence(string n){
            name = n;
        }

        public override Status Process(){
            Status childstatus = children[childIndex].Process();
            
            return Status.RUNNING;
        }
    }
}
