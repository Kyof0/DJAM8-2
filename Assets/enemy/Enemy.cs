using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Enemy : MonoBehaviour
{
    public int AttackDuration;
    public int IdleDuration;
    public bool isAble = true;
    public enum EnemyState{
        Idle,
        Attacking
    }
    public EnemyState currentState;
    void Start(){
        currentState = EnemyState.Idle;
    }

    // Update is called once per frame
    void Update(){
        HandleState(currentState);
    }
    public void HandleState(EnemyState state){
        switch(state) {
            case EnemyState.Idle:
                Idle();
                break;

            case EnemyState.Attacking:
                Attacking();
                break;
        }
    }
    public void Idle() {
        if (isAble) {
            StartCoroutine(WaitFor(IdleDuration));
            isAble = false;
        }
    }
    public void Attacking() {
        if (isAble)
        {
            StartCoroutine(WaitFor(AttackDuration));
            isAble = false;
        }
    }
    public IEnumerator WaitFor(float delay){
        Debug.Log($"{currentState}: {delay}");
        yield return new WaitForSeconds(delay);
        if (currentState == EnemyState.Idle)
        {
            currentState = EnemyState.Attacking;
            isAble = true;
        }
        else
        {
            currentState = EnemyState.Idle;
            isAble = true;
        }
    }
}
