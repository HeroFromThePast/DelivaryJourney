using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReproducirTutorial : MonoBehaviour
{
    public int tut;
    /*private void Awake()
    {
        tut = PlayerPrefs.GetInt("TutorialHabilitado");
        if (tut > 0)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);

        }
    }*/
    public void PlayTutorial()
    {
        SceneManager.LoadScene("VideoTEst");
    }
}
