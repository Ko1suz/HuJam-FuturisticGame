using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D2 : BaseDimension
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        SetPostProcces();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        SetPostProcces();
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }


    void SetPostProcces()
    {
        GameManager._instance.PostProccesControl.FilmGrain.active = false;
        gameManager.PostProccesControl.reflectionProbe.gameObject.SetActive(true);
        gameManager.PostProccesControl.reflectionProbe.intensity = 1;
        gameManager.PostProccesControl.directinoalLightGameObj.transform.rotation = Quaternion.Euler(-45, 0, 0);
    }
}
