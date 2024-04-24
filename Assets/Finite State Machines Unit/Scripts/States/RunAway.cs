using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FiniteStateMachines{
    public class RunAway : State
    {
        public RunAway(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc,_agent,_anim,_player) {
            name = STATE.RUNAWAY;
            
        }

        public override void Enter(){
            anim.SetTrigger("isRunning");
            agent.speed = 6;
            agent.isStopped = false;
            agent.destination = GameEnvironment.Singleton.safeLocation.position;
            base.Enter();
        }

        public override void EventUpdate(){
            if(!agent.hasPath){ //You can also use agent.remainingDistance < 1
                nextState = new Patrol(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }
        }

        public override void Exit(){
            anim.ResetTrigger("isRunning");
            base.Exit();
        }
    }
}
