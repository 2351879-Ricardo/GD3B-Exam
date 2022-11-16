using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameOverNav : MonoBehaviour
{
    [SerializeField] private List<GameObject> navItems;
    private int _index = 0;
    
    public void OnUp()
    {
        _index--;
        if (_index < 0) _index = navItems.Count-1;
        if (_index >= navItems.Count) _index = 0;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.firstSelectedGameObject = navItems[_index];
        EventSystem.current.SetSelectedGameObject(navItems[_index]);
    }

    public void OnDown()
    {
        _index++;
        if (_index < 0) _index = navItems.Count-1;
        if (_index >= navItems.Count) _index = 0;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.firstSelectedGameObject = navItems[_index];
        EventSystem.current.SetSelectedGameObject(navItems[_index]);
    }
    
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
