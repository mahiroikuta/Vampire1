using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public Action<EnemyComponent> onRemoveEnemy;
    public Action<BulletComponent> onRemoveBullet;
    public Action<DamageTextComponent> onRemoveText;
    public Action<GameObject> bulletHitEnemy;
    public Action<GameObject> enemyHitPlayer;
    public Action<GameObject> damageText;
    public Action showLevelUp;
    public Action backGame;
    public Action showTitle;
    public Action startGame;
    public Action resetGame;
    public Action showResult;
    public Action gameOver;
    public Action<float> updateXp;
}
