using UnityEngine;
using UnityEngine.SceneManagement;

public class ClassSelection : MonoBehaviour
{
    public GameObject[] classes;
    public int selested = 0;

    public void Next() 
    {
        classes[selested].SetActive(false);
        selested++;
        if (selested == classes.Length)
            selested = 0;
        classes[selested].SetActive(true);
    }
    public void Previous() 
    {
        classes[selested].SetActive(false);
        selested--;
        if (selested <0)
            selested = classes.Length -1;
        classes[selested].SetActive(true);
    }
    public void Play() 
    {
        PlayerPrefs.SetInt("playerClass", selested);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
