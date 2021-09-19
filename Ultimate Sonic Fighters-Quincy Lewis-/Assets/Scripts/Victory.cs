using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    public string RematchSceneIndex;
    public string MainMenuIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var winnerName = GameManager.instance.winner.name; 
        var winnerSprite = GameManager.instance.winner.GetComponent<SpriteRenderer>().sprite;
        
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }
}
