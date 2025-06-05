using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float castDistance = 5f;
    [SerializeField] private Vector3 raycastOffset = new Vector3(0, 1f, 0f);
    [SerializeField] private KeyCode interactButton;


    // public void Update()
    // {
    //     if (Input.GetKeyDown(interactButton))
    //     {
    //         interact();
    //     }
    // }

    public void OnTriggerStay2D(Collider2D collider)
    {
        IInteractable interactable = collider.gameObject.GetComponent<IInteractable>();
        Debug.Log("test1");
        if (interactable != null && interactable.CanInteract())
        {
            Debug.Log("test2");
            if (Input.GetKeyDown(interactButton))
            {
                interactable.Interact(this);
                Debug.Log("test3");
            }
        }
            
    }


    public void interact()
    {
        if (DoInteractionTest(out IInteractable interactable))
        {
            if (interactable.CanInteract())
            {
                interactable.Interact(this);
            }
        }
    }

    private bool DoInteractionTest(out IInteractable interactable)
    {
        interactable = null;
        Ray ray = new Ray(transform.position + raycastOffset, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, castDistance))
        {
            interactable = hitInfo.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                return true;
            }
            return false;
        }
        return false;
    }
}
