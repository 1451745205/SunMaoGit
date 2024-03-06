using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XFramework.Objects;

public class Shoot : MonoBehaviour
{
    public Transform point;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            GameObject obj = ObjectPool.Instantiate(bullet);
            obj.GetComponent<Rigidbody>().velocity = obj.transform.forward * 10f;
            ObjectPool.Destroy(obj, 3f,item =>
            {
                item.transform.position = point.position;
            });
        }
    }
}
