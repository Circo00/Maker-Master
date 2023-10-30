using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float lifetime = 1f;
    public Vector3 offset = new Vector3(0f, 0.3f, 0f);
    public Vector3 randomizeintensity = new Vector3(0.5f, 0f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
        transform.localPosition += offset;
        transform.localPosition += new Vector3(Random.Range(-randomizeintensity.x, randomizeintensity.x), Random.Range(-randomizeintensity.y, randomizeintensity.y), Random.Range(-randomizeintensity.z, randomizeintensity.z));
    }

    
}
