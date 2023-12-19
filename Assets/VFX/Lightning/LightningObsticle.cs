using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningObsticle : MonoBehaviour
{
    BoxCollider boxCollider;
    [SerializeField] float timer = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }
    private void OnEnable()
    {
        timer = 0;
        boxCollider.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 2.4f && timer <= 3)
        {
            boxCollider.enabled = true;
        }
        else if (timer >= 3)
        {
            boxCollider.enabled = false;
        }
    }
}
