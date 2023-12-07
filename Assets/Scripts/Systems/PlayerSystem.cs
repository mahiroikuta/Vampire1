using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSystem
{
    GameState _gameState;
    GameEvent _gameEvent;

    PlayerComponent _playerComp;
    BulletComponent _bullet;
    Animator playerAnim;
    Vector3 _pos;
    Vector3 mousePos;
    Vector2 direction;
    bool moving = false;
    string trigger;
    string[] triggers = {"isUp", "isDown", "isRight", "isLeft", "moveUp", "moveDown", "moveRight", "moveLeft"};
    public PlayerSystem(GameState gameState, GameEvent gameEvent)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;

        _playerComp = _gameState.player.GetComponent<PlayerComponent>();
        _bullet = _gameState.bulletPrefab.GetComponent<BulletComponent>();
        playerAnim = _gameState.player.GetComponent<Animator>();

        _gameEvent.enemyHitPlayer += DamagedByEnemy;

        Init();
    }

    void Init()
    {
        Vector3 basePos = new Vector3(0, 0, 0);
        _gameState.player.transform.position = basePos;
        _playerComp.hpBar.maxValue = _playerComp.hp;
        _playerComp.hpBar.value = _playerComp.hp;
    }

    public void OnUpdate()
    {
        if (_gameState.gameStatus != GameStatus.IsPlaying) return;
        _playerComp.coolTimer += Time.deltaTime;
        mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        mousePos.z = 0;
        direction = (mousePos - _playerComp.transform.position).normalized;
        TrackCursor();
        moving = MovePlayer();

        controlAnim(direction);
    }

    // WASDで移動する
    bool MovePlayer()
    {
        float hor=0;
        float ver=0;
        _pos = _playerComp.transform.position;
        if (Input.GetKey(KeyCode.W))
        {
            ver = _playerComp.speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            ver = -_playerComp.speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            hor = _playerComp.speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            hor = -_playerComp.speed * Time.deltaTime;
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

        if (distance != Vector3.zero)
        {
            _playerComp.transform.position = _pos;
            return true;
        }
        return false;
    }

    // マウスカーソルの方向を向く
    // void TrackCursor()
    // {
    //     Vector3 pos = Camera.main.WorldToScreenPoint (_gameState.player.transform.localPosition);
	// 	Quaternion rotation = Quaternion.LookRotation(Vector3.forward, Input.mousePosition - pos);
	// 	_playerComp.emptyObj.transform.rotation = rotation;
    // }

    void TrackCursor()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
		_playerComp.emptyObj.transform.rotation = Quaternion.Euler(0, 0, angle);
    }


    void DamagedByEnemy(GameObject enemy)
    {
        float hp = _playerComp.hp;
        float attack = enemy.GetComponent<EnemyComponent>().attack;
        if (hp <= attack)
        {
            hp = 0;
        }
        else
        {
            hp -= attack;
        }
        _playerComp.hpBar.value = hp;
        _playerComp.hp = hp;
    }

    void controlAnim(Vector2 direction)
    {
        foreach (string trig in triggers)
        {
            playerAnim.ResetTrigger(trig);
        }

        if (direction.y >= direction.x && direction.y >= -direction.x) //up
        {
            trigger = moving ? "moveUp" : "isUp";
        }
        else if (direction.y <= direction.x && direction.y <= -direction.x) //down
        {
            trigger = moving ? "moveDown" : "isDown";
        }
        else if (direction.y < direction.x && direction.y > -direction.x) //right
        {
            trigger = moving ? "moveRight" : "isRight";
        }
        else if (direction.y > direction.x && direction.y < -direction.x) //left
        {
            trigger = moving ? "moveLeft" : "isLeft";
        }
        else trigger = "isDown";
        playerAnim.SetTrigger(trigger);
    }
}
