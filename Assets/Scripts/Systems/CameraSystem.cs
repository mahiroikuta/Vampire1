using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem
{
    GameState _gameState;
    GameEvent _gameEvent;

    public float _pSpeed;
    public Vector3 _pos;

    public CameraSystem(GameState gameState, GameEvent gameEvent)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;

        _pSpeed = gameState.player.GetComponent<PlayerComponent>().speed;
        _pos = gameState.camera.GetComponent<CameraComponent>().pos;

        Init();
    }

    public void Init()
    {
        Vector3 basePos = new Vector3(0, 0, -10f);
        _pos = basePos;
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
        if (Input.GetKey(KeyCode.W))
        {
            ver = _pSpeed * Time.deltaTime * 5;
        }
        if (Input.GetKey(KeyCode.S))
        {
            ver = -_pSpeed * Time.deltaTime * 5;
        }
        if (Input.GetKey(KeyCode.D))
        {
            hor = _pSpeed * Time.deltaTime * 5;
        }
        if (Input.GetKey(KeyCode.A))
        {
            hor = -_pSpeed * Time.deltaTime * 5;
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
        _gameState.camera.transform.position = _pos;
    }
}
