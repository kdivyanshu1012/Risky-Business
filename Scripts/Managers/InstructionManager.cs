using UnityEngine;
using TMPro;
using System.Collections;

public class InstructionManager : MonoBehaviour
{
    
    private float totalDuration = 10f;
    
    private void Start()
    {
        StartCoroutine(TextEffect());
    }
    private IEnumerator TextEffect()
    {
        yield return new WaitForSeconds(totalDuration);
        Destroy(gameObject);

    }





}
