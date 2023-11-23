using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSystem
{
    GameState _gameState;
    GameEvent _gameEvent;

    PlayerComponent _player;
    BulletComponent _bullet;
    Vector3 _pos;
    public PlayerSystem(GameState gameState, GameEvent gameEvent)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;

        _player = _gameState.player.GetComponent<PlayerComponent>();
        _bullet = _gameState.bulletPrefab.GetComponent<BulletComponent>();

        _gameEvent.enemyHitPlayer += DamagedByEnemy;

        Init();
    }

    void Init()
    {
        Vector3 basePos = new Vector3(0, 0, 0);
        _gameState.player.transform.position = basePos;
        _player.hpBar.maxValue = _player.hp;
        _player.hpBar.value = _player.hp;
    }

    public void OnUpdate()
    {
        MovePlayer();
        TrackCursor();
    }

    // WASDで移動する
    void MovePlayer()
    {
        float hor=0;
        float ver=0;
        if (Input.GetKey(KeyCode.W))
        {
            ver = _player.speed * Time.deltaTime * 5;
        }
        if (Input.GetKey(KeyCode.S))
        {
            ver = -_player.speed * Time.deltaTime * 5;
        }
        if (Input.GetKey(KeyCode.D))
        {
            hor = _player.speed * Time.deltaTime * 5;
        }
        if (Input.GetKey(KeyCode.A))
        {
            hor = -_player.speed * Time.deltaTime * 5;
        }
        Vector3 distance = new Vector3(hor, ver, 0);
        _pos = _pos+distance;

        if (_pos.x > 32f)
        {
            _pos.x = 32f;
        }
        else if (_pos.x < -32f)
        {
            _pos.x = -32f;
        }

        if (_pos.y > 40f)
        {
            _pos.y = 40f;
        }
        else if (_pos.y < -40f)
        {
            _pos.y = -40f;
        }
        _player.transform.position = _pos;
    }

    // マウスカーソルの方向を向く
    void TrackCursor()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint (_gameState.player.transform.localPosition);
		Quaternion rotation = Quaternion.LookRotation(Vector3.forward, Input.mousePosition - pos );
		_player.emptyObj.transform.rotation = rotation;
    }

    void DamagedByEnemy(GameObject enemy)
    {
        int hp = _player.hp;
        int attack = enemy.GetComponent<EnemyComponent>().attack;
        if (hp <= attack)
        {
            hp = 0;
        }
        else
        {
            hp -= attack;
        }
        _player.hpBar.value = hp;
        _player.hp = hp;
    }
}
