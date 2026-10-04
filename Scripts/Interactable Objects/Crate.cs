using UnityEngine;
using System;
using System.Collections;

public class Crate : MonoBehaviour, IInteractable
{
    [SerializeField] private int timeToBreakCrate = 3;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject crateGameAnimator;
    [SerializeField] private GameObject crateGameObject;
    [SerializeField] private ParticleSystem openParticles;
    [SerializeField] private ParticleSystem particleBurst;
    public static event Action onCrateBroken;
    private bool isBreaking = false;

    public void interact()
    {
        if (isBreaking)
        {
            AudioManager.Instance.PlaySFX(9);
            Debug.Log("is Breaking true");
            return;
        }
        isBreaking = true;
        StartCoroutine(CrateRoutine());
    }

    private IEnumerator CrateRoutine()
    {
        openParticles.Play();
        
        yield return new WaitForSeconds(timeToBreakCrate);

        openParticles.Stop();



        animator.SetBool("isOpen", true);
        yield return new WaitForSeconds(0.5f);
        AudioManager.Instance.PlaySFX(2);
        particleBurst.Play();
        yield return new WaitForSeconds(1f);
        particleBurst.Stop();
        onCrateBroken?.Invoke();
        animator.SetBool("isOpen", false);
        crateGameAnimator.SetActive(false);

        crateGameObject.SetActive(true);
        
        
        yield return new WaitForSeconds(PlayerStats.Instance.CrateCooldown);
        crateGameObject.SetActive(false);
        crateGameAnimator.SetActive(true);

        isBreaking = false;
    }
}
