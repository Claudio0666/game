using UnityEngine;

public class WashingMachine : MonoBehaviour
{
    // Referência ao ObjectivesManager
    private ObjectivesManager objectivesManager;

    // Defina a chave de interação (exemplo: tecla "E")
    public KeyCode interactionKey = KeyCode.E;

    // Verifica se o jogador está perto da máquina de lavar
    private bool isPlayerInRange = false;

    void Start()
    {
        // Tenta encontrar o ObjectivesManager na cena
        objectivesManager = FindObjectOfType<ObjectivesManager>();
    }

    void Update()
    {
        // Verifica se o jogador está perto da máquina de lavar e pressionou a tecla de interação
        if (isPlayerInRange && Input.GetKeyDown(interactionKey))
        {
            // Verifica se o jogador coletou todas as roupas
            if (objectivesManager != null && objectivesManager.collectedClothes >= objectivesManager.totalClothes)
            {
                // Marca que as roupas foram colocadas na máquina de lavar
                objectivesManager.SetClothesInWashingMachine(true);
            }
            else
            {
                Debug.Log("Você precisa coletar todas as roupas antes de colocá-las na máquina.");
            }
        }
    }

    // Detecta quando o jogador entra na área de interação com a máquina de lavar (Collider Trigger)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // O jogador está perto da máquina de lavar
        }
    }

    // Detecta quando o jogador sai da área de interação com a máquina de lavar (Collider Trigger)
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // O jogador saiu da área
        }
    }

    // Detecta colisões físicas (sem isTrigger) - opcional, dependendo de seu objetivo
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // A máquina de lavar colidiu fisicamente com o jogador (opcional, dependendo de seu jogo)
            Debug.Log("O jogador colidiu fisicamente com a máquina de lavar.");
        }
    }

    // Detecta quando o jogador sai da área de colisão física (sem isTrigger) - opcional
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // O jogador saiu da colisão física com a máquina
            Debug.Log("O jogador saiu da colisão física com a máquina de lavar.");
        }
    }
}
