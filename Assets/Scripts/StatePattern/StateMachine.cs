using UnityEngine;


public abstract class StateMachine : MonoBehaviour
{


    protected State CurrentState;

    public void SetState(State state)
    {
        Debug.Log(CurrentState);
        if(CurrentState != null)
        {
            CurrentState.Exit();
        }
        CurrentState = state;
        StartCoroutine(routine: CurrentState.Enter());
    }

    public void UpdateState()
    {
        StartCoroutine(routine: CurrentState.UpdateState());
    }
}


