using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScreen : MonoBehaviour
{
    public virtual void Init(GameState gameState, GameEvent gameEvent){}
    public virtual void OnShow()
    {
        this.gameObject.SetActive(true);
    }

    public virtual void OnHide()
    {
        this.gameObject.SetActive(false);
    }
}
