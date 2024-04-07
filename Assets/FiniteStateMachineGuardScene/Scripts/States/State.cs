using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class State
{
    public enum STATE{
        IDLE, PATROL, PURSUE, ATTACK, SLEEP
    };

    public enum EVENT{
        ENTER, UPDATE, EXIT
    };

    public STATE name;
    protected EVENT stage;
    protected GameObject npc;
    protected Animator anim;
    protected Transform player;
    protected State nextState;
    protected NavMeshAgent agent;

    float visionDistance = 10f;
    float visionAngle = 30f;
    float shootDistance = 7f;

    public State(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player){
        npc = _npc;
        agent = _agent;
        anim = _anim;
        stage = EVENT.ENTER;
        player = _player;
    }

    public virtual void Enter(){ stage = EVENT.UPDATE;}
    public virtual void EventUpdate() { stage = EVENT.UPDATE;}
    public virtual void Exit(){ stage = EVENT.EXIT;}

    public State Process(){
        if(stage == EVENT.ENTER) { Enter();}
        if(stage == EVENT.UPDATE) { EventUpdate();}
        if(stage == EVENT.EXIT) { 
            Exit();
            return nextState;
        }
        return this;
    }

    public bool CanSeePlayer(){
        Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);

        if(direction.magnitude < visionDistance && angle < visionAngle){
            return true;
        }
        return false;
    }

    public bool CanAttackPlayer(){
        Vector3 direction = player.position - npc.transform.position;
        if(direction.magnitude < shootDistance){
            return true;
        }
        return false;
    }
}