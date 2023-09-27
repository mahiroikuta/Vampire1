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
