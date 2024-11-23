using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public GameObject objectivesPanel; // Painel da lista de objetivos
    public ObjectivesManager objectivesManager; // Refer�ncia ao gerenciador de objetivos

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica se � o jogador
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

        // Destr�i o item coletado
        Destroy(gameObject);
    }
}
