using UnityEngine;

public class PlayerAppearance : MonoBehaviour
{
    public Sprite[] shipSprites;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int index = PlayerPrefs.GetInt("SelectedShip", 0);
        index =  Mathf.Clamp(index, 0, shipSprites.Length -1);
        GetComponent<SpriteRenderer>().sprite = shipSprites[index];   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
