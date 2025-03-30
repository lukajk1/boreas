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
                }
                else
                {
                    Time.timeScale = 1;
                }
            }
        }
    }
    private static bool isInDialogue;
    public static bool IsInDialogue { get; set; }

    private static int menusOpen;
    public static int MenusOpen
    {
        get
        {
            return menusOpen;
        }

        set
        {
            menusOpen = value;
            if (menusOpen == 0)
            {
                IsPaused = false;
            }
            else
            {
                IsPaused = true;
            }
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
        if (I != null) 
        {
            Debug.LogError($"More than one instance of {I} in scene");
        }

        I = this;
    }

    private void Start()
    {
        InitializeRun?.Invoke();
    }
}
