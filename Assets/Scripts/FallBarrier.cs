using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBarrier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);
            StartCoroutine(DelayedResetLevel());
        } else
        {
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator DelayedResetLevel()
    {
        yield return new WaitForSeconds(2.5f);
        GameManager.Instance.ResetLevel();
    }
}
