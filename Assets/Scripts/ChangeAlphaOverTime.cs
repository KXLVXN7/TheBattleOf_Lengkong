using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAlphaOverTime : MonoBehaviour
{
    private Renderer objectRenderer;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        // Ambil warna saat ini dari objek
        Color currentColor = objectRenderer.material.color;

        // Setel komponen alpha (A) ke 1.0f (maksimal)
        currentColor.a = 1.0f;

        // Atur warna objek dengan komponen alpha yang telah diubah
        objectRenderer.material.color = currentColor;
    }
}
