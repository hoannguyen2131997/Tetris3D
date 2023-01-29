using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGird : MonoBehaviour
{
    public Vector3Int Dimension;

    public float BlockSize;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 d = new Vector3(Dimension.x, Dimension.y, Dimension.z);
        Gizmos.DrawWireCube(transform.position, d*BlockSize);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
