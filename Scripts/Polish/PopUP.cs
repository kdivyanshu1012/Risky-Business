using System.Collections;
using UnityEngine;

public class PopUP : MonoBehaviour
{
    [SerializeField] private GameObject dialogPopUP;
    [SerializeField] private int dialogPopUPTimer;


    private void OnTriggerEnter2D(Collider2D player)
    {
        if(player.CompareTag("Player"))
        {
            StartCoroutine(popUPRoutine());
        }
        
    }

    private IEnumerator popUPRoutine()
    {
        dialogPopUP.SetActive(true);
        yield return new WaitForSeconds(dialogPopUPTimer);
        dialogPopUP.SetActive(false);
    }
}
