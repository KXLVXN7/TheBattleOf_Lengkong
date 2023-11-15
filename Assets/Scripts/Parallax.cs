using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class Parallax : MonoBehaviour
{
    public Camera camparallax;
    public Transform subjectParallax;

    Vector2 startPosition;
    float startZ;
    Vector2 travel => (Vector2)camparallax.transform.position - startPosition;

    float distanceFromSubject => transform.position.z - subjectParallax.position.z;
    float clippingPlane => (camparallax.transform.position.z + (distanceFromSubject > 0 ? camparallax.farClipPlane : camparallax.nearClipPlane));

    float parallaxFactor => Mathf.Abs(distanceFromSubject) / clippingPlane;


    public void Start()
    {
        startPosition= transform.position;
        startZ= transform.position.z;
    }
    public void Update()
    {   
        Vector2 newPos = startPosition + travel * 0.9f;
        transform.position = new Vector3(newPos.x, newPos.y, startZ);
    }
}
