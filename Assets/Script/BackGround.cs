using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public Vector2 offset;
    public float speed = 0.1f;

    void Update()
    {
        offset.y += speed * Time.deltaTime;
        GetComponent<MeshRenderer>().material.mainTextureOffset = offset;
    }
}
