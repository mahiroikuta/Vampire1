using System;
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

    public LayerMask mask = -1;
    public EnemySystem(GameState gameState, GameEvent gameEvent)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;

        _eSpeed = _gameState.enemyPrefab.GetComponent<EnemyComponent>().speed;

    }

    public void OnUpdate()
    {
        EnemyAction();
    }

    void EnemyAction()
    {
        Vector3 pos = _gameState.player.transform.position;
        foreach (GameObject enemy in _gameState.enemys)
        {
            MoveEnemy(enemy, pos);
            EnemyDirection(enemy, pos);
        }
    }

    void MoveEnemy(GameObject enemy, Vector3 pos)
    {
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, pos, _eSpeed * Time.deltaTime);
    }

    void EnemyDirection(GameObject enemy, Vector3 pos)
    {
        Vector3 ePos = enemy.transform.position;
        Vector3 direc = (_gameState.player.transform.position - ePos).normalized;
        Ray ray = new Ray(ePos, direc);

        RaycastHit hit;

        Debug.DrawLine(ray.origin, ray.origin+ray.direction * 100, Color.red, 0);

        bool isHit = Physics.SphereCast(ePos, 0.5f, direc, out hit, 0.1f, mask);

        if (isHit)
        {
            GameObject hitObj = hit.collider.gameObject;
            if (hitObj.layer == 3)
            {
                _gameEvent.enemyHitPlayer?.Invoke(hitObj);
                return;
            }
            else return;
        }
    }
}
