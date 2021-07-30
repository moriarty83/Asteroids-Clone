using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueState : State
{
    public ContinueState(GameManager gameManager) : base(gameManager)
    {
    }

    public override IEnumerator Enter()
    {
        GameManager.gameText.enabled = true;
        GameManager.gameText.text = "Your Ship Got Busted\n" +
            "You have " + GameManager.lives + "lives left.\n" +
            "Press Return to Continue";


        yield break;
    }

    public override IEnumerator UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {

            GameManager.alive = true;
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