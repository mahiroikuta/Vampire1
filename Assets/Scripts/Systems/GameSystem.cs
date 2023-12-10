using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem
{
    private GameState _gameState;
    private GameEvent _gameEvent;

    public GameSystem(GameState gameState, GameEvent gameEvent)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;

        _gameEvent.startGame += Init;
        _gameEvent.resetGame += ResetGame;
        _gameState.timeText.gameObject.SetActive(false);
    }

    private void Init()
    {
        _gameState.timeText.gameObject.SetActive(true);
    }

    public void OnUpdate()
    {
        if ( _gameState.gameStatus == GameStatus.IsPlaying) CountTime(Time.deltaTime);
    }


    private void CountTime(float time)
    {
        _gameState.timer += time;
        SetTime(_gameState.timer);
    }

    private void SetTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        string timeText = string.Format("{0:00}:{1:00}", minutes, seconds);
        _gameState.timeText.SetText(timeText);
    }

    private void ResetGame()
    {
        SetTime(0);
        MonoBehaviour.Destroy(_gameState.player);
        foreach(EnemyComponent enemy in _gameState.enemies) enemy.gameObject.SetActive(false);
        _gameState.enemies.Clear();
        foreach(BulletComponent bullet in _gameState.bullets) bullet.gameObject.SetActive(false);
        _gameState.bullets.Clear();
        foreach(DamageTextComponent damageText in _gameState.damageTexts) damageText.gameObject.SetActive(false);
        _gameState.damageTexts.Clear();
    }
}
