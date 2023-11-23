using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    [SerializeField]
    GameState gameState;
    [SerializeField]
    GameEvent gameEvent;

    PlayerSystem playerSystem;
    CameraSystem cameraSystem;
    EnemySpawnSystem enemySpawnSystem;
    BulletSystem bulletSystem;
    EnemyPool enemyPool;
    BulletPool bulletPool;

    EnemySystem enemySystem;
    // Start is called before the first frame update
    void Start()
    {
        playerSystem = new PlayerSystem(gameState, gameEvent);
        cameraSystem = new CameraSystem(gameState, gameEvent);
        enemyPool = new EnemyPool(gameState, gameEvent);
        enemySpawnSystem = new EnemySpawnSystem(gameState, gameEvent, enemyPool);
        enemySystem = new EnemySystem(gameState, gameEvent);
        bulletPool = new BulletPool(gameState, gameEvent);
        bulletSystem = new BulletSystem(gameState, gameEvent, bulletPool);
    }

    // Update is called once per frame
    void Update()
    {
        playerSystem.OnUpdate();
        cameraSystem.OnUpdate();
        enemySpawnSystem.OnUpdate();
        enemySystem.OnUpdate();
        bulletSystem.OnUpdate();
    }
}
