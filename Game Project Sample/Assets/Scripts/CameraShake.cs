// This is the library this script uses
using UnityEngine;

public class CameraShake : MonoBehaviour {

    //This is used to make CameraShake a singleton to have it be easily accessible from all other scripts
    //This is doable because I know in advance there should only ever be a single CameraShake in the scene
    public static CameraShake inst;

    [HideInInspector]
    public float shakeDuration;

    [HideInInspector]
    public float shakeAmount;
    readonly float decreaseFactor = 1.0f;

    Vector3 originalPos;

    // Awake runs before Start() so use it to run code that initlaizes variables or sets game states before the game Starts.
    // Most of the time you can run things in start, but sometimes it is important that the code is run before the Start method runs.
    private void Awake() {
        //Here we're setting the CameraShake singleton to this instance of the class. And making sure there isn't another one
        //If there is we destroy it.
        if (inst != null) {
            Destroy(gameObject);
        } else {
            inst = this;
        }
    }

    // Start is always called once at the start of the game, or when the object containing this script first becomes active. 
    void Start() {
        originalPos = transform.localPosition;
    }

    // In Unity, Update() is a function that runs every frame. 
    void Update() {
        if (shakeDuration > 0) {
            // this adds a random position within a sphere around the camera's osition multiped by a shakeAmount factor every frame
            // This creates a randomized camera shake for the duration set
            transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            //This decreases the shake duration gradually 
            shakeDuration -= Time.deltaTime * decreaseFactor;
        } else {
            //When the shake duration is 0, return camera to its initial position
            shakeDuration = 0f;
            transform.localPosition = originalPos;
        }
    }
}