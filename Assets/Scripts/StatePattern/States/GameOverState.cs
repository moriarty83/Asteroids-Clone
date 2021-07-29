using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverState : State
{
    public GameOverState(GameManager gameManager) : base(gameManager)
    {
    }

    public override IEnumerator Enter()
    {
        GameManager.gameText.enabled = true;
        GameManager.gameText.text = "Game Over\n" +
            "You made it to level " + GameManager.level + "\n" +
            "Click to Begin a New Game";
        yield break;
    }

    public override IEnumerator UpdateState()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.level = 1;
            GameManager.score = 0;
            GameManager.lives = 3;
            GameManager.SetState(new BeginState(GameManager));
        }
        yield break;
    }

    public override IEnumerator Exit()
    {
        Debug.Log("Running Exit");
        yield break;
    }
}