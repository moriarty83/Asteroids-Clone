
using System.Collections;


public abstract class State
{
    protected GameManager GameManager;

    public State(GameManager gameManager)
    {
        GameManager = gameManager;
    }

    public virtual IEnumerator Enter()
    {
        yield break;
    }

    public virtual IEnumerator UpdateState()
    {
        yield break;
    }

    public virtual IEnumerator Exit()
    {
        yield break;
    }
}

