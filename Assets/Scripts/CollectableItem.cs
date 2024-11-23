using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public GameObject objectivesPanel; // Painel da lista de objetivos
    public ObjectivesManager objectivesManager; // Referência ao gerenciador de objetivos

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica se é o jogador
        {
            Collect();
        }
    }

    void Collect()
    {
        // Exibe o painel de objetivos via ObjectivesManager
        if (objectivesManager != null)
        {
            objectivesManager.ShowPanel();
        }

        // Destrói o item coletado
        Destroy(gameObject);
    }
}
