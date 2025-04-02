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
    }

    private void Update()
    {
        // no reason to tie this to actions system, best if esc isn't rebindable.
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            SetEscMenu(!isOpen);
        }
    }
    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            if (!isOpen) SetEscMenu(isOpen);
        }
        else
        {
            // regained focus
        }
    }

    private void SetEscMenu(bool value)
    {
        escPage.SetActive(value);
        isOpen = value;

        if (value)
        {
            Cursor.visible = true;
            AudioListener.pause = true;
            Game.MenusOpen++;
        }
        else
        {
            AudioListener.pause = false;
            Game.MenusOpen--;
        }
    }
}
