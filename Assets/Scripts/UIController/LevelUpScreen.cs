using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpScreen : BaseScreen
{
    GameState _gameState;
    GameEvent _gameEvent;
    [SerializeField] private Button coolTimeButton;
    [SerializeField] private Button damageUpButton;
    [SerializeField] private Button bulletSpeedButton;
    PlayerComponent playerComponent;
    public override void Init(GameState gameState, GameEvent gameEvent)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;

        coolTimeButton.onClick.AddListener(EnhanceCoolTime);
        damageUpButton.onClick.AddListener(EnhanceDamageUp);
        bulletSpeedButton.onClick.AddListener(EnhanceBulletSpeed);

        _gameEvent.showLevelUp += OnShow;
        _gameEvent.backGame += OnHide;
        _gameEvent.startGame += GameInit;
        OnHide();
    }

    private void GameInit()
    {
        playerComponent = _gameState.player.GetComponent<PlayerComponent>();
    }

    private void EnhanceCoolTime()
    {
        playerComponent.coolTimeLevel++;
        _gameEvent.backGame?.Invoke();
    }

    private void EnhanceDamageUp()
    {
        playerComponent.damageUpLevel++;
        _gameEvent.backGame?.Invoke();
    }

    private void EnhanceBulletSpeed()
    {
        playerComponent.bulletSpeedLevel++;
        _gameEvent.backGame?.Invoke();
    }
}
