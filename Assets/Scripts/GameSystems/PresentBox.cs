using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentBox : MonoBehaviour
{
    /// <summary>
    /// Žæ“¾ŒoŒ±’l
    /// </summary>
    [SerializeField]
    private int m_exp = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            var player = collision.gameObject;
            if (!player) return;

            var expSystem = player.GetComponent<PlayerExpSystem>();
            if (!expSystem) return;

            expSystem.AddExp(m_exp);
            Debug.Log($"{nameof(PresentBox)}.{nameof(OnTriggerEnter2D)} Get Exp.");
            Destroy(gameObject);
        }
    }
}
