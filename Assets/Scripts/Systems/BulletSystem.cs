using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSystem
{
    private GameState _gameState;
    private GameEvent _gameEvent;

    private PlayerComponent _playerComp;
    private BulletComponent _bulletComp;
    private BulletPool _bulletPool;
    private LayerMask enemyLayer = 1 << 6;
    private LayerMask obstacleLayer = 1 << 7;

    private List<BulletComponent> bulletsToRemove = new List<BulletComponent>();

    public BulletSystem(GameState gameState, GameEvent gameEvent, BulletPool bulletPool)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;

        _bulletComp = _gameState.bulletPrefab.GetComponent<BulletComponent>();
        _bulletPool = bulletPool;

        _gameEvent.startGame += Init;
    }

    void Init()
    {
        _playerComp = _gameState.player.GetComponent<PlayerComponent>();
    }

    public void OnUpdate()
    {
        if (_gameState.gameStatus != GameStatus.IsPlaying) return;
        BulletAction();
        if (Input.GetMouseButton(0))
        {
            if (_playerComp.coolTimer > 2 /_playerComp.coolTimeLevel)
            {
                _playerComp.coolTimer = 0;
                FireEnemy();
            }
        }

    }

    private void BulletAction()
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

    private bool ObstacleInPath(BulletComponent bulletComp)
    {
        RaycastHit2D hit = Physics2D.Raycast(_bulletComp.transform.position, _bulletComp.transform.up, 0.5f, obstacleLayer);
        if (hit.collider != null)
        {
            bulletsToRemove.Add(bulletComp);
            return true;
        }
        return false;
    }

    private void MoveForward()
    {
        RaycastHit2D hit = Physics2D.Raycast(_bulletComp.transform.position, _bulletComp.transform.up, 0.5f, enemyLayer);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                _gameEvent.bulletHitEnemy?.Invoke(hit.collider.gameObject);
                _gameEvent.damageText?.Invoke(hit.collider.gameObject);
                bulletsToRemove.Add(_bulletComp);
                return;
            }
        }
        else _bulletComp.transform.position += _bulletComp.transform.up * (_playerComp.bulletSpeedLevel * _playerComp.bulletSpeed) * Time.deltaTime;
    }

    private void FireEnemy()
    {
        // for (int i=0; i<_bulletComp.bulletSplitCount)
        // {
            GameObject bullet = _bulletPool.OnShootBullet(_gameState.bulletPrefab);
            BulletComponent bulletComponent = bullet.GetComponent<BulletComponent>();
            _gameState.bullets.Add(bulletComponent);
        // }
    }
}
