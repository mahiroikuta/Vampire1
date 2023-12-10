using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScreen : MonoBehaviour
{
    public virtual void Init(GameState gameState, GameEvent gameEvent){}
    public virtual void OnShow()
    {
        this.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public virtual void OnHide()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
