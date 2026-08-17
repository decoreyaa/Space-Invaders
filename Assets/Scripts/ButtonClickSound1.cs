using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(AudioManager.Instance.PlayClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
