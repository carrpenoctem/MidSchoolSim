using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    private static GameObject _activePanel;

    public void ActivatePanel(GameObject _newPanel)
    {
        if(_activePanel == null)
        {
            _activePanel = _newPanel;
            _activePanel.SetActive(true);
            _activePanel.GetComponent<Animator>().Play("MainMenu_SidePane");
        }
        else if(_activePanel == _newPanel)
        {
            _activePanel.GetComponent<Animator>().Play("MainMenu_SidePaneClose");
            _activePanel = null;
        }
        else
        {
            _activePanel.GetComponent<Animator>().Play("MainMenu_SidePaneClose");
            _activePanel = _newPanel;
            _activePanel.SetActive(true);
            _activePanel.GetComponent<Animator>().Play("MainMenu_SidePane");
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
