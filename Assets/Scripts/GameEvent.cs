using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public Action<GameObject> onRemoveGameObject;
    public Action<GameObject> enemyHitPlayer;
}
