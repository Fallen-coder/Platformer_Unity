using UnityEngine;

[CreateAssetMenu(fileName = "NewSkinAlbum", menuName = "Skins/Player Skin Album")]
public class PlayerSkinAlbum : ScriptableObject
{
    public PlayerSkin[] skins;
}