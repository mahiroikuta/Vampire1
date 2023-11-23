using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public Action<GameObject> onRemoveEnemy;
    public Action<GameObject> onRemoveBullet;
    public Action<GameObject> bulletHitEnemy;
    public Action<GameObject> enemyHitPlayer;
}
