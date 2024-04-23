using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTrees
{
    public class Leaf : Node
    {
        public delegate Status Tick();
        public Tick ProcessMethod;
        
        public Leaf() {

        }

        public Leaf(string n, Tick pm) {
            name = n;
            ProcessMethod = pm;
        }

        public override Status Process()
        {
            Debug.Log("[Leaf] Running "+name+" Process");
            if(ProcessMethod != null){
                return ProcessMethod();
            }
            return Status.FAILURE;
        }
    }
}
