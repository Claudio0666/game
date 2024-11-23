using UnityEngine;

public class WashingMachine : MonoBehaviour
{
    // Refer�ncia ao ObjectivesManager
    private ObjectivesManager objectivesManager;

    // Defina a chave de intera��o (exemplo: tecla "E")
    public KeyCode interactionKey = KeyCode.E;

    // Verifica se o jogador est� perto da m�quina de lavar
    private bool isPlayerInRange = false;

    void Start()
    {
        // Tenta encontrar o ObjectivesManager na cena
        objectivesManager = FindObjectOfType<ObjectivesManager>();
    }

    void Update()
    {
        // Verifica se o jogador est� perto da m�quina de lavar e pressionou a tecla de intera��o
        if (isPlayerInRange && Input.GetKeyDown(interactionKey))
        {
            // Verifica se o jogador coletou todas as roupas
            if (objectivesManager != null && objectivesManager.collectedClothes >= objectivesManager.totalClothes)
            {
                // Marca que as roupas foram colocadas na m�quina de lavar
                objectivesManager.SetClothesInWashingMachine(true);
            }
            else
            {
                Debug.Log("Voc� precisa coletar todas as roupas antes de coloc�-las na m�quina.");
            }
        }
    }

    // Detecta quando o jogador entra na �rea de intera��o com a m�quina de lavar (Collider Trigger)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // O jogador est� perto da m�quina de lavar
        }
    }

    // Detecta quando o jogador sai da �rea de intera��o com a m�quina de lavar (Collider Trigger)
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // O jogador saiu da �rea
        }
    }

    // Detecta colis�es f�sicas (sem isTrigger) - opcional, dependendo de seu objetivo
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // A m�quina de lavar colidiu fisicamente com o jogador (opcional, dependendo de seu jogo)
            Debug.Log("O jogador colidiu fisicamente com a m�quina de lavar.");
        }
    }

    // Detecta quando o jogador sai da �rea de colis�o f�sica (sem isTrigger) - opcional
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // O jogador saiu da colis�o f�sica com a m�quina
            Debug.Log("O jogador saiu da colis�o f�sica com a m�quina de lavar.");
        }
    }
}
