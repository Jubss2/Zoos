using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    private float time;
    private void Update()
    {
        time += Time.deltaTime;
        if(time > 0.5f ) {
            Destroy(gameObject);
        }
    }
}
