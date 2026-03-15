using System.Collections.Generic;
using UnityEngine;

public static class Effect
{
    private static GameObject EffectObject;
    private static List<GameObject> Effects = new();

    public static void Initialize(GameObject effect)
    {
        EffectObject = effect;
        Effects.Add(EffectObject);
    }

    public static void Play(Vector3 position1, Vector3 position2)
    {
        var effect = GetGameObject();
        effect.transform.position = position1;
        effect.GetComponent<FrameAnimation>().Play();

        effect = GetGameObject();
        effect.transform.position = position2;
        effect.GetComponent<FrameAnimation>().Play();
    }

    private static GameObject GetGameObject()
    {
        foreach (var effect in Effects)
        {
            if (!effect.GetComponent<FrameAnimation>().IsPlaying)
            {
                return effect;
            }
        }
        return GameObject.Instantiate(EffectObject, EffectObject.transform.parent);
    }
}