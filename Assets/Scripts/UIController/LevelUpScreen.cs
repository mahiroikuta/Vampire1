using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpScreen : MonoBehaviour
{
    GameEvent gameEvent;
    public void OnShow()
    {
        this.gameObject.SetActive(true);
    }

    public void OnHide()
    {
        this.gameObject.SetActive(false);
        gameEvent.hideLevelUpScreen?.Invoke();
    }
}
