using UnityEngine;
using System.Collections;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioClip footStepSFX;
    private PlayerMovement movement;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        StartCoroutine(PlayFootsteps());
    }

    IEnumerator PlayFootsteps()
    {
        while (true)
        {
            if (movement.moveDirection.magnitude > 0.1f)
            {
                AudioManager.instance.PlayerSFX(footStepSFX);
            }

            yield return new WaitForSeconds(0.35f);
        }
    }
}