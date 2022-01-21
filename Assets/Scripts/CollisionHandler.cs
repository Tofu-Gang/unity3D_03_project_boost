using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float crashDelay = 1f;
    [SerializeField] float successDelay = 3f;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip successAudio;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;
    
    AudioSource my_audiosource;
    Movement movementScript;

    bool isTransitioning = false;
    bool collisionsOn = true;

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
                    if (collisionsOn)
                    {
                        CrashSequence();
                    }
                    break;
            }
        }
    }

    void CrashSequence()
    {
        MeshRenderer[] mesh_renderers = GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer mesh_renderer in mesh_renderers)
        {
            mesh_renderer.enabled = false;
        }
        isTransitioning = true;
        my_audiosource.Stop();
        my_audiosource.PlayOneShot(crashAudio);
        crashParticles.Play();
        movementScript.enabled = false;
        Invoke("ReloadLevel", crashDelay);
    }

    void SuccessSequence()
    {
        isTransitioning = true;
        my_audiosource.Stop();
        my_audiosource.PlayOneShot(successAudio);
        successParticles.Play();
        movementScript.enabled = false;
        Invoke("LoadNextLevel", successDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        isTransitioning = false;
    }

    public void LoadNextLevel()
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

    public void switchCollisionsOnOff()
    {
        collisionsOn = !collisionsOn;
        Debug.Log("Collisions on: " + collisionsOn.ToString());
    }
}
