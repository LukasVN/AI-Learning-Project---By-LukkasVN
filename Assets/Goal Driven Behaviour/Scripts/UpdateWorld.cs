using System.Collections;
using System.Collections.Generic;
using GoalDrivenBehaviour;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateWorld : MonoBehaviour
{
    private Text states;

    private void Start() {
        states = GetComponent<Text>();
    }
    void LateUpdate(){
        Dictionary<string, int> worldstates = GWorld.Instance.GetWorld().GetStates();
        states.text = "";
        foreach (KeyValuePair<string, int> s in worldstates){
            states.text += s.Key + ", " + s.Value + "\n";
            
        }
    }
}
