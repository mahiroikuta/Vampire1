using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSystem : MonoBehaviour
{
    GameState _gameState;
    GameEvent _gameEvent;

    PlayerComponent _playerComp;
    BulletComponent _bulletComp;
    LayerMask enemyLayer = 1 << 6;
    LayerMask obstacleLayer = 1 << 7;

    public BulletSystem(GameState gameState, GameEvent gameEvent)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;

        _playerComp = gameState.player.GetComponent<PlayerComponent>();


        Init();
    }

    void Init()
    {

    }
    void OnUpdate()
    {
        BulletAction();
        if (Input.GetMouseButtonDown(0))
        {
            if (_bulletComp.bulletCoolTimer > _bulletComp.bulletCoolDown)
            {
                FireEnemy();
            }
        }

    }

    void BulletAction()
    {
        foreach (BulletComponent bulletComp in _gameState.bullets)
        {
            if (!ObstacleInPath(bulletComp))
            {
                MoveForward();
            }
        }
    }

    bool ObstacleInPath(BulletComponent bulletComp)
    {
        RaycastHit hit;

        if (Physics.Raycast(_bulletComp.transform.position, _bulletComp.transform.forward, out hit, 0.5f, obstacleLayer))
        {
            _gameEvent.onRemoveBullet?.Invoke(bulletComp.gameObject);
            return true;
        }
        return false;
    }

    void MoveForward()
    {
        RaycastHit hit;

        if (Physics.Raycast(_bulletComp.transform.position, _bulletComp.transform.forward, out hit, 0.5f, enemyLayer))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                _gameEvent.bulletHitEnemy?.Invoke(_bulletComp.gameObject);
                _gameEvent.onRemoveBullet?.Invoke(_bulletComp.gameObject);
            }
        }
        else
        {
            _bulletComp.transform.position += _bulletComp.transform.forward * _bulletComp.bulletSpeed * Time.deltaTime;
        }
    }

    void FireEnemy()
    {
        // for (int i=0; i<_bulletComp.bulletSplitCount)
        // {
            GameObject bullet = _bulletPool.OnSpawnEnemy(_gameState.enemyPrefab);
            EnemyComponent enemyComponent = enemy.GetComponent<EnemyComponent>();
            _gameState.enemies.Add(enemyComponent);
        // }
    }
}
