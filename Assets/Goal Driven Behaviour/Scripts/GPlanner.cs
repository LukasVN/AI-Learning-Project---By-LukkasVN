using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GoalDrivenBehaviour{
    public class Node {
        public Node parent;
        public float cost;
        public Dictionary<string, int> state;
        public GAction action;
        public Node(Node parent, float cost, Dictionary<string, int> state, GAction action){
            this.parent = parent;
            this.cost = cost;
            this.state = new Dictionary<string, int>(state); // Copy state instead of creating new empty dictionary
            this.action = action;
        }
    }

public class GPlanner {
    public Queue<GAction> Plan(List<GAction> actions, Dictionary<string,int> goal, WorldStates states){
        List<GAction> usableActions = actions.Where(a => a.IsAchivable()).ToList(); // Use LINQ for filtering

        List<Node> leaves = new List<Node>();
        Node start = new Node(null, 0, states.GetStates(), null); // Use provided states

        if(!BuildGraph(start, leaves, usableActions, goal)){
            Debug.Log("NO PLAN");
            return null;
        }

        Node cheapest = leaves.OrderBy(leaf => leaf.cost).FirstOrDefault(); // Use LINQ for finding cheapest leaf

        List<GAction> result = new List<GAction>();
        Node n = cheapest;
        while(n != null){
            if(n.action != null){
                result.Insert(0, n.action);
            }
            n = n.parent;
        }

        Queue<GAction> queue = new Queue<GAction>(result);
        
        Debug.Log("The Plan is: ");
        foreach (GAction a in queue){
            Debug.Log("Q: " + a.actionName);
        }

        return queue;
    }

    private bool BuildGraph(Node parent, List<Node> leaves, List<GAction> usableActions, Dictionary<string, int> goal){
        foreach (GAction action in usableActions){
            if(action.IsAchivableGiven(parent.state)){
                Dictionary<string, int> currentState = new Dictionary<string, int>(parent.state);
                foreach (KeyValuePair<string, int> effect in action.aftereffects){
                    currentState[effect.Key] = effect.Value; // Update state directly
                }

                Node node = new Node(parent, parent.cost + action.actionCost, currentState, action);

                if(GoalAchieved(goal, currentState)){
                    leaves.Add(node);
                    return true; // No need to continue searching if goal is achieved
                }
                else{
                    List<GAction> subset = ActionSubset(usableActions, action);
                    if(BuildGraph(node, leaves, subset, goal)){
                        return true; // Goal achieved, no need to continue searching
                    }
                }
            }
        }
        return false; // Goal not achieved
    }

    private bool GoalAchieved(Dictionary<string,int> goal, Dictionary<string, int> state){
        return goal.Keys.All(key => state.ContainsKey(key) && state[key] == goal[key]); // Use LINQ for goal achievement check
    }

    private List<GAction> ActionSubset(List<GAction> actions, GAction toRemove){
        return actions.Where(action => action != toRemove).ToList();
    }
}
}
