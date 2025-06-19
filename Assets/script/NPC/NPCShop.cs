using UnityEngine;

public class NPCShop : MonoBehaviour
{
    public GameObject shopUI;

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            shopUI.SetActive(!shopUI.activeSelf);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            //Show "Nhấn E để giao dịch"
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            shopUI.SetActive(false);
        }
    }
}
