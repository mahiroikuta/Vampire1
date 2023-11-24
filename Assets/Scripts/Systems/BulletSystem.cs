using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSystem
{
    GameState _gameState;
    GameEvent _gameEvent;

    PlayerComponent _playerComp;
    BulletComponent _bulletComp;
    BulletPool _bulletPool;
    LayerMask enemyLayer = 1 << 6;
    LayerMask obstacleLayer = 1 << 7;

    List<BulletComponent> bulletsToRemove = new List<BulletComponent>();

    public BulletSystem(GameState gameState, GameEvent gameEvent, BulletPool bulletPool)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;

        _playerComp = _gameState.player.GetComponent<PlayerComponent>();
        _bulletComp = _gameState.bulletPrefab.GetComponent<BulletComponent>();
        _bulletPool = bulletPool;

        // Init();
    }

    // void Init()
    // {

    // }

    public void OnUpdate()
    {
        BulletAction();
        if (Input.GetMouseButton(0))
        {
            if (_playerComp.coolTimer > _bulletComp.bulletCoolDown)
            {
                _playerComp.coolTimer = 0;
                FireEnemy();
            }
        }

    }

    void BulletAction()
    {
        foreach (BulletComponent bulletComp in _gameState.bullets)
        {
            _bulletComp = bulletComp;
            if (!ObstacleInPath(bulletComp))
            {
                MoveForward();
            }
        }

        foreach (BulletComponent bulletComp in bulletsToRemove)
        {
            _gameEvent.onRemoveBullet?.Invoke(bulletComp);
        }
        bulletsToRemove.Clear();
    }

    bool ObstacleInPath(BulletComponent bulletComp)
    {
        RaycastHit hit;

        if (Physics.Raycast(_bulletComp.transform.position, _bulletComp.transform.up, out hit, 0.5f, obstacleLayer))
        {
            bulletsToRemove.Add(bulletComp);
            return true;
        }
        return false;
    }

    void MoveForward()
    {
        RaycastHit hit;

        if (Physics.Raycast(_bulletComp.transform.position, _bulletComp.transform.up, out hit, 0.5f, enemyLayer))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                _gameEvent.bulletHitEnemy?.Invoke(_bulletComp.gameObject, hit.collider.gameObject);
                bulletsToRemove.Add(_bulletComp);
                return;
            }
        }
        else _bulletComp.transform.position += _bulletComp.transform.up * _bulletComp.bulletSpeed * Time.deltaTime;
    }

    void FireEnemy()
    {
        // for (int i=0; i<_bulletComp.bulletSplitCount)
        // {
            GameObject bullet = _bulletPool.OnShootBullet(_gameState.bulletPrefab);
            BulletComponent bulletComponent = bullet.GetComponent<BulletComponent>();
            _gameState.bullets.Add(bulletComponent);
        // }
    }
}
