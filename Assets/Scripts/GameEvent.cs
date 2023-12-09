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
    public Action showLevelUpScreen;
    public Action hideLevelUpScreen;
    public Action levelUp;
    public Action selectEnhance;
    public Action setTitleScreen;
    public Action startGame;
}
