using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessManager : MonoBehaviour
{
    [SerializeField] Volume volume;
    ChromaticAberration chromaticAberration;
    Vignette vignette;


    [SerializeField] AnimationCurve chromaticAberrationCurve;
    [SerializeField] AnimationCurve vignetteCurve;
    [SerializeField] ColorParameter vignetteColor;
    [SerializeField] ColorParameter defaultVignetteColor;
    float defaultVignetteIntensity;

    [SerializeField] float shootEffectDuration;
    [SerializeField] float damageEffectDuration;
    void Start()
    {
        volume.profile.TryGet<ChromaticAberration>(out chromaticAberration);
        volume.profile.TryGet<Vignette>(out vignette);
        defaultVignetteIntensity = vignette.intensity.value;
        //defaultVignetteColor = (ColorParameter)vignette.color;


        
    }

    void Update()
    {
        
    }

    public void ShootEffect()
    {
        StartCoroutine(AnimateShootEffect());
    }

    public void DamageEffect(float healthLeft)
    {
        float diff = 100f - healthLeft;

        float intensity = Unity.Mathematics.math.remap(0f, 100f, 0.25f, 0.35f, diff);


        StartCoroutine(AnimateDamageEffect(intensity));
    }

    IEnumerator AnimateDamageEffect(float intensity)
    {
        float time = 0f;
        float duration = damageEffectDuration;
        vignette.color.SetValue(vignetteColor);
        vignette.intensity.value = intensity;
        while(time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        vignette.color.SetValue(defaultVignetteColor);
        //vignette.intensity.value = defaultVignetteIntensity;
    }

    IEnumerator AnimateShootEffect()
    {
        float time = 0f;
        float duration = shootEffectDuration;
        while(time < duration)
        {
            chromaticAberration.intensity.value = chromaticAberrationCurve.Evaluate(time);
            //vignette.intensity.value = vignetteCurve.Evaluate(time);
            time += Time.deltaTime;
            yield return null;
        }
        chromaticAberration.intensity.value = chromaticAberrationCurve.keys[chromaticAberrationCurve.length - 1].value;
        //vignette.intensity.value = vignetteCurve.keys[vignetteCurve.length - 1].value;
    }
}
