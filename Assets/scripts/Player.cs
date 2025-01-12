using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer bigRenderer;


    private DeathAnimation deathAnimation;
    private AnimatedSprite animatedSprite;

    public bool big => bigRenderer.enabled;
    public bool small => smallRenderer.enabled;
    public bool dead => deathAnimation.enabled;

    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
        animatedSprite = GetComponentInChildren<AnimatedSprite>();
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
    private void Shrink()
    {

    }
    private void Death()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnimation.enabled = true;

        GameManager.Instance.ResetLevel(3f);

        animatedSprite.enabled = false;
    }
}
