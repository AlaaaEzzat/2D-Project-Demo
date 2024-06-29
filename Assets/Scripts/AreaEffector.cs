using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffector : MonoBehaviour
{
   [SerializeField] private GameObject blackScreen , player , startPos;
    private bool restart = false;

    private void Update()
    {
        if (restart)
        {
            player.transform.position = startPos.transform.position;
            restart = false;
            blackScreen.GetComponent<BlackScreen>().anim.SetBool("Black", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(wait_Seconds());
        }
    }
    IEnumerator wait_Seconds()
    {
        yield return new WaitForSeconds(2);
        blackScreen.GetComponent<BlackScreen>().anim.SetBool("Black", true);  
        yield return new WaitForSeconds(2);
        restart = true;
    }
}
