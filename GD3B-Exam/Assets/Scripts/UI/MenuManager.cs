using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> navItems;
    private int _index = 0;

    private MenuInputActions _menuInputActions;

    private void Start()
    {
        InitMenuActions();
        _index = 0;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.firstSelectedGameObject = navItems[_index];
        EventSystem.current.SetSelectedGameObject(navItems[_index]);
    }

    private void Up(InputAction.CallbackContext context)
    {
        _index--;
        if (_index < 0) _index = navItems.Count-1;
        if (_index >= navItems.Count) _index = 0;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.firstSelectedGameObject = navItems[_index];
        EventSystem.current.SetSelectedGameObject(navItems[_index]);
    }

    private void Down(InputAction.CallbackContext context)
    {
        _index++;
        if (_index < 0) _index = navItems.Count-1;
        if (_index >= navItems.Count) _index = 0;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.firstSelectedGameObject = navItems[_index];
        EventSystem.current.SetSelectedGameObject(navItems[_index]);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void InitMenuActions()
    {
        _menuInputActions = FindObjectOfType<MenuInit>().MenuInputs;
        AssignBindings();
        _menuInputActions.UI.Enable();
    }

    private void AssignBindings()
    {
        _menuInputActions.UI.Down.performed += Down;
        _menuInputActions.UI.Up.performed += Up;
    }
}
