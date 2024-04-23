using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTrees
{
    public class Node{
        public enum Status {SUCCESS, RUNNING, FAILURE};
        public Status status;
        public List<Node> children = new List<Node>();
        public int childIndex = 0;
        public string name;

        public Node(){ 
            
        }
        public Node(string n){
            name = n;
        }

        public virtual Status Process(){
            return children[childIndex].Process();
        }

        public void AddChild(Node n){
            children.Add(n);
        }

    }
}

