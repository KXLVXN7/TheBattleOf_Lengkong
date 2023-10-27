using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Referensi ke pemain (player)
    public float smoothing = 5f; // Kekuatan pergerakan kamera (semakin kecil semakin mulus)

    Vector3 offset; // Jarak awal antara kamera dan pemain

    void Start()
    {
        // Menghitung jarak awal antara kamera dan pemain
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            // Hitung posisi target yang diikuti oleh kamera
            Vector3 targetCamPos = target.position + offset;

            // Ubah posisi kamera menuju posisi target secara mulus
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
    }
}
