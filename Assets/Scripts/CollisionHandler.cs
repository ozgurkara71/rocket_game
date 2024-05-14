using UnityEngine;
// The following namespace should be used for reloading the scene/level
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 1f;
    AudioSource audioSourceOnCollision;
    [SerializeField] AudioClip voiceFinish;
    [SerializeField] AudioClip voiceMissionFailed;

    [SerializeField] ParticleSystem particleFinish;
    [SerializeField] ParticleSystem particleMissionFailed;

    // Program indicates the transection with following variable. While transectioning everything
    // should stop (voice, movement etc. Player control has already disabled).
    bool isTransitioning = false;

    Rigidbody rbToDebugging;
    bool collisionDisable = false;

    void Start()
    {
        audioSourceOnCollision = GetComponent<AudioSource>();

        rbToDebugging = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Debugging();
    }

    void OnCollisionEnter(Collision collision)
    {
        /*
            If player has already entered a collision once (if player has won or lost), 
            there's no need to do anything for subsequent collisions.
            In this game, the sounds are still playing. 
            We set "isTransitioning = true;" after the first collision to seperate subsequent collisions. 
            Once the variable is true, the subsequent collision is understood to occur after winning or losing, 
            and we don't need to process it. return;
         */

        if (isTransitioning || collisionDisable)
            return;
        switch(collision.gameObject.tag) 
        {
            case "Start":
                Debug.Log("Start!");
                break;
            case "Finish":
                LoadNewLevelSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void ReloadLevel()
    {
        // Following code line detects the index of the current scene and loads the current scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadNextLevel()
    {
        // Load next level and if current level is the final level, return to the first level.
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int nextLevel = currentLevel + 1;

        // SceneManager.sceneCountInBuildSettings returns the sum of scenes in game
        if (nextLevel == SceneManager.sceneCountInBuildSettings)
        {
            nextLevel = 0;
        }
        SceneManager.LoadScene(nextLevel);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSourceOnCollision.Stop();
        audioSourceOnCollision.PlayOneShot(voiceMissionFailed);
        particleMissionFailed.Play();

        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayInSeconds);
    }

    void LoadNewLevelSequence()
    {
        isTransitioning = true;
        audioSourceOnCollision.Stop();
        audioSourceOnCollision.PlayOneShot(voiceFinish);
        particleFinish.Play();

        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delayInSeconds);
    }

    void Debugging()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            //toggle collision assigned the opposite value to the variable
            collisionDisable = !collisionDisable;

        }
    }
}
