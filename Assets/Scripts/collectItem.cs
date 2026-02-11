using UnityEngine;

public class collectItem : MonoBehaviour
{
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            Collect();
        }
    }

    void Collect()
    {
        // Aqui você pode adicionar pontuação ou inventário
        Debug.Log("Item coletado: " + gameObject.name);

        // Deleta o item
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Aperte F para coletar " + gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
