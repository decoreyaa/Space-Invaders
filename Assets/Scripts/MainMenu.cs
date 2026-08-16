using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject shipPanel;
    public GameObject quitButton;
    public GameObject shipsButton;
    public GameObject title;
    public GameObject startButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SelectShip(int index)
    {
        PlayerPrefs.SetInt("SelectedShip", index);
        PlayerPrefs.Save();
    }

    public void OpenShipPanel()
    {
        shipPanel.SetActive(true);
        quitButton.SetActive(false);
        shipsButton.SetActive(false);
        startButton.SetActive(false);
        title.SetActive(false);
    }

    public void CloseShipPanel()
    {
        shipPanel.SetActive(false);
        quitButton.SetActive(true);
        shipsButton.SetActive(true);
        startButton.SetActive(true);
        title.SetActive(true);
    }
}
