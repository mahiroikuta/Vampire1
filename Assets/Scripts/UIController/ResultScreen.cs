using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : BaseScreen
{
    GameState _gameState;
    GameEvent _gameEvent;
    [SerializeField] private Button titleButton;
    public override void Init(GameState gameState, GameEvent gameEvent)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;

        _gameEvent.showResult += OnShow;
        _gameEvent.showTitle += OnHide;

        titleButton.onClick.AddListener(ShowTitleScreen);
    }

    private void ShowTitleScreen()
    {
        _gameEvent.resetGame?.Invoke();
        _gameEvent.showTitle?.Invoke();
    }

    public override void OnShow()
    {
        _gameState.gameStatus = GameStatus.Result;
        this.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
