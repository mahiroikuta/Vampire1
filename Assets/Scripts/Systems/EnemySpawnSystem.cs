using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnSystem
{
    private GameState _gameState;
    private GameEvent _gameEvent;
    private EnemyPool _enemyPool;

    private float duration;

    public EnemySpawnSystem(GameState gameState, GameEvent gameEvent, EnemyPool enemyPool)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;
        _enemyPool = enemyPool;

        duration = 0;
    }

    public void OnUpdate()
    {
        if (_gameState.gameStatus != GameStatus.IsPlaying) return;
        duration += Time.deltaTime;
        if (duration > _gameState.enemyDuration)
        {
            duration = 0;
            SpawnEnemy();
        }
    }

    // 敵を一定距離離れた位置に生成
    private void SpawnEnemy()
    {
        // ObjectPool使って画面外に生成
        GameObject enemy = _enemyPool.OnSpawnEnemy(_gameState.enemyPrefab);
        EnemyComponent enemyComponent = enemy.GetComponent<EnemyComponent>();
        _gameState.enemies.Add(enemyComponent);
    }
}
