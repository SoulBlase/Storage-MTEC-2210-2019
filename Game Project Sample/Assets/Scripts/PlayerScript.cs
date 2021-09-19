// This is the library this script uses
using UnityEngine;

public class PlayerScript : MonoBehaviour{

    // The Player Variables. In Unity, making variables Public exposes them to the Inspector  
    // This Allows you to change them from the Unity Editor. 
    // If a variable is Public, the initialzed value is taken from the Inspector, not from the code.
    [Header("PLAYER SETTINGS")]
    public int playerhealth = 4;
    public float moveSpeed = 6;
    public float bulletSpeed = 600;
    public int maxBulletsOnScreen = 8;
    public float autofireDelay = 0.1f;

    // The Camera shake Variables related to the player. 
    [Header("CAMERA SETTINGS")]
    public float hitShakeDuration = 0.3f;
    public float deathShakeDuration = 1f;

    bool autofire = true;
    bool hitTaken;
    bool ready;

    Rigidbody2D rb;

    Vector2 defaultPos;
    Vector2 velocity;

    float t;
    float f;
    float inputDelayFactor;

    int maxPlayerHealth;

    float flashDuration = 0.1f;


    GameObject bulletPrefab;
    Transform bulletHolder;

    SpriteRenderer sr;
    ParticleSystem psHit;


    // Start is always called once at the start of the game, or when the object containing this script first becomes active. 
    private void Start() {

        //In Unity, when you put a Prefab in a folder called "Assets/Resources", you can then use a Resources.Load method that loads the prefab at runtime.
        //This way you don't need to have the prefab in the scene when the game starts. 
        bulletPrefab = Resources.Load<GameObject>("PlayerBullet") as GameObject;


        //A script attached to a GameObject in Unity can access any other component attached to the same GameObject with the... 
        //...Get Component function. Here we're getting a reference to the RigidBody  and Sprite Renderer components on the player.
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        //in the GameManager script, we create an empty GameObject that will serve as the parent for all bullet objects the player shoots.
        // Here we are getting a reference to that GameObject by accessing the instance of the GameManager and accessing the bullet holder transform
        bulletHolder = GameManager.inst.bulletHolder.transform;

        //This gets a reference to the ship damage Particle System
        psHit = GameObject.Find("HitTaken_Player").GetComponent<ParticleSystem>();

        t = autofireDelay;

        defaultPos = transform.position;

        //Since we are exposing most of the player variables to the Inspector, we just need a little code to make sure values that might... 
        //...break the code. For example making sure autoFireDelay is always a positive number or that Player Health is greater than or equal to 1.
        // Mathf is a library of Math functions in C#. Mathf.Max(a,b) checks the two values passed and chooses the larger one. 
        // This way if the value of maxBulletsOnScreen is set to a negative number, the value will default to 0.
        maxBulletsOnScreen = Mathf.Max(0, maxBulletsOnScreen);
        playerhealth = Mathf.Max(playerhealth, 1);
        maxPlayerHealth = playerhealth;

        autofireDelay = Mathf.Abs(autofireDelay);

        hitShakeDuration = Mathf.Max(hitShakeDuration, 0);
        deathShakeDuration = Mathf.Max(deathShakeDuration, 0);

        ready = false;
        inputDelayFactor = 0.5f;
    }

    // In Unity, Update() is a function that runs every frame. 
    private void Update() {

        // Here we check if the game is ready for input before we enable it.
        if (ready) {
            MovePlayer();
        } else {
            DelayInput();
        }

        // Here we have the Fire input. We have the fire input on a basic timer which waits for the duration of the variable autofireDelay.
        // We have a float t which is set to the delay time. 
        if (Input.GetKey(KeyCode.Space)) {
            if (autofire) {
                if (t > 0) {
                    t -= Time.deltaTime;
                } else {
                    Fire();
                    t = autofireDelay;
                }
            }
        }

        // Here we use the same basic timer as above.  
        // this is for flashing the player sprite color when the player takes a hit
        if (hitTaken) {
            if (f > 0) {
                sr.color = Color.red;
                f -= Time.deltaTime;
            } else {
                sr.color = Color.white;
                f = flashDuration;
                hitTaken = false;
            }
        }
    }

    // FixedUpdate is a an Update function that runs at a fixed time Step (Normal update's will vary slightly since it depends on the computer's frametime in milliseconds.
    // In Unity, movement that uses the physics engine should run at a fixed timestep so we put this code in FixedUpdate()  
    private void FixedUpdate() {

        // Move Position is a RigidBody method that moves the object manually while still respecting collision physics.
        // RigidBody2D.MovePosition takes a Vector2, so we have to cast transform.position to a Vector2 (it is a Vectoer3 by default)
        rb.MovePosition((Vector2)transform.position + velocity);
    }

    void MovePlayer() {

        //here were setting the X and Y values of our custom Velocity Vector2. 
        //We are getting the present Unity Axis values for horizontal (x) and vertical (y). A positive X value means its moving right, a negative X means it's moving to the left. 
        velocity.x = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        velocity.y = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
    }

    //When  called, the Fire function checks if the amount of bullets on screen are less then the Max value for that we set.
    // If it is , it creates a bullet at a specfic point and sets its speed to the speed variable value we set in the Inspector
    void Fire() {

        // To get the amount of bullets on screen, we get a reference to the Bullet Holder GameObject that is holding all the bullets see how many bullets there are
        var bulletAmount = bulletHolder.gameObject.GetComponentsInChildren<BulletScript>().Length;

        if (bulletAmount < maxBulletsOnScreen) {

            float firePoint = sr.bounds.size.x / 1.5f;
            Vector2 pos = new Vector2(transform.position.x + firePoint, transform.position.y);
            var bullet = Instantiate(bulletPrefab, pos, Quaternion.identity, bulletHolder);
            bullet.GetComponent<BulletScript>().speed = bulletSpeed;

            //Here we access the SoundManager instance and tell it to play the sound at index 1, at the position the bullet was fired.
            SoundManager.inst.PlaySoundAtPosition(pos, 1);
        }
    }

    //We call this function every time a bullet collides with the player's collision box (See OnTriggerEnter2D below)
    void TakeHit() {
        hitTaken = true;

        if (playerhealth - 1 > 0) {
            playerhealth -= 1;

            //Plays the hit sound
            SoundManager.inst.PlaySoundAtPosition((Vector2)transform.position, 2);

            //Stop the hit particle system and plays it again
            psHit.Stop();
            psHit.Play();

            //Adjust the player's health bar
            GameManager.inst.SetNewFillAmount(playerhealth, maxPlayerHealth);

            //Shake the camera
            CameraShake.inst.shakeDuration = hitShakeDuration;
        } else {

            //if player health <= 0, call the PlayerKilled function
            PlayerKilled();
        }
    }

    public void PlayerKilled() {

        //Set the player health bar amount to 0, trigger the explosion Particle System, destroy the player and trigger the Restart screen
        GameManager.inst.SetNewFillAmount(0, maxPlayerHealth);
        GameManager.inst.TriggerExplosion(transform.position, deathShakeDuration);
        GameManager.inst.GameOver();
        Destroy(gameObject);
    }

    //OnTriggerEnter2D is a Unity method for detecting collisions with Triggers. In this case the bullet box collider is set to a trigger.
    // If the player collides with a trigger that is tagged EnemyBullet, call the TakeHit function
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "EnemyBullet") {
            TakeHit();
        }
    }

    //Sometimes if you are pressing buttons before the game has fully loaded, it can read your input and move the player off the screen before the game even starts
    //Here I have put a small delay timer to prevent this from happening
    void DelayInput() {
        if (inputDelayFactor > 0) {
            inputDelayFactor -= Time.deltaTime;
        } else {
            ready = true;
        }

    }
}
