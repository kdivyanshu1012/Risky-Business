using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    private IInteractable currentInteractableObject;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || MobileInputManager.InteractPressed)
        {
            tryInteract();
        }
    }

    private void tryInteract()
    {

        AudioManager.Instance.PlaySFX(9);
        currentInteractableObject.interact();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();

       

        if (interactable != null)
        {
            currentInteractableObject = interactable;
            

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();

        

        if (interactable == currentInteractableObject)
        {
            currentInteractableObject = null;
            
        }
    }
}
