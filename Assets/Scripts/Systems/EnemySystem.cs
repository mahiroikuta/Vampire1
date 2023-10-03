using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem
{
    GameState _gameState;
    GameEvent _gameEvent;

    float time;
    Vector3 _pPos;
    float _eSpeed;
    public EnemySystem(GameState gameState, GameEvent gameEvent)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;

        _eSpeed = _gameState.enemyPrefab.GetComponent<EnemyComponent>().speed;

    }

    public void OnUpdate()
    {
        MoveEnemy();
    }

    void MoveEnemy()
    {
        foreach (GameObject enemy in _gameState.enemys)
        {
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, _gameState.player.transform.position, _eSpeed * Time.deltaTime);
        }
    }
}
