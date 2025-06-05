using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class BuildingInteraction : MonoBehaviour, IInteractable
{
    public string sceneToLoad;
    public Animator fadeAnim;
    public float fadeTime = .5f;
    public Vector2 newPlayerPosition;
    private Transform player;

    public bool CanInteract()
    {
        return true;
    }

    public bool Interact(Interactor interactor)
    {

        player = interactor.gameObject.transform;
        fadeAnim.Play("FadeAnim");
        StartCoroutine(DelayFade());
        return true;
    }

    IEnumerator DelayFade()
    {
        yield return new WaitForSeconds(fadeTime);
        player.position = newPlayerPosition;
        SceneManager.LoadScene(sceneToLoad);
    }

}
