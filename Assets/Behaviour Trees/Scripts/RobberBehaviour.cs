using System.Collections;
using System.Collections.Generic;
using BehaviourTrees;
using UnityEngine;

namespace BehaviourTrees
{
    public class RobberBehaviour : MonoBehaviour
    {
        private BehaviourTree tree;
        void Start()
        {
            tree = new BehaviourTree();
            Node steal = new Node("Steal something");
            Node goToDiamond = new Node("Go To Diamond");
            Node goToVan = new Node("Go To Van");
            steal.AddChild(goToDiamond);
            steal.AddChild(goToVan);
            tree.AddChild(steal);

            Node eat = new Node("Eat Something");
            Node pizza = new Node("Go To Pizza Shop");
            Node buy = new Node("BuyPizza");

            tree.PrintTree();
        }

        void Update()
        {
            
        }
    }
}