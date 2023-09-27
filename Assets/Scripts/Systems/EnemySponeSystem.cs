using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySponeSystem
{
    GameState _gameState;
    GameEvent _gameEvent;

    public EnemySponeSystem(GameState gameState, GameEvent gameEvent)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;
    }

    public void OnUpdate()
    {
    }

    // 敵を一定距離離れた位置に生成
    void sponeEnemy()
    {
        // ObjectPool使って画面外に生成
    }
}
