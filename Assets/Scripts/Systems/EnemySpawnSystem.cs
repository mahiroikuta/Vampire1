using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnSystem
{
    GameState _gameState;
    GameEvent _gameEvent;
    EnemyPool _enemyPool;

    float time;

    public EnemySpawnSystem(GameState gameState, GameEvent gameEvent, EnemyPool enemyPool)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;
        _enemyPool = enemyPool;

        time = 0;
    }

    public void OnUpdate()
    {
        CountTime();
        if (time > 1)
        {
            time = 0;
            SpawnEnemy();
        }
    }

    void CountTime()
    {
        time += Time.deltaTime;
    }
    // 敵を一定距離離れた位置に生成
    void SpawnEnemy()
    {
        // ObjectPool使って画面外に生成
        GameObject enemy = _enemyPool.OnSpawnEnemy(_gameState.enemyPrefab);
        EnemyComponent enemyComponent = enemy.GetComponent<EnemyComponent>();
        _gameState.enemies.Add(enemyComponent);
    }
}
