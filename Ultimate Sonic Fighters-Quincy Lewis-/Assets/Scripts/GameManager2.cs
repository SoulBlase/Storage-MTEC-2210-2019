using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour {

	/*public static GameManager instance;

    int p1RoundsWon;
    int p2RoundsWon;

    public GameObject p1;
    public GameObject p2;

    public static GameObject winner;

    void Awake(){
    	if(instance != null){
    		Destroy(gameObject)
    	} else{
    		instance = this;
    		DontDestroyLonLoad(gameObject);
    		//This code just makes sure the GameManager in the each scene is the same instance. 
    	}

    }


    // In your playerscript, you can call OnRoundEnd when the player health reaches 0 
    // and pass it the other player GameObject (ie the winning plauyer). 
    // That probably means each player should have a reference to the other player GameObject;
    public void OnRoundEnd(GameObject player){

    	if(player.name == "Player1"){
    		if (p1RoundsWon == 0){
    			p1RoundsWon++;
    		} else{
    			ChangeToEndScene(player)

    		}
    	} else if (player.name == "Player2"){
    		if (p2RoundsWon == 0){
    			p2RoundsWon++;
    		} else{
    			ChangeToEndScene(player)

    		}

    	}
    }

    void ChangeToEndScene(GameObject player){

   		winner = player;
   		SceneManager.LoadScene(SceneIndex);
   		//This line of code will change scenes to the scene index number. The way to see your scene 
   		//index number is to open each scene you have in the game. And when each scene is open, go to 
   		//Build Settings and find a button that says add open scene to build. Once all the scenes are 
   		//added, on the right of the list will be index numbers. these are the scene indexes which you
   		 //can use to load the different scenes.

    }*/



}