using UnityEngine;

public class CollectableItem1 : MonoBehaviour
{
    public ObjectivesManager objectivesManager; // Referência ao gerenciador de objetivos
    private bool playerInRange = false; // Verifica se o jogador está dentro do alcance

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica se é o jogador
        {
            playerInRange = true; // Jogador entrou na área de coleta
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica se o jogador saiu da área de coleta
        {
            playerInRange = false; // Jogador saiu da área
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E)) // Verifica se o jogador está na área e pressionou 'E'
        {
            Collect();
        }
    }

    void Collect()
    {
        // Atualiza o objetivo das ervas daninhas coletadas
        if (objectivesManager != null)
        {
            objectivesManager.UpdateWeedsObjective();
        }

        // Destrói o item coletado
        Destroy(gameObject);
    }
}
