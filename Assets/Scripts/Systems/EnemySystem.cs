using UnityEngine;

public class EnemySystem
{
    GameState _gameState;
    GameEvent _gameEvent;

    float time;
    Vector3 _pPos;
    float _eSpeed;
    private float avoidDistance = 1f;
    private LayerMask playerLayer = 3;
    private LayerMask obstacleLayer = 7;

    PlayerComponent _player;
    EnemyComponent _enemy;
    EnemyComponent empty;
    public EnemySystem(GameState gameState, GameEvent gameEvent)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;

        _eSpeed = _gameState.enemyPrefab.GetComponent<EnemyComponent>().speed;
        _player = _gameState.player.GetComponent<PlayerComponent>();
    }

    public void OnUpdate()
    {
        EnemyAction();
    }

    void EnemyAction()
    {
        Vector3 pos = _gameState.player.transform.position;
        foreach (EnemyComponent enemy in _gameState.enemies)
        {
            _enemy = enemy;
            Vector3 dirToPlayer = (_gameState.player.transform.position - _enemy.transform.position).normalized;
            _enemy.empty.transform.rotation = Quaternion.LookRotation(dirToPlayer);
            if (!ObstacleInPath())
            {
                MoveTowardsPlayer();
            }
        }
    }

    bool ObstacleInPath()
    {
        RaycastHit hit;

        Vector3 forward = _enemy.empty.transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(_enemy.transform.position, forward, Color.blue);

        if (Physics.Raycast(_enemy.transform.position, _enemy.empty.transform.forward, out hit, 2.0f, obstacleLayer))
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

        if (Physics.Raycast(_enemy.transform.position, _enemy.empty.transform.forward, out hit, 0.5f, playerLayer))
        {
            Debug.Log("#hit player");
            if (hit.collider.gameObject == _gameState.player.gameObject)
            {
                EnemyHitPlayer();
            }
        }
        else
        {
            _enemy.transform.position += _enemy.empty.transform.forward * _eSpeed * Time.deltaTime;
        }
    }

    void AvoidObstacle(RaycastHit hit)
    {
        Vector3 avoidDir = Vector3.right * (Random.value > 0.5f ? -avoidDistance : avoidDistance);

        _enemy.transform.position += avoidDir;
        Vector3 dirToPlayer = (_player.gameObject.transform.position - _enemy.transform.position).normalized;
        _enemy.empty.transform.rotation = Quaternion.LookRotation(dirToPlayer);
    }

    void EnemyHitPlayer()
    {
        _gameEvent.enemyHitPlayer?.Invoke(_enemy.gameObject);
    }
}
