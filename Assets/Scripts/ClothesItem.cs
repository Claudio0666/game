using UnityEngine;

public class ClothesItem : MonoBehaviour
{
    // Referência ao ObjectivesManager
    private ObjectivesManager objectivesManager;

    // Variável para verificar se o jogador está dentro do alcance para interagir
    private bool isPlayerInRange = false;

    void Start()
    {
        // Tenta encontrar o ObjectivesManager na cena
        objectivesManager = FindObjectOfType<ObjectivesManager>();
    }

    void Update()
    {
        // Verifica se o jogador está dentro do alcance e pressionou a tecla 'E'
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Chama o método de coleta de roupas no ObjectivesManager
            if (objectivesManager != null)
            {
                objectivesManager.CollectClothes();
                // Opcional: Destroy o item de roupa após ser coletado
                Destroy(gameObject);
            }
        }
    }

    // Detecta quando o jogador entra na área de interação com o item de roupa
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // O jogador está perto do item de roupa
        }
    }

    // Detecta quando o jogador sai da área de interação com o item de roupa
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // O jogador saiu da área de interação
        }
    }
}
