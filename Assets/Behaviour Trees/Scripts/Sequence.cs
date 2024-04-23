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
            Debug.Log("[Sequence] Running "+name+" Process");
            Status childstatus = children[childIndex].Process();
            if(childstatus == Status.RUNNING){
                return Status.RUNNING;
            }
            if(childstatus == Status.FAILURE){
                return childstatus;
            }

            childIndex++;

            if(childIndex >= children.Count){
                childIndex = 0;
                return Status.SUCCESS;
            }
            
            return Status.RUNNING;
        }
    }
}
