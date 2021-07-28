using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BeginState : State
{
    public BeginState(GameManager gameManager) : base(gameManager)
    {

    }

    public override IEnumerator Enter()
    {
        Debug.Log("Begin State");
        GameManager.alive = true;
        GameManager.lives = 3;
        yield return new WaitForEndOfFrame();

        GameManager.SetState(new PlayState(GameManager));
        GameManager.gameText.enabled = false;
    }

    public override IEnumerator UpdateState()
    {
        yield break;
    }

    public override IEnumerator Exit()
    {
        yield break;
    }





}

