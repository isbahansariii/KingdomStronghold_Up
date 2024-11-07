using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class mainMenu : MonoBehaviour
{
    public GameObject Main_Menu;
    public GameObject instructions; //user manual
    //public GameObject options;


    // Start is called before the first frame update
    void Start()
    {
        Main_Menu.SetActive(true);
        instructions.SetActive(false);
        //options.SetActive(false);
    }

    public void instructionBtnClicked()
    {
        Main_Menu.SetActive(false);
        instructions.SetActive(true);
    }

    public void insCrossBtnClicked()
    {
        Main_Menu.SetActive(true);
        instructions.SetActive(false);
    }

    public void playBtnClicked()
    {
        SceneManager.LoadScene(1);
    }

    
}
