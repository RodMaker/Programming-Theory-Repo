using UnityEngine;

// Handles setting a color to a given renderer and material slot. Used to simplify coloring out Unit
// It can be the added on the visual prefab and is used on the CharacterHandler
public class ColorHandler : MonoBehaviour
{
    // Start() and Update() methods deleted - we don't need them right now

    public Renderer TintRenderer;
    public int TintMaterialSlot;

    public void SetColor(Color color)
    {
        var prop = new MaterialPropertyBlock();
        prop.SetColor("_BaseColor", color);
        TintRenderer.SetPropertyBlock(prop, TintMaterialSlot);
    }
}
