using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem
{
    GameState _gameState;
    GameEvent _gameEvent;

    Quaternion _dir;
    public PlayerSystem(GameState gameState, GameEvent gameEvent)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;

        _dir = gameState.player.GetComponent<PlayerComponent>().dir;
    }

    public void Init()
    {
        Vector3 basePos = new Vector3(0, 0, 0);
        _gameState.player.transform.position = basePos;
    }

    public void OnUpdate()
    {
        MovePlayer();
        TrackCursor();
    }

    // WASDで移動する
    void MovePlayer()
    {
        float hor = Input.GetAxis("Horizontal")/30;
        float ver = Input.GetAxis("Vertical")/30;
        Vector3 pos = _gameState.player.transform.position;
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
        _gameState.player.transform.position = newPos;
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
