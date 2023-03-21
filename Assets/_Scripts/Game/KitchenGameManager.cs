using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance { get; private set; }

    public event EventHandler<EventArgs> OnStateChange;

    [SerializeField] private float gamePlayingTimerMax = 20f;

    private float gamePlayingTimer;

    private float waitingToStateTimer = 1f;

    private float countDownToStateTimer = 3f;
    public enum State
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver,
    }

    private State state;
    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }
    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStateTimer -= Time.deltaTime;
                if (waitingToStateTimer < 0f)
                {
                    state = State.CountDownToStart;
                    OnStateChange?.Invoke(this, new EventArgs());
                }
                break;
            case State.CountDownToStart:
                countDownToStateTimer -= Time.deltaTime;
                if (countDownToStateTimer < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChange?.Invoke(this, new EventArgs());
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChange?.Invoke(this, new EventArgs());
                }
                break;
            case State.GameOver:
                break;
        }
        Debug.Log(state);
    }
    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }
    public bool IsGameOver()
    {
        return state == State.GameOver;
    }
    public bool IsCountDownToStart()
    {
        return state == State.CountDownToStart;
    }
    public float GetCountDownStateTimer()
    {
        return countDownToStateTimer;
    }
    public float GetPlayingTimeNormalized()
    {
        return 1 - gamePlayingTimer / gamePlayingTimerMax;
    }
}
