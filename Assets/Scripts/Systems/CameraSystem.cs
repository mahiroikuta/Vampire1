using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem
{
    GameState _gameState;
    GameEvent _gameEvent;

    PlayerComponent _player;
    public Vector3 _pos;

    public CameraSystem(GameState gameState, GameEvent gameEvent)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;

        _player = gameState.player.GetComponent<PlayerComponent>();

        Init();
    }

    void Init()
    {
        Vector3 basePos = new Vector3(0, 0, -10f);
        _gameState.camera.transform.position = basePos;
    }

    public void OnUpdate()
    {
        MoveCamera();
    }

    // WASDで移動する
    void MoveCamera()
    {
        float hor=0;
        float ver=0;
        _pos = _gameState.camera.transform.position;
        if (Input.GetKey(KeyCode.W))
        {
            ver = _player.speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            ver = -_player.speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            hor = _player.speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            hor = -_player.speed * Time.deltaTime;
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
        _gameState.camera.transform.position = _pos;
    }
}
