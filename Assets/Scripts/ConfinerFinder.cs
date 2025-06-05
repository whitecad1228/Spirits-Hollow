using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public class ConfinerFinder : MonoBehaviour
{

    public void Start()
    {
        CinemachineCamera camera = GetComponent<CinemachineCamera>();
        camera.Follow = GameObject.Find("Player").transform;
    }
    
    // private void OnEnable()
    // {
    //     SceneManager.sceneLoaded += OnSceneLoaded;
    // }
    // private void OnDisable()
    // {
    //     SceneManager.sceneLoaded -= OnSceneLoaded;
    // }

    // private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     // CinemachineCamera camera = GetComponent<CinemachineCamera>();
    //     // camera.gameObject.SetActive(false);

    //     CinemachineConfiner2D confiner = GetComponent<CinemachineConfiner2D>();
    //     confiner.InvalidateBoundingShapeCache();
    //     confiner.InvalidateLensCache();
    //     confiner.BoundingShape2D = GameObject.FindWithTag("Confiner").GetComponent<PolygonCollider2D>();
    //     confiner.InvalidateBoundingShapeCache();
    //     confiner.InvalidateLensCache();
    //     // camera.gameObject.SetActive(true);
    // }
}
