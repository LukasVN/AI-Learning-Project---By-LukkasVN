using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GoalDrivenBehaviour{
    public class SubGoal{
        public Dictionary<string, int> subGoals;
        public bool remove;

        public SubGoal(string s, int i, bool r){
            subGoals = new Dictionary<string, int>();
            subGoals.Add(s,i);
            remove = r;
        }

    }
    public class GAgent : MonoBehaviour
    {
        public List<GAction> actions = new List<GAction>();
        public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();

        GPlanner planner;
        Queue<GAction> actionsQueue = new Queue<GAction>();
        public GAction currentAction;
        SubGoal currentGoal;

        protected virtual void Start() {
            GAction[] acts = GetComponents<GAction>();
            foreach (GAction act in acts) {
                actions.Add(act);
            }
        }

        bool invoked = false;
        void CompleteAction(){
            currentAction.running = false;
            currentAction.PostPerform();
            invoked = false;
        }

        private void LateUpdate() {

            if(currentAction != null && currentAction.running){
                if(currentAction.agent.hasPath && currentAction.agent.remainingDistance <1f){
                    if(!invoked){
                        Invoke("CompleteAction", currentAction.duration);
                        invoked = true;
                    }
                }
                return;
            }

            if(planner == null || actionsQueue == null){
                planner = new GPlanner();

                var sortedGoals = from entry in goals orderby entry.Value descending select entry;
                
                foreach (KeyValuePair<SubGoal,int> sg in sortedGoals){
                    actionsQueue = planner.Plan(actions, sg.Key.subGoals, null); //error
                    if(actionsQueue != null){
                        currentGoal = sg.Key;
                        break;
                    }
                }
            }

            if(actionsQueue != null && actionsQueue.Count == 0){
                if(currentGoal.remove){
                    goals.Remove(currentGoal);
                }
                planner = null;
            }

            if(actionsQueue != null && actionsQueue.Count > 0){
                currentAction = actionsQueue.Dequeue();
                if(currentAction.PrePerform()){
                    if(currentAction.target == null && currentAction.targetTag != ""){
                        currentAction.target = GameObject.FindWithTag(currentAction.targetTag);
                    }

                    if(currentAction.targetTag != null){
                        currentAction.running = true;
                        currentAction.agent.SetDestination(currentAction.target.transform.position);
                    }
                }
                else{
                    actionsQueue = null;
                }
            }


        }
    }
}
