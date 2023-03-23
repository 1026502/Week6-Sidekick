using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject[] menuitems;
    // Start is called before the first frame update
    void Start()
    {
        
    }

   
    public void ChangeSceneAtIndex(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void NextSceneChange()
    {
        int i = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(i+1);
    }

    public void PreviousSceneChange()
    {
        int i = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(i-1);  
    }

    public void OpenMenuItem(int i)
    {
        menuitems[i].gameObject.SetActive(true);
    }
    
    public void CloseMenuItem(int i)
    {
        menuitems[i].gameObject.SetActive(false);
    }
}
