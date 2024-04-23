using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTrees
{
    public class Selector : Node
    {
        public Selector(string n){
            name = n;
        }

        public override Status Process(){
            Debug.Log("[Selector] Running "+name+" Process");
            Status childstatus = children[childIndex].Process();
            if(childstatus == Status.RUNNING){
                return Status.RUNNING;
            }
            if(childstatus == Status.SUCCESS){
                childIndex = 0;
                return Status.SUCCESS;
            }

            childIndex++;

            if(childIndex >= children.Count){
                childIndex = 0;
                return Status.FAILURE;
            }
            
            return Status.RUNNING;
        }
    }
}
