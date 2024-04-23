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
        public GameObject diamondHolder;
        public GameObject van;
        public GameObject frontDoor;
        public GameObject backDoor;
        private NavMeshAgent agent;
        public enum ActionState {IDLE, WORKING};
        private ActionState state = ActionState.IDLE;

        Node.Status treeStatus = Node.Status.RUNNING;

        [Range(0,1000)]
        public int money = 800;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            tree = new BehaviourTree();
            Sequence steal = new Sequence("Steal something");
            Leaf hasEnoughMoney = new Leaf("Has Enough Money", CheckMoney);
            Leaf goToFrontDoor = new Leaf("Go To Front Door", GoToFrontDoor);
            Leaf goToBackDoor = new Leaf("Go To Back Door", GoToBackDoor);
            Leaf goToDiamond = new Leaf("Go To Diamond", GoToDiamond);
            Leaf goToVan = new Leaf("Go To Van", GoToVan);
            Selector openDoor = new Selector("Open Door");


            openDoor.AddChild(goToFrontDoor);
            openDoor.AddChild(goToBackDoor);

            steal.AddChild(hasEnoughMoney);
            steal.AddChild(openDoor);
            steal.AddChild(goToDiamond);
            // steal.AddChild(goToBackDoor);
            steal.AddChild(goToVan);
            tree.AddChild(steal);

            tree.PrintTree();


        }

        public Node.Status CheckMoney(){
            if(money >= 500){
                return Node.Status.FAILURE;
            }
            return Node.Status.SUCCESS;
        }

        public Node.Status GoToDiamond(){
            return StealDiamond(diamondHolder);
        }
        
        public Node.Status GoToFrontDoor(){
            return GoToDoor(frontDoor);
        }

        public Node.Status GoToBackDoor(){
            return GoToDoor(backDoor);
        }


        public Node.Status GoToVan(){
            return FinishedStealing(van);
        }

        public Node.Status FinishedStealing(GameObject van){
            Node.Status s = GoToLocation(van.transform.position);
            if(s == Node.Status.SUCCESS){
                transform.GetChild(1).gameObject.SetActive(false);
                money += 300;
            }
            return s;
        }

        public Node.Status EscapeInVan(GameObject van){
            Node.Status s = GoToLocation(van.transform.position);
            if(s == Node.Status.SUCCESS){
                van.SetActive(false);
                gameObject.SetActive(false);
            }
            return s;
        }

        public Node.Status StealDiamond(GameObject diamondHolder){
            Node.Status s = GoToLocation(diamondHolder.transform.position);
            if(s == Node.Status.SUCCESS){
                diamondHolder.transform.GetChild(0).parent = gameObject.transform;
                return Node.Status.SUCCESS;
            }
            return s;
        }

        public Node.Status GoToDoor(GameObject door){
            Node.Status s = GoToLocation(door.transform.position);
            if(s == Node.Status.SUCCESS){
                if(!door.GetComponent<Lock>().isLocked){
                    door.SetActive(false);
                    return Node.Status.SUCCESS;
                }
                return Node.Status.FAILURE;
            }
            else{
                return s;
            }
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
            if(treeStatus != Node.Status.SUCCESS){
                treeStatus = tree.Process();
            }
        }
    }
}