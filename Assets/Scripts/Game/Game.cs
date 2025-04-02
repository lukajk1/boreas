using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    private static bool isPaused;
    public static bool IsPaused 
    { 
        get
        {
            return isPaused;
        }
        private set
        {
            if (value != isPaused)
            {
                isPaused = value;
                PauseUpdated?.Invoke(value);

                if (value)
                {
                    Time.timeScale = 0;
                    CursorLocked = false;
                }
                else
                {
                    Time.timeScale = 1;
                    CursorLocked = true;
                }
            }
        }
    }
    private static bool isInDialogue;
    public static bool IsInDialogue { get; set; }

    private static int _menusOpen;
    public static int MenusOpen
    {
        get => _menusOpen;
        set
        {
            _menusOpen = value;
            if (_menusOpen == 0)
            {
                IsPaused = false;
            }
            else
            {
                IsPaused = true;
            }
        }
    }

    private static bool _cursorLocked = false;
    public static bool CursorLocked
    {
        get => _cursorLocked;
        set
        {
            if (_cursorLocked != value)
            {
                _cursorLocked = value;
            }

            if (_cursorLocked) Cursor.lockState = CursorLockMode.Locked;
            else Cursor.lockState = CursorLockMode.None;
        }
    }

    public Transform PlayerTransform;
    public Camera PlayerCamera;
    public PlayerUnit PlayerUnitInstance;

    public static Action<bool> PauseUpdated;
    public static Action InitializeRun;
    public static Game I { get; private set; }

    private void Awake()
    {
        if (I != null) Debug.LogError($"More than one instance of {I} in scene");
        I = this;
    }

    private void Start()
    {
        InitializeRun?.Invoke();
        CursorLocked = true;
    }
}
