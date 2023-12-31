using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSystem
{
    private GameState _gameState;
    private GameEvent _gameEvent;

    private PlayerComponent _playerComp;
    private Animator playerAnim;
    private Vector3 _pos;
    private Vector3 mousePos;
    private Vector2 direction;
    private bool moving = false;
    private string trigger;
    private string[] triggers = {"isUp", "isDown", "isRight", "isLeft", "moveUp", "moveDown", "moveRight", "moveLeft"};
    private Vector3 basePos = new Vector3(0, 0, 0);
    public PlayerSystem(GameState gameState, GameEvent gameEvent)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;

        _gameEvent.enemyHitPlayer += DamagedByEnemy;
        _gameEvent.startGame += Init;
        _gameEvent.updateXp += UpdateXp;
        _gameEvent.resetGame += ResetXp;
    }

    private void Init()
    {
        _gameState.player = GameObject.Instantiate(_gameState.playerPrefab, basePos, Quaternion.identity);
        _playerComp = _gameState.player.GetComponent<PlayerComponent>();
        playerAnim = _gameState.player.GetComponent<Animator>();

        _playerComp.hpBar.maxValue = _playerComp.maxHp;
        _playerComp.hpBar.value = _playerComp.hp;
        _playerComp.xpBar.maxValue = _playerComp.maxXp;
        _playerComp.xpBar.value = _playerComp.xp;
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

        ControlAnim(direction);
    }

    // WASDで移動する
    private bool MovePlayer()
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

    private void TrackCursor()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
		_playerComp.emptyObj.transform.rotation = Quaternion.Euler(0, 0, angle);
    }


    private void DamagedByEnemy(GameObject enemy)
    {
        float hp = _playerComp.hp;
        float attack = enemy.GetComponent<EnemyComponent>().attack;
        SetHp(attack);
        if (_playerComp.hp <= 0)
        {
            SetHp(0);
            _gameEvent.gameOver?.Invoke();
        }
    }

    private void ControlAnim(Vector2 direction)
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
    
    private void UpdateXp(float dropXp)
    {
        SetXp(dropXp);
        if (_playerComp.xp >= _playerComp.maxXp)
        {
            ResetXp();
            _playerComp.level++;
            _playerComp.maxXp = _gameState.baseXp * _playerComp.level;
            _playerComp.xpBar.maxValue = _gameState.baseXp * _playerComp.level;
            _gameEvent.showLevelUp?.Invoke();
        }
    }

    private void SetXp(float xp)
    {
        _playerComp.xp += xp;
        _playerComp.xpBar.value = _playerComp.xp;
    }

    private void ResetXp()
    {
        _playerComp.xp = 0;
        _playerComp.xpBar.value = _playerComp.xp;
    }

    private void SetHp(float attack)
    {
        _playerComp.hp -= attack;
        _playerComp.hpBar.value = _playerComp.hp;
    }
}
