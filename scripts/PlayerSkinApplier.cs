using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerSkinApplier : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); 
    }

    private void OnEnable()
    {
        // Listen for global skin changes
        if (SkinManager.Instance != null)
        {
            SkinManager.Instance.OnSkinChanged += ApplySkin;
        }
    }

    private void Start()
    {
        // Force the skin to apply on start-up
        Invoke(nameof(ApplyCurrentSkin), 0.05f); // Delays it by a split second so SkinManager can load first
    }

    private void OnDisable()
    {
        if (SkinManager.Instance != null)
        {
            SkinManager.Instance.OnSkinChanged -= ApplySkin;
        }
    }

    private void ApplyCurrentSkin()
    {
        if (SkinManager.Instance != null)
        {
            ApplySkin(SkinManager.Instance.GetCurrentSkin());
        }
    }

    private void ApplySkin(PlayerSkin skin)
    {
        if (skin == null) return;

        // 1. Update the static/starting sprite
        if (skin.defaultSprite != null)
        {
            spriteRenderer.sprite = skin.defaultSprite;
            Debug.Log($"Successfully applied sprite: {skin.skinName} to Player SpriteRenderer!");
        }

        // 2. Update animations if you use an Animator
        if (animator != null && skin.animatorOverride != null)
        {
            animator.runtimeAnimatorController = skin.animatorOverride;
        }
    }
}