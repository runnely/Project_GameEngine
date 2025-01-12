using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer bigRenderer;
    private PlayerSpriteRenderer activeRenderer;


    private DeathAnimation deathAnimation;
    private CapsuleCollider2D capsuleCollider;

    private AnimatedSprite animatedSprite;

    public bool big => bigRenderer.enabled;
    public bool small => smallRenderer.enabled;
    public bool dead => deathAnimation.enabled;

    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
        animatedSprite = GetComponentInChildren<AnimatedSprite>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    public void Hit()
    {
        if (big)
        {
            Shrink();
        } else
        {
            Death();
        }
    }
    private void Death()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnimation.enabled = true;

        GameManager.Instance.ResetLevel(3f);

        animatedSprite.enabled = false;
    }
    public void Grow()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = true;
        activeRenderer = bigRenderer;

        capsuleCollider.size = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0f, 0.5f);

        StartCoroutine(ScaleAnimation());
    }
    private void Shrink()
    {
        smallRenderer.enabled = true;
        bigRenderer.enabled = false;
        activeRenderer = smallRenderer;

        capsuleCollider.offset = new Vector2(0f, 0f);
        capsuleCollider.size = new Vector2(1f, 1f);

        StartCoroutine(ScaleAnimation());
    }

    private IEnumerator ScaleAnimation()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration) { 

            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                smallRenderer.enabled = !smallRenderer.enabled;
                bigRenderer.enabled= !smallRenderer.enabled;
            }
            
            yield return null; 
        
        }

        smallRenderer.enabled = false;
        bigRenderer.enabled = false;   
        activeRenderer.enabled = true;
    }
}
