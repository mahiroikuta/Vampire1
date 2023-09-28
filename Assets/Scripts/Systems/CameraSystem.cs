using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem
{
    GameState _gameState;
    GameEvent _gameEvent;

    public CameraSystem(GameState gameState, GameEvent gameEvent)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;

        Init();
    }

    public void Init()
    {
        Vector3 basePos = new Vector3(0, 0, -10f);
        _gameState.camera.transform.position = basePos;
    }

    public void OnUpdate()
    {
        MovePlayer();
    }

    // WASDで移動する
    void MovePlayer()
    {
        float hor = Input.GetAxis("Horizontal")/30;
        float ver = Input.GetAxis("Vertical")/30;
        Vector3 pos = _gameState.camera.transform.position;
        Vector3 newPos = new Vector3(pos.x+hor, pos.y+ver, pos.z);
        if (newPos.x > 32f)
        {
            newPos.x = 32f;
        }
        else if (newPos.x < -32f)
        {
            newPos.x = -32f;
        }

        if (newPos.y > 40f)
        {
            newPos.y = 40f;
        }
        else if (newPos.y < -40f)
        {
            newPos.y = -40f;
        }
        _gameState.camera.transform.position = newPos;
    }
}
