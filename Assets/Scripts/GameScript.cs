using UnityEngine;

public class GameScript : MonoBehaviour
{
    private SceneLoader _sceneLoader;
    // Start is called before the first frame update
    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        _sceneLoader = GetComponent<SceneLoader>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _sceneLoader.StartLoadingScene("MainMenu");
        }
    }
}
