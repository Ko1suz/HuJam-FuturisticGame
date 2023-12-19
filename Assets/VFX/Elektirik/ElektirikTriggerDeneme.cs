using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElektirikTriggerDeneme : MonoBehaviour
{
    public GameObject vfxEffect;
    public Transform[] points;
    SphereCollider sphereCollider;


    public List<GameObject> targets;
    // Start is called before the first frame update
    void Start()
    {
        vfxEffect.SetActive(false);

    }

    float x;
    float y;
    float z;

    public float timer;
    public float timer2;
    public float yenilemeHizi = 0.1f;

    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }
    private void Update()
    {
        CheckDistance();
        VFX_targetSystem();
    }

    private void OnDisable()
    {
        points[1].transform.localPosition = Vector3.zero;
        points[2].transform.localPosition = Vector3.zero;
        points[3].transform.localPosition = Vector3.zero;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            targets.Add(other.gameObject);
        }
    }

    void CheckDistance()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, targets[i].gameObject.GetComponent<Collider>().ClosestPoint(transform.position));
            if (distance > sphereCollider.radius || targets[i].activeSelf == false)
            {
                targets.Remove(targets[i]);
                vfxEffect.SetActive(false);
            }
        }
    }

    float randomTargettimer = 0;
    float dfTimer = 0;
    private float dfPer = .2f;
    void VFX_targetSystem()
    {
        if (targets.Count > 0)
        {
            vfxEffect.SetActive(true);
            timer += Time.deltaTime;
            timer2 += Time.deltaTime;
            if (timer > yenilemeHizi)
            {
                x = Random.Range(points[0].transform.position.x, points[3].transform.position.x);
                y = Random.Range(points[0].transform.position.y, points[3].transform.position.y);
                z = Random.Range(points[0].transform.position.z, points[3].transform.position.z);
                timer = 0;
            }
            points[0].transform.position = transform.position;
            points[1].transform.position = Vector3.Lerp(points[1].transform.position, new Vector3(x, y, z), 0.1f);

            if (timer2 > yenilemeHizi)
            {
                x = Random.Range(points[0].transform.position.x, points[3].transform.position.x);
                y = Random.Range(points[0].transform.position.y, points[3].transform.position.y);
                z = Random.Range(points[0].transform.position.z, points[3].transform.position.z);
                timer2 = 0;
            }

            points[2].transform.position = Vector3.Lerp(points[2].transform.position, new Vector3(x, y, z), 0.1f);
            points[3].transform.position = targets[Random.Range(0, targets.Count)].gameObject.GetComponent<Collider>().ClosestPoint(transform.position);
        }
        else
        {
            vfxEffect.SetActive(false);
        }

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            targets.Remove(other.gameObject);
        }
    }

    public void CloseVfx()
    {
        vfxEffect.SetActive(false);
        targets.Clear();
    }
}
