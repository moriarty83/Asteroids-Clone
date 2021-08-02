using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinState : State
{
    public WinState(GameManager gameManager) : base(gameManager)
    {

    }

    public override IEnumerator Enter()
    {
        GameManager.gameText.enabled = true;
        GameManager.gameText.text = "You Completed Level " + GameManager.level + "!\n\n" +
            "Press Return to Continue";


        yield break;
    }

    public override IEnumerator UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameManager.level += 1;
            GameManager.gameText.enabled = false;
            GameManager.SetState(new PlayState(GameManager));
        }
        yield break;
    }

    public override IEnumerator Exit()
    {
        yield break;
    }

}