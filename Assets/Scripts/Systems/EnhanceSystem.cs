using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhanceSystem
{
    GameState _gameState;
    GameEvent _gameEvent;
    PlayerComponent _playerComponent;

    public EnhanceSystem(GameState gameState, GameEvent gameEvent)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;
    }

    public void OnUpdate()
    {
        if (_gameState.gameStatus != GameStatus.LevelUp) return;
        levelUpAction();
    }

    void levelUpAction()
    {
        Time.timeScale = 0;
        _gameEvent.showLevelUpScreen?.Invoke();
    }

    void endEnhanceFaze()
    {
        Time.timeScale = 1;
    }
}
