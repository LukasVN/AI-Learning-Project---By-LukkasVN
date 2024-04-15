using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    //GPlanner planner;
    Queue<GAction> actionsQueue = new Queue<GAction>();
    public GAction currentAction;
    SubGoal currentGoal;

    private void Start() {
        GAction[] acts = GetComponents<GAction>();
        foreach (GAction act in acts) {
            actions.Add(act);
        }
    }

    private void LateUpdate() {
        
    }
}
