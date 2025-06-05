using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public string sceneToLoad;
    public Animator fadeAnim;
    public float fadeTime = .5f;
    public Vector2 newPlayerPosition;
    private Transform player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.transform;
            fadeAnim.Play("FadeAnim");
            StartCoroutine(DelayFade());
        }

    }

    IEnumerator DelayFade()
    {
        yield return new WaitForSeconds(fadeTime);
        player.position = newPlayerPosition;
        SceneManager.LoadScene(sceneToLoad);
    }
}
