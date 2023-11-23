using UnityEngine;

public class EnemySystem
{
    GameState _gameState;
    GameEvent _gameEvent;

    float time;
    Vector3 _pPos;
    float _eSpeed;
    private float avoidDistance = 1f;
    private float cooldownTimer = 0; // 持ち時間
    private float coolTime = 1;
    private LayerMask playerLayer = 1 << 3;
    private LayerMask obstacleLayer = 1 << 7;

    PlayerComponent _player;
    EnemyComponent _enemyComp;
    public EnemySystem(GameState gameState, GameEvent gameEvent)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;

        _eSpeed = _gameState.enemyPrefab.GetComponent<EnemyComponent>().speed;
        _player = _gameState.player.GetComponent<PlayerComponent>();
    }

    public void OnUpdate()
    {
        time += time.deltaTime;
        EnemyAction();
    }

    void EnemyAction()
    {
        Vector3 pos = _gameState.player.transform.position;
        foreach (EnemyComponent enemyComp in _gameState.enemies)
        {
            _enemyComp = enemyComp;
            Vector3 dirToPlayer = (_gameState.player.transform.position - _enemyComp.transform.position).normalized;
            _enemyComp.empty.transform.rotation = Quaternion.LookRotation(dirToPlayer);
            if (!ObstacleInPath())
            {
                MoveTowardsPlayer();
            }
        }
    }

    bool ObstacleInPath()
    {
        RaycastHit hit;

        if (Physics.Raycast(_enemyComp.transform.position, _enemyComp.empty.transform.forward, out hit, 2.0f, obstacleLayer))
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

        if (Physics.Raycast(_enemyComp.transform.position, _enemyComp.empty.transform.forward, out hit, 0.5f, playerLayer))
        {
            if (hit.collider.gameObject == _player.gameObject)
            {
                EnemyHitPlayer();
            }
        }
        else
        {
            _enemyComp.transform.position += _enemyComp.empty.transform.forward * _eSpeed * Time.deltaTime;
        }
    }

    void AvoidObstacle(RaycastHit hit)
    {
        Vector3 avoidDir = Vector3.right * (Random.value > 0.5f ? -avoidDistance : avoidDistance);

        _enemyComp.transform.position += avoidDir;
        Vector3 dirToPlayer = (_player.gameObject.transform.position - _enemyComp.transform.position).normalized;
        _enemyComp.empty.transform.rotation = Quaternion.LookRotation(dirToPlayer);
    }

    void EnemyHitPlayer()
    {
        _gameEvent.enemyHitPlayer?.Invoke(_enemyComp.gameObject);
    }
}
