using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : State{
    private int currentIndex = -1;
    
    public Patrol(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc,_agent,_anim,_player) {
        name = STATE.PATROL;
        agent.speed = 2;
        agent.isStopped = false;
    }

    public override void Enter(){
        float lastDistance = Mathf.Infinity;
        for(int i = 0;i < GameEnvironment.Singleton.Checkpoints.Count; i++){
            GameObject thisWP = GameEnvironment.Singleton.Checkpoints[i];
            float distance = Vector3.Distance(npc.transform.position, thisWP.transform.position);
            if(distance < lastDistance){
                currentIndex = i-1; // Due to how the actual code works, we need to subtract 1 to the index to avoid strange behaviour with EventUpdate() code
                lastDistance = distance;
            }
        }
        anim.SetTrigger("isWalking");
        base.Enter();
    }

    public override void EventUpdate(){
        if(agent.remainingDistance < 1){
            if(currentIndex >= GameEnvironment.Singleton.Checkpoints.Count -1){
                currentIndex = 0;
            } else{
                currentIndex++;
            }
            agent.SetDestination(GameEnvironment.Singleton.Checkpoints[currentIndex].transform.position);
        }

        if(IsPlayerBehind()){
            nextState = new RunAway(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }

        if(CanSeePlayer()){
            nextState = new Pursue(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit(){
        anim.ResetTrigger("isWalking");
        base.Exit();
    }
}
