using UnityEngine;

public class ClothesItem : MonoBehaviour
{
    // Refer�ncia ao ObjectivesManager
    private ObjectivesManager objectivesManager;

    // Vari�vel para verificar se o jogador est� dentro do alcance para interagir
    private bool isPlayerInRange = false;

    void Start()
    {
        // Tenta encontrar o ObjectivesManager na cena
        objectivesManager = FindObjectOfType<ObjectivesManager>();
    }

    void Update()
    {
        // Verifica se o jogador est� dentro do alcance e pressionou a tecla 'E'
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Chama o m�todo de coleta de roupas no ObjectivesManager
            if (objectivesManager != null)
            {
                objectivesManager.CollectClothes();
                // Opcional: Destroy o item de roupa ap�s ser coletado
                Destroy(gameObject);
            }
        }
    }

    // Detecta quando o jogador entra na �rea de intera��o com o item de roupa
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // O jogador est� perto do item de roupa
        }
    }

    // Detecta quando o jogador sai da �rea de intera��o com o item de roupa
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // O jogador saiu da �rea de intera��o
        }
    }
}
