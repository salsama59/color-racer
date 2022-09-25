using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckpointCollider : MonoBehaviour
{
    [SerializeField]
    private int checkpontIndex;

    public static event Action<int> OnCheckpointEnter;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagsConstants.PLAYER_TAG) && OnCheckpointEnter != null)
        {
            OnCheckpointEnter.Invoke(this.CheckpontIndex);
        }
    }

    public int CheckpontIndex { get => checkpontIndex; set => checkpontIndex = value; }
}
