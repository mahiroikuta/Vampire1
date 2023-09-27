using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnSystem
{
    GameState _gameState;
    GameEvent _gameEvent;

    public EnemySpawnSystem(GameState gameState, GameEvent gameEvent)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;
    }

    public void OnUpdate()
    {
    }

    // 敵を一定距離離れた位置に生成
    void SpawnEnemy()
    {
        // ObjectPool使って画面外に生成
    }
}
