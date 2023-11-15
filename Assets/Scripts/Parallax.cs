using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class Parallax : MonoBehaviour
{
    public Camera camparallax;
    public Transform subjectParallax;

    Vector2 startPosition;
    float startZ;
    Vector2 travel => (Vector2)camparallax.transform.position - startPosition;
    Vector2 parallaxFactor;


    public void Start()
    {
        startPosition= transform.position;
        startZ= transform.position.z;
    }
    public void Update()
    {
        transform.position = startPosition + travel;
    }
}
