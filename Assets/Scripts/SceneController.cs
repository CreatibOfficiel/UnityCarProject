using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour
{

    private static SceneController instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            GameObject.Destroy(instance);

        DontDestroyOnLoad(this);
    }

    public void loadScene(int index)
    {
        if(SceneManager.GetActiveScene().buildIndex != index)
        {
            SceneManager.LoadScene(index);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1) && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(3);
        }
    }

    public static SceneController GetInstance()
    {
        return instance;
    }
}
