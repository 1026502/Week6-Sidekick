using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public MenuManager menuManager;
    private void Start()
    {
        menuManager = GetComponent<MenuManager>();
    }

    private void OnMouseUpAsButton()
    {

        Debug.Log("Button has been pressed");
        menuManager.NextSceneChange();
    }
}
