using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhanceSystem
{
    private GameState _gameState;
    private GameEvent _gameEvent;

    public EnhanceSystem(GameState gameState, GameEvent gameEvent)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;

        _gameEvent.levelUp += levelUpAction;
        _gameEvent.selectEnhance += endEnhanceFaze;
    }

    public void OnUpdate()
    {
    }

    private void levelUpAction()
    {
        Time.timeScale = 0;
        _gameEvent.showLevelUpScreen?.Invoke();
    }

    private void endEnhanceFaze()
    {
        Time.timeScale = 1;
        _gameEvent.hideLevelUpScreen?.Invoke();
    }
}
