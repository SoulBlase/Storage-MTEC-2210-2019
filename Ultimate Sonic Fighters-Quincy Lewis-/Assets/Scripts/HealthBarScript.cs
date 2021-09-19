using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class HealthBarScript : MonoBehaviour
{
    /*[Header("UI Settings")]
    public Gradient HealthBarGradient;
    public Gradient HealthBar2Gradient;
    public Color TextColor;*/
    
    Image HealthBar;
    Image HealthBar2;

    public float TargetAmount;

    public Slider LifeBar;
    int Health;
    int MaxHealth = 50;

    public int SonicHealth;
    public int KnucklesHealth;

    /*public void Awake()
    {
        var canvas = GameObject.Find("Canvas");
        HealthBar = canvas.GetComponentInChildren<Image>();
        HealthBar2 = canvas.GetComponentInChildren<Image>();
    }*/

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        /*float s = 10;
        HealthBar.fillAmount = Mathf.Lerp(HealthBar.fillAmount, TargetAmount, s * Time.deltaTime);
        HealthBar.color = HealthBarGradient.Evaluate(HealthBar.fillAmount);

        HealthBar2.fillAmount = Mathf.Lerp(HealthBar2.fillAmount, TargetAmount, s * Time.deltaTime);
        HealthBar2.color = HealthBar2Gradient.Evaluate(HealthBar2.fillAmount);*/

        LifeBar.value = Health;
        SonicHealth = 50;
        KnucklesHealth = 50;

        /*if(Health <= 0)
        {

            SceneManager.LoadScene("Sonic's VictoryScene", LoadSceneMode.Single);
        }*/
    }

    public void takeDamageKnuckles()
    {
        KnucklesHealth -= 5;
    }

    public void takeDamageSonic()
    {
        SonicHealth -= 5;
    }

    public void DamageTaken(int Damage)
    {
        Health -= Damage;
    }
}
