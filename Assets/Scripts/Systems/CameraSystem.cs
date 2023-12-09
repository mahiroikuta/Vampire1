using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem
{
    private GameState _gameState;
    private GameEvent _gameEvent;

    private PlayerComponent _player;
    public Vector3 _pos;

    public CameraSystem(GameState gameState, GameEvent gameEvent)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;

        _gameEvent.startGame += Init;
        _gameState.timeText.gameObject.SetActive(false);
    }

    private void Init()
    {
        Vector3 basePos = new Vector3(0, 0, -10f);
        _gameState.camera.transform.position = basePos;
        _player = _gameState.player.GetComponent<PlayerComponent>();
        _gameState.timeText.gameObject.SetActive(true);
    }

    public void OnUpdate()
    {
        if (_gameState.gameStatus != GameStatus.IsPlaying) return;
        MoveCamera();
        CountTime();
    }

    // WASDで移動する
    private void MoveCamera()
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

    private void CountTime()
    {
        _gameState.timer += Time.deltaTime;
        int minutes = Mathf.FloorToInt(_gameState.timer / 60);
        int seconds = Mathf.FloorToInt(_gameState.timer % 60);
        string timeText = string.Format("{0:00}:{1:00}", minutes, seconds);
        _gameState.timeText.SetText(timeText);
    }
}
