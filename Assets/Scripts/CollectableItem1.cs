using UnityEngine;

public class CollectableItem1 : MonoBehaviour
{
    public ObjectivesManager objectivesManager; // Refer�ncia ao gerenciador de objetivos
    private bool playerInRange = false; // Verifica se o jogador est� dentro do alcance

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica se � o jogador
        {
            playerInRange = true; // Jogador entrou na �rea de coleta
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica se o jogador saiu da �rea de coleta
        {
            playerInRange = false; // Jogador saiu da �rea
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E)) // Verifica se o jogador est� na �rea e pressionou 'E'
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

        // Destr�i o item coletado
        Destroy(gameObject);
    }
}
