using UnityEngine;
using StarterAssets;

public class CarEnterExit : MonoBehaviour
{
    [Header("Referências")]
    public CarController carController;
    public Transform exitPoint;

    [Header("Câmeras")]
    public GameObject carCamera; // câmera do carro

    GameObject player;
    bool playerInside = false;

    void Start()
    {
        carController.enabled = false;
        if (carCamera != null)
            carCamera.SetActive(false);
    }

    void Update()
    {
        if (player == null) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!playerInside)
                EnterCar();
            else
                ExitCar();
        }
    }

    void EnterCar()
    {
        playerInside = true;

        // PLAYER OFF
        player.GetComponent<ThirdPersonController>().enabled = false;
        player.GetComponent<StarterAssetsInputs>().enabled = false;
        player.GetComponent<CharacterController>().enabled = false;

        foreach (Renderer r in player.GetComponentsInChildren<Renderer>())
            r.enabled = false;

        // CAR ON
        carController.enabled = true;

        if (carCamera != null)
            carCamera.SetActive(true);
    }

    void ExitCar()
    {
        playerInside = false;

        // CAR OFF
        carController.enabled = false;

        if (carCamera != null)
            carCamera.SetActive(false);

        // POSICIONA PLAYER
        player.transform.position = exitPoint.position;

        // PLAYER ON
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<ThirdPersonController>().enabled = true;
        player.GetComponent<StarterAssetsInputs>().enabled = true;

        foreach (Renderer r in player.GetComponentsInChildren<Renderer>())
            r.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            player = other.gameObject;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            player = null;
    }
}
