using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosao : MonoBehaviour
{
    private float time;
    private void Update()
    {
        time += Time.deltaTime;
        if (time > 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
