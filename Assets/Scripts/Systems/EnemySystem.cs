using UnityEngine;

public class EnemySystem
{
    GameState _gameState;
    GameEvent _gameEvent;

    float _eSpeed;
    float avoidDistance = 1f;
    LayerMask playerLayer = 1 << 3;
    LayerMask obstacleLayer = 1 << 7;
    PlayerComponent _player;
    EnemyComponent _enemyComp;
    public EnemySystem(GameState gameState, GameEvent gameEvent)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;

        _eSpeed = _gameState.enemyPrefab.GetComponent<EnemyComponent>().speed;
        _player = _gameState.player.GetComponent<PlayerComponent>();

        _gameEvent.bulletHitEnemy += DamagedByBullet;
    }

    public void OnUpdate()
    {
        EnemyAction();
    }

    void EnemyAction()
    {
        foreach (EnemyComponent enemyComp in _gameState.enemies)
        {
            _enemyComp = enemyComp;
            _enemyComp.coolDownTimer += Time.deltaTime;
            Vector3 dirToPlayer = (_gameState.player.transform.position - _enemyComp.transform.position).normalized;
            _enemyComp.emptyObj.transform.rotation = Quaternion.LookRotation(dirToPlayer);
            if (!ObstacleInPath())
            {
                MoveTowardsPlayer();
            }
        }
    }

    bool ObstacleInPath()
    {
        RaycastHit hit;

        if (Physics.Raycast(_enemyComp.transform.position, _enemyComp.emptyObj.transform.forward, out hit, 2.0f, obstacleLayer))
        {
            Debug.Log("#hit object");
            AvoidObstacle(hit);
            return true;
        }
        return false;
    }

    void MoveTowardsPlayer()
    {
        RaycastHit hit;

        if (Physics.Raycast(_enemyComp.transform.position, _enemyComp.emptyObj.transform.forward, out hit, 0.5f, playerLayer))
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (_enemyComp.coolDownTimer < _enemyComp.coolTime) return;
                _enemyComp.coolDownTimer = 0;
                EnemyHitPlayer();
                return;
            }
        }
        else _enemyComp.transform.position += _enemyComp.emptyObj.transform.forward * _eSpeed * Time.deltaTime;
    }

    void AvoidObstacle(RaycastHit hit)
    {
        Vector3 avoidDir = Vector3.right * (Random.value > 0.5f ? -avoidDistance : avoidDistance);

        _enemyComp.transform.position += avoidDir;
        Vector3 dirToPlayer = (_player.gameObject.transform.position - _enemyComp.transform.position).normalized;
        _enemyComp.emptyObj.transform.rotation = Quaternion.LookRotation(dirToPlayer);
    }

    void EnemyHitPlayer()
    {
        _gameEvent.enemyHitPlayer?.Invoke(_enemyComp.gameObject);
    }

    void DamagedByBullet(GameObject bullet, GameObject enemy)
    {
        EnemyComponent enemyComp = enemy.GetComponent<EnemyComponent>();
        BulletComponent bulletComp = bullet.GetComponent<BulletComponent>();
        int damage = bulletComp.bulletDamage;
        enemyComp.hp -= damage;
        enemyComp.hpBar.value = enemyComp.hp;
        if (enemyComp.hp <= 0) _gameEvent.onRemoveEnemy?.Invoke(enemyComp);
    }
}
