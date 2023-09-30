using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem
{
    GameState _gameState;
    GameEvent _gameEvent;

    Quaternion _dir;
    float _speed;
    Vector3 _pos;
    public PlayerSystem(GameState gameState, GameEvent gameEvent)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;

        _dir = gameState.player.GetComponent<PlayerComponent>().dir;
        _speed = gameState.player.GetComponent<PlayerComponent>().speed;
        _pos = gameState.player.GetComponent<PlayerComponent>().pos;

        Init();
    }

    public void Init()
    {
        Vector3 basePos = new Vector3(0, 0, 0);
        _pos = basePos;
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
            ver = _speed * Time.deltaTime * 5;
        }
        if (Input.GetKey(KeyCode.S))
        {
            ver = -_speed * Time.deltaTime * 5;
        }
        if (Input.GetKey(KeyCode.D))
        {
            hor = _speed * Time.deltaTime * 5;
        }
        if (Input.GetKey(KeyCode.A))
        {
            hor = -_speed * Time.deltaTime * 5;
        }

        _pos = new Vector3(_pos.x+hor, _pos.y+ver, _pos.z);

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
        _gameState.player.transform.position = _pos;
    }

    // マウスカーソルの方向を向く
    void TrackCursor()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint (_gameState.player.transform.localPosition);
		Quaternion rotation = Quaternion.LookRotation(Vector3.forward, Input.mousePosition - pos );
        _dir = rotation;
		_gameState.player.transform.localRotation = _dir;
    }
}
