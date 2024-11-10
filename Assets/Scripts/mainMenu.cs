/*
It handles the behavior of a main menu, allowing the player to:
    - View instructions     OR
    - start the game
*/

using UnityEngine;
using UnityEngine.SceneManagement;


public class mainMenu : MonoBehaviour
{
    public GameObject Main_Menu;
    public GameObject instructions; //user manual


    // Start is called before the first frame update
    void Start()
    {
        Main_Menu.SetActive(true);
        instructions.SetActive(false);
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
