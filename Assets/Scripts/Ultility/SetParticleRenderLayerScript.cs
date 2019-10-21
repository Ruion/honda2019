using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(ParticleSystem))]
public class SetParticleRenderLayerScript : MonoBehaviour
{

    [ContextMenu("SortLayer")]
    public void SortLayer()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        ParticleSystemRenderer particleSystem = GetComponent<ParticleSystemRenderer>();

        particleSystem.sortingLayerID = spriteRenderer.sortingLayerID;
        particleSystem.sortingOrder = spriteRenderer.sortingOrder;
    }

}