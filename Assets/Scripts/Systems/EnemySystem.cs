using UnityEngine;

public class EnemySystem
{
    private GameState _gameState;
    private GameEvent _gameEvent;

    private float _eSpeed;
    private float avoidDistance = 0.1f;
    private LayerMask playerLayer = 1 << 3;
    private LayerMask obstacleLayer = 1 << 7;
    private PlayerComponent _player;
    private EnemyComponent _enemyComp;
    public EnemySystem(GameState gameState, GameEvent gameEvent)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;

        _eSpeed = _gameState.enemyPrefab.GetComponent<EnemyComponent>().speed;

        _gameEvent.startGame += Init;
        _gameEvent.bulletHitEnemy += DamagedByBullet;
    }

    private void Init()
    {
        _player = _gameState.player.GetComponent<PlayerComponent>();
    }

    public void OnUpdate()
    {
        if (_gameState.gameStatus != GameStatus.IsPlaying) return;
        EnemyAction();
    }

    private void EnemyAction()
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

    private bool ObstacleInPath()
    {
        RaycastHit2D hit = Physics2D.Raycast(_enemyComp.transform.position, _enemyComp.emptyObj.transform.forward, 5.0f, obstacleLayer);

        if (hit.collider != null)
        {
            Debug.Log("#hit object");
            // AvoidObstacle();
            return true;
        }
        return false;
    }

    private void MoveTowardsPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(_enemyComp.transform.position, _enemyComp.emptyObj.transform.forward, 0.5f, playerLayer);

        if (hit.collider != null)
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

    private void AvoidObstacle()
    {
        Vector3 avoidDir = Vector3.right * (Random.value > 0.5f ? -avoidDistance : avoidDistance);

        _enemyComp.transform.position += avoidDir;
    }

    private void EnemyHitPlayer()
    {
        _gameEvent.enemyHitPlayer?.Invoke(_enemyComp.gameObject);
        _gameEvent.damageText?.Invoke(_player.gameObject);
    }

    private void DamagedByBullet(GameObject enemy)
    {
        EnemyComponent enemyComp = enemy.GetComponent<EnemyComponent>();
        float damage = _player.damageUpLevel * _player.attack;
        enemyComp.hp -= damage;
        enemyComp.hpBar.value = enemyComp.hp;
        Debug.Log(enemyComp.hp);
        if (enemyComp.hp <= 0)
        {
            _gameEvent.onRemoveEnemy?.Invoke(enemyComp);
            _player.xp += enemyComp.dropXp;
        }
    }
}
