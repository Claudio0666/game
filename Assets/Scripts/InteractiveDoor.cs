using UnityEngine;

public class InteractiveDoor : MonoBehaviour
{
    private bool playerInRange = false; // Verifica se o jogador está próximo
    private Collider2D doorCollider;   // Collider principal da porta
    private SpriteRenderer doorSprite; // Sprite da porta

    public bool isLocked = false; // Estado da porta: trancada ou destrancada
    public string lockedMessage = "A porta está trancada.";
    public string unlockedMessage = "A porta está destrancada. Pressione E para abrir/fechar.";
    public string lockMessage = "A porta foi trancada.";
    public string unlockMessage = "A porta foi destrancada.";
    public float openTransparency = 0.5f; // Transparência da porta quando aberta
    private Color originalColor;

    void Start()
    {
        doorCollider = GetComponent<Collider2D>();
        doorSprite = GetComponent<SpriteRenderer>();

        if (doorCollider == null)
            Debug.LogError("Nenhum Collider2D encontrado no objeto da porta!");

        if (doorSprite == null)
            Debug.LogError("Nenhum SpriteRenderer encontrado no objeto da porta!");
        else
            originalColor = doorSprite.color; // Salva a cor original
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (isLocked)
            {
                Debug.Log(lockedMessage);
            }
            else
            {
                ToggleDoor();  // Alterna entre abrir e fechar a porta
            }
        }

        if (playerInRange && Input.GetKeyDown(KeyCode.R))
        {
            ToggleLock();  // Alterna entre trancar e destrancar a porta
        }
    }

    // Função para abrir ou fechar a porta
    void ToggleDoor()
    {
        if (doorCollider != null)
        {
            bool isOpen = doorCollider.enabled; // Porta está "fechada" se o Collider estiver ativado

            // Inverte o estado do Collider para "abrir" ou "fechar"
            doorCollider.enabled = !isOpen;

            if (doorSprite != null)
            {
                // Ajusta a transparência com base no estado da porta
                doorSprite.color = isOpen
                    ? new Color(originalColor.r, originalColor.g, originalColor.b, openTransparency) // Porta fechada (semi-transparente)
                    : originalColor; // Porta aberta (opaca)
            }

            // Ajuste do Collider para permitir a passagem do jogador
            if (!isOpen)
            {
                // Porta fechada: mantém o Collider ativo (como um bloqueio)
            }
            else
            {
                // Porta aberta: ajusta o Collider para permitir a passagem
                doorCollider.enabled = false;  // Desativa o Collider para que o jogador possa atravessar
            }

            Debug.Log(!isOpen ? "Porta fechada" : "Porta aberta");
        }
    }

    // Função para trancar ou destrancar a porta
    void ToggleLock()
    {
        if (isLocked)
        {
            isLocked = false; // Destranca a porta
            Debug.Log(unlockMessage); // Exibe a mensagem de porta destrancada
        }
        else
        {
            isLocked = true; // Tranca a porta
            Debug.Log(lockMessage); // Exibe a mensagem de porta trancada
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true; // Define que o jogador está próximo
            Debug.Log(isLocked ? lockedMessage : unlockedMessage);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false; // Define que o jogador saiu do alcance
        }
    }
}
