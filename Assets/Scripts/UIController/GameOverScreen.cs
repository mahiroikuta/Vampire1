using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : BaseScreen
{
    GameState _gameState;
    GameEvent _gameEvent;
    [SerializeField] private Button resultButton;
    public override void Init(GameState gameState, GameEvent gameEvent)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;

        _gameEvent.gameOver += OnShow;
        _gameEvent.showResult += OnHide;

        resultButton.onClick.AddListener(ShowResultScreen);
    }
    
    private void ShowResultScreen()
    {
        _gameEvent.showResult?.Invoke();
    }
    public override void OnShow()
    {
        _gameState.gameStatus = GameStatus.GameOver;
        this.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
