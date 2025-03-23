using UnityEngine;
using UnityEngine.UI;

public class EscapeMenu : MonoBehaviour
{

    [SerializeField] private GameObject escPage;
    [SerializeField] private Button backToGame;
    [SerializeField] private Button quit;

    private bool isOpen;

    private void Start()
    {
        backToGame.onClick.AddListener(() => SetEscMenu(false));
        quit.onClick.AddListener(() => Application.Quit());
        escPage.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked; // this needs to be moved somewhere more central eventually 
    }

    private void Update()
    {
        // no reason to tie this to actions system, best if esc isn't rebindable.
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            SetEscMenu(!isOpen);
        }
    }

    private void SetEscMenu(bool value)
    {
        escPage.SetActive(value);
        isOpen = value;

        if (value)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            AudioListener.pause = true;
            Game.MenusOpen++;
        }
        else
        {
            AudioListener.pause = false;
            Cursor.lockState = CursorLockMode.Locked;
            Game.MenusOpen--;
        }
    }
}
