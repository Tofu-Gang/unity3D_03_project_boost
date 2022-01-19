using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float crashDelay = 1f;
    [SerializeField] float successDelay = 1f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;
    
    AudioSource my_audiosource;
    Movement movementScript;

    bool isTransitioning = false;

    // Start is called before the first frame update
    void Start()
    {
        my_audiosource = GetComponent<AudioSource>();
        movementScript = GetComponent<Movement>();
    }
    
    void OnCollisionEnter(Collision other) {
        if (!isTransitioning)
        {
            switch(other.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("This is friendly");
                    break;
                case "Finish":
                    SuccessSequence();
                    break;
                default:
                    CrashSequence();
                    break;
            }
        }
    }

    void CrashSequence()
    {
        // TODO: add particle effects upon crash
        isTransitioning = true;
        my_audiosource.Stop();
        my_audiosource.PlayOneShot(crash);
        movementScript.enabled = false;
        Invoke("ReloadLevel", crashDelay);
    }

    void SuccessSequence()
    {
        // TODO: add particle effects upon success
        isTransitioning = true;
        my_audiosource.Stop();
        my_audiosource.PlayOneShot(success);
        movementScript.enabled = false;
        Invoke("LoadNextLevel", successDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        isTransitioning = false;
    }

    void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
        isTransitioning = false;
    }
}
