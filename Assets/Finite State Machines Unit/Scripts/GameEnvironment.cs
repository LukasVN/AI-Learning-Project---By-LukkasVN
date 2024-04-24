using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace FiniteStateMachines{
    public sealed class GameEnvironment
    {
        private static GameEnvironment instance;
        private List<GameObject> checkpoints = new List<GameObject>();
        public List<GameObject> Checkpoints { get { return checkpoints; } }
        public Transform safeLocation;

        public static GameEnvironment Singleton{
            get{
                if(instance == null){
                    instance = new GameEnvironment();
                    instance.Checkpoints.AddRange(GameObject.FindGameObjectsWithTag("Checkpoint"));
                    instance.safeLocation = GameObject.FindGameObjectWithTag("safe").transform;
                    instance.checkpoints = instance.checkpoints.OrderBy(waypoint => waypoint.name).ToList();
                }
                return instance;
            }
        }
    }
}