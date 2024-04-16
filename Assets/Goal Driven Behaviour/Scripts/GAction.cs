using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GoalDrivenBehaviour{

    public abstract class GAction : MonoBehaviour
    {
        public string actionName = "Action";
        public float actionCost = 1.0f;
        public GameObject target;
        public GameObject targetTag;
        public float duration = 0;
        public WorldState[] ws_preConditions;
        public WorldState[] ws_afterEffects;
        public NavMeshAgent agent;

        public Dictionary<string, int> preconditions;
        public Dictionary<string, int> aftereffects;

        public WorldState agentBeliefs;

        public bool running = false;

        public GAction(){
            preconditions = new Dictionary<string, int>();
            aftereffects = new Dictionary<string, int>();
        }

        private void Awake() {
            agent = gameObject.GetComponent<NavMeshAgent>();

            if(preconditions != null){
                foreach (WorldState ws in ws_preConditions){
                    preconditions.Add(ws.key, ws.value);
                }
            }
            if(aftereffects != null){
                foreach (WorldState ws in ws_afterEffects){
                    aftereffects.Add(ws.key, ws.value);
                }
            }
        }

        public bool IsAchivable(){
            return true;
        }

        public bool IsAchivableGiven(Dictionary<string, int> conditions){
            foreach (KeyValuePair<string, int> pair in preconditions){
                if(!conditions.ContainsKey(pair.Key)){
                    return false;
                }
                return true;
            }

            return true;
        }

        public abstract bool PrePerform();
        public abstract bool PostPerform();
    }
}
