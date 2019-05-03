using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    private static GameObject _activePanel;
    public Camera mainCamera;

    private void Start()
    {
        Animator cameraAnimator = mainCamera.GetComponent<Animator>();
        CheckResolution(cameraAnimator);
    }

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

    private void CheckResolution(Animator animator)
    {
        if (Screen.width < 1280)
        {
            animator.SetBool("ResolutionLow", true);
        }
        else if(Screen.width>=1280 && Screen.width < 1920)
        {
            animator.SetBool("ResolutionMid", true);
        }
        else
        {
            animator.SetBool("ResolutionHigh",true);
        }
    }
}
