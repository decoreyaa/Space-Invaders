using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject shipPanel;
    public GameObject quitButton;
    public GameObject shipsButton;
    public GameObject title;
    public GameObject startButton;
    public AudioClip clickSound;
    private AudioSource audioSource;
    public GameObject Volume;
    public TMP_Text highScoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // null-guarded so the menu still runs before the text object is wired up
        if (highScoreText != null)
            highScoreText.text = "High Score: " + PlayerPrefs.GetInt(GameManager.HighScoreKey, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayClick()
    {
        audioSource.PlayOneShot(clickSound);
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
