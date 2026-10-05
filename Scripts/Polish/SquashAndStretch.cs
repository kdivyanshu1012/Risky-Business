using UnityEngine;
using System.Collections;

public class SquashAndStretch : MonoBehaviour
{
    [SerializeField] private Transform sprite;

    [Header("Squash Settings")]
    [SerializeField] private float stretchAmount = 0.15f;
    [SerializeField] private float animationTime = 0.15f;

    private Vector3 originalScale;
    private Coroutine squashRoutine;

    private void Start()
    {
        if (sprite == null)
        {
            sprite = transform;
        }

        originalScale = sprite.localScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (squashRoutine != null)
        {
            StopCoroutine(squashRoutine);
        }

        squashRoutine = StartCoroutine(SquashRoutine());
        
    }

    private IEnumerator SquashRoutine()
    {
        
        sprite.localScale = new Vector3(
            originalScale.y + stretchAmount,
            originalScale.z
        );

        yield return new WaitForSeconds(animationTime);


        sprite.localScale = originalScale;

        squashRoutine = null;
    }
}