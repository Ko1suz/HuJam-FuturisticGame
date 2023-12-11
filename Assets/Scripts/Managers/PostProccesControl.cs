using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class PostProccesControl : MonoBehaviour
{
    [SerializeField] Volume PostProcces;
    public GameObject directinoalLightGameObj;
    public Light directinoalLight;
    public ReflectionProbe reflectionProbe;
    public Bloom bloom;
    public SplitToning splitToning;
    public ChromaticAberration chromaticAberration;
    public MotionBlur motionBlur;
    public LensDistortion lensDistortion;
    public FilmGrain FilmGrain;



    // Start is called before the first frame update
    private void Awake()
    {
       

        SetReferaces();
    }

    void SetReferaces()
    {
        PostProcces.profile.TryGet(out bloom);
        PostProcces.profile.TryGet(out splitToning);
        PostProcces.profile.TryGet(out chromaticAberration); // 0 / 1
        PostProcces.profile.TryGet(out motionBlur);
        PostProcces.profile.TryGet(out lensDistortion); // 0 / -0.6
        PostProcces.profile.TryGet(out FilmGrain); // 0 / -0.6
    }
}
