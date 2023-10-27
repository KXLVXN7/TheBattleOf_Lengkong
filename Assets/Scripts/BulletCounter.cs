using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Import library UI

public class BulletCounter : MonoBehaviour
{
    public Text bulletText; // Referensi ke komponen UI Text

    void Start()
    {
        // Pastikan untuk menetapkan referensi UI Text di inspektor Unity
        if (bulletText == null)
        {
            Debug.LogError("UI Text reference is not set!");
        }
    }
}
