using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D1 : BaseDimension
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
        gameManager.PostProccesControl.FilmGrain.active = true;
        gameManager.PostProccesControl.reflectionProbe.gameObject.SetActive(false);
        gameManager.PostProccesControl.directinoalLightGameObj.transform.rotation = Quaternion.Euler(90, 0, 0);
    }
}
