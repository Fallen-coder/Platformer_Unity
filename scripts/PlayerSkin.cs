using UnityEngine;

[System.Serializable]
public class PlayerSkin
{
    public string skinName;
    public Sprite uiIcon;
    public Sprite defaultSprite;
    public AnimatorOverrideController animatorOverride; // Optional: for custom animations per skin
}