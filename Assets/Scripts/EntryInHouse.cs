using UnityEngine;
using System.Collections;

public class EntryInHouse : MonoBehaviour
{
    public Transform destino;
    public KeyCode tecla = KeyCode.F;

    private bool playerPerto;
    private bool podeTeleportar = true;
    public float delay = 0.2f;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (playerPerto && podeTeleportar && Input.GetKeyDown(tecla))
        {
            StartCoroutine(Teleportar());
        }
    }

    IEnumerator Teleportar()
    {
        podeTeleportar = false;

        player.transform.position = destino.position;

        yield return new WaitForSeconds(delay);

        podeTeleportar = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerPerto = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerPerto = false;
    }
}
