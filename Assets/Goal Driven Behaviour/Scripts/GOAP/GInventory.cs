using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoalDrivenBehaviour{
    public class GInventory
    {
        List<GameObject> items = new List<GameObject>();

        public void AddItem(GameObject item){
            items.Add(item);
        }

        public GameObject FindItemWithTag(string tag){
            foreach(GameObject item in items){
                if(item.tag == tag){
                    return item;
                }
            }
            return null;
        }

        public void RemoveItem(GameObject item){
            int index = -1;
            foreach (GameObject go in items){
                index++;
                if(go == item){
                    break;
                }
            }
            if(index >= -1){
                items.RemoveAt(index);
            }
        }
    }
}