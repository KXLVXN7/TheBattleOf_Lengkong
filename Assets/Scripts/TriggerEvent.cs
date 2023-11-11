using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public enum TypeTag
{
    Player,
    Enemy,
    Checkpoint,
    Finish,
    Trigger,
    other,
}
public class TriggerEvent : MonoBehaviour
{
    public TypeTag targetTag;
    public UnityEvent<GameObject> OnTrigger;
    /*    public GameObject Trap_Tong01;
        public Transform spawnPoint;*/
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == targetTag.ToString())
        {
            Debug.Log(gameObject.tag + " Kena! " + col.gameObject.tag);
            OnTrigger?.Invoke(col.gameObject);

        }
    }
    /*    private void onTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Instantiate(Trap_Tong01);
            }
        }
    */

}