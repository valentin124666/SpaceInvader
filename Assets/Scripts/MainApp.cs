using System;
using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using Managers.Interfaces;
using Settings;
using UnityEngine;

public class MainApp : MonoBehaviour
{
    public event Action LateUpdateEvent;
    public event Action FixedUpdateEvent;

    private static MainApp _Instance;

    public static MainApp Instance
    {
        get { return _Instance; }
        private set { _Instance = value; }
    }

    [SerializeField] private LevelData _levelData;

    public LevelData LevelData
    {
        get { return _levelData; }
    }

    [SerializeField] private GameData _gameData;

    public GameData GameData
    {
        get { return _gameData; }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (Instance == this)
        {
            ResourceLoading().Forget();
        }
    }

    private async UniTask ResourceLoading()
    {
        await ResourceLoader.Init();

        await GameClient.Instance.InitServices();

        GameClient.Get<IGameplayManager>().ChangeAppState(Enumerators.AppState.AppStart);
    }

    void Update()
    {
        if (Instance == this)
        {
            GameClient.Instance.Update();
        }
    }

    private void LateUpdate()
    {
        if (Instance == this)
        {
            LateUpdateEvent?.Invoke();
        }
    }

    private void FixedUpdate()
    {
        if (Instance == this)
        {
            FixedUpdateEvent?.Invoke();
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            GameClient.Instance.Dispose();
        }
    }
}