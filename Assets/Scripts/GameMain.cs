using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    [SerializeField]
    GameState gameState;
    [SerializeField]
    GameEvent gameEvent;
    LevelUpScreen levelUpScreen;

    PlayerSystem playerSystem;
    CameraSystem cameraSystem;
    EnemySpawnSystem enemySpawnSystem;
    BulletSystem bulletSystem;
    DamageTextSystem damageTextSystem;
    EnhanceSystem enhanceSystem;
    EnemyPool enemyPool;
    BulletPool bulletPool;
    DamageTextPool damageTextPool;

    EnemySystem enemySystem;
    // Start is called before the first frame update
    void Start()
    {
        gameEvent.showLevelUpScreen += levelUpScreen.OnShow;
        gameEvent.hideLevelUpScreen += levelUpScreen.OnHide;
        playerSystem = new PlayerSystem(gameState, gameEvent);
        cameraSystem = new CameraSystem(gameState, gameEvent);
        enemyPool = new EnemyPool(gameState, gameEvent);
        enemySpawnSystem = new EnemySpawnSystem(gameState, gameEvent, enemyPool);
        enemySystem = new EnemySystem(gameState, gameEvent);
        bulletPool = new BulletPool(gameState, gameEvent);
        bulletSystem = new BulletSystem(gameState, gameEvent, bulletPool);
        damageTextPool = new DamageTextPool(gameState, gameEvent);
        damageTextSystem = new DamageTextSystem(gameState, gameEvent, damageTextPool);
    }

    // Update is called once per frame
    void Update()
    {
        enhanceSystem.OnUpdate();
        playerSystem.OnUpdate();
        cameraSystem.OnUpdate();
        enemySpawnSystem.OnUpdate();
        enemySystem.OnUpdate();
        bulletSystem.OnUpdate();
        damageTextSystem.OnUpdate();
    }
}
