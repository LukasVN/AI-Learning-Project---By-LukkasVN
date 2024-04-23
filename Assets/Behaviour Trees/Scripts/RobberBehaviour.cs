using System.Collections;
using System.Collections.Generic;
using BehaviourTrees;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviourTrees
{
    public class RobberBehaviour : MonoBehaviour
    {
        private BehaviourTree tree;
        public GameObject diamond;
        public GameObject van;
        public GameObject frontDoor;
        public GameObject backDoor;
        private NavMeshAgent agent;
        public enum ActionState {IDLE, WORKING};
        private ActionState state = ActionState.IDLE;

        Node.Status treeStatus = Node.Status.RUNNING;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            tree = new BehaviourTree();
            Sequence steal = new Sequence("Steal something");
            Leaf goToFrontDoor = new Leaf("Go To Front Door", GoToFrontDoor);
            Leaf goToBackDoor = new Leaf("Go To Back Door", GoToBackDoor);
            Leaf goToDiamond = new Leaf("Go To Diamond", GoToDiamond);
            Leaf goToVan = new Leaf("Go To Van", GoToVan);
            Selector openDoor = new Selector("Open Door");

            openDoor.AddChild(goToFrontDoor);
            openDoor.AddChild(goToBackDoor);

            steal.AddChild(openDoor);
            steal.AddChild(goToDiamond);
            // steal.AddChild(goToBackDoor);
            steal.AddChild(goToVan);
            tree.AddChild(steal);

            tree.PrintTree();


        }

        public Node.Status GoToDiamond(){
            return GoToLocation(diamond.transform.position);
        }
        
        public Node.Status GoToFrontDoor(){
            return GoToLocation(frontDoor.transform.position);
        }

        public Node.Status GoToBackDoor(){
            return GoToLocation(backDoor.transform.position);
        }


        public Node.Status GoToVan(){
            return GoToLocation(van.transform.position);
        }

        public Node.Status GoToLocation(Vector3 destination){
            float distanceToTarget = Vector3.Distance(destination, transform.position);
            if(state == ActionState.IDLE){
                agent.SetDestination(destination);
                state = ActionState.WORKING;
            }
            else if(Vector3.Distance(agent.pathEndPosition,destination) >= 2){
                state = ActionState.IDLE;
                return Node.Status.FAILURE;
            }
            else if(distanceToTarget < 2){
                state = ActionState.IDLE;
                return Node.Status.SUCCESS;
            }
            return Node.Status.RUNNING;
            
        }

        void FixedUpdate()
        {
            if(treeStatus == Node.Status.RUNNING){
                treeStatus = tree.Process();
            }
        }
    }
}