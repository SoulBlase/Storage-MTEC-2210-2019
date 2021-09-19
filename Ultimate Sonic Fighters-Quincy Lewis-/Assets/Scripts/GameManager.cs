using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //This is used to make GameManager a singleton to have it be easily accessible from all other scripts
    //This is doable because I know in advance there should only ever be a single GameManager in the scene
    public static GameManager instance;

    

    Text[] gameTexts;
    Text PauseText;
    //Image HealthBar;
    //Image HealthBar2;


    bool KO;

    GameObject[] pauseObjects;

    int p1RoundsWon;
    int p2RoundsWon;

    public Image LifeBar;
    int Health;
    int MaxHealth = 50;

    public GameObject p1;
    public GameObject p2;

    public GameObject winner;
    public string SceneIndex;

    public HealthBarScript HLT;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);

        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            //This code just makes sure the GameManager in the each scene is the same instance. 
        }

        /*var canvas = GameObject.Find("Canvas");
        HealthBar = canvas.GetComponentInChildren<Image>();
        HealthBar2 = canvas.GetComponentInChildren<Image>();*/

    }

    // In your playerscript, you can call OnRoundEnd when the player health reaches 0 
    // and pass it the other player GameObject (ie the winning plauyer). 
    // That probably means each player should have a reference to the other player GameObject;
    public void OnRoundEnd(GameObject player)
    {
        if (Health <= 0)
        {
            if (player.name == "Player1")
            {
                if (p1RoundsWon == 0)
                {
                    p1RoundsWon++;
                    SceneManager.LoadScene("Sonic's VictoryScene", LoadSceneMode.Single);
                }
                else
                {
                    ChangeToEndScene(player);

                }
            }
            else if (player.name == "Player2")
            {
                if (p2RoundsWon == 0)
                {
                    p2RoundsWon++;
                    SceneManager.LoadScene("Knuckles' VictoryScene", LoadSceneMode.Single);
                }
                else
                {
                    ChangeToEndScene(player);


                }

            }

            
        }
    }
    void OnTriggerEnter(Collider Colide)
    {
        if(Colide.gameObject.tag == "Player 1")
        {
            HLT.DamageTaken(5);
            Destroy(Colide.gameObject);
        }
        if(Colide.gameObject.tag == "Player 2")
        {
            HLT.DamageTaken(5);
            Destroy(Colide.gameObject);
        }
    }

    /*private static void NewMethod(GameObject player)
    {
        ChangeToEndScene(player);
    }*/

    void ChangeToEndScene(GameObject player)
    {

        winner = player;
        SceneManager.LoadScene(SceneIndex);
        //This line of code will change scenes to the scene index number. The way to see your scene 
        //index number is to open each scene you have in the game. And when each scene is open, go to 
        //Build Settings and find a button that says add open scene to build. Once all the scenes are 
        //added, on the right of the list will be index numbers. these are the scene indexes which you
        //can use to load the different scenes.

    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        hidePaused();
    }

    // Update is called once per frame
    void Update()
    {
        //Uses the Backspace button to pause and unpause the game
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            if(Time.timeScale == 1)
            {
                Time.timeScale = 0;
                showPaused();
            }
            else if(Time.timeScale == 0)
            {
                Debug.Log("High");
                Time.timeScale = 1;
                hidePaused();
            }
        }

        

    }

    //Reloads the game
    public void Reload()
    {
        Application.LoadLevel(Application.loadedLevel);

    }

    //Controls the pausing of the game
    public void pauseControl()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPaused();
        }
        else if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePaused();
        }
    }

    //Shows Objects with ShowOnPause Tag
    public void showPaused()
    {
        foreach(GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    //Hides Objects with ShowOnPause Tag
    public void hidePaused()
    {
        foreach(GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    //loads inputted level
    public void LoadLevel(string level)
    {
        Application.LoadLevel(level);
    }

    
}
