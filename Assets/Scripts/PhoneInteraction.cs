using UnityEngine;

public class PhoneInteraction : MonoBehaviour
{
    public TaskManager taskManager;
    public ObjectivesManager objectivesManager; // Adiciona a referência ao ObjectivesManager
    public GameObject dialoguePanel;
    public UnityEngine.UI.Text dialogueText;

    private bool isPlayerNear = false;
    private bool isPhoneAnswered = false;

    // Diálogos
    private string[] dialogues = {
        "Alô, quem está falando?",
        "Oi! E a mamãe",
        "Certifique-se de completar todas as tarefas!",
        "Não volto para casa hoje"
    };

    private int currentDialogueIndex = 0;

    void Update()
    {
        // Verifica se o jogador está perto e pressionou E
        if (Input.GetKeyDown(KeyCode.E) && isPlayerNear)
        {
            Debug.Log("Jogador pressionou a tecla E perto do telefone.");
            HandleDialogue();
        }
    }

    private void HandleDialogue()
    {
        // Atende o telefone apenas se ainda não foi atendido
        if (!isPhoneAnswered)
        {
            AnswerPhone();
        }
        else
        {
            Debug.Log("O telefone já foi atendido.");
        }
    }

    private void AnswerPhone()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(true); // Exibe o painel de diálogo
            Debug.Log("Painel de diálogo ativado.");

            if (dialogueText != null)
            {
                if (currentDialogueIndex < dialogues.Length)
                {
                    // Atualiza o texto e cor do diálogo
                    dialogueText.text = dialogues[currentDialogueIndex];
                    UpdateDialogueColor(currentDialogueIndex);
                    Debug.Log($"Texto atualizado no painel: {dialogueText.text}");
                    currentDialogueIndex++;
                }
                else
                {
                    // Finaliza o diálogo
                    dialoguePanel.SetActive(false); // Esconde o painel
                    Debug.Log("Finalizando o diálogo e escondendo o painel.");
                    isPhoneAnswered = true;

                    // Atualiza o objetivo no ObjectivesManager
                    if (objectivesManager != null)
                    {
                        objectivesManager.AnswerPhone(); // Chama o método para marcar o objetivo como concluído
                    }

                    if (taskManager != null)
                    {
                        taskManager.AnswerPhone(); // Marca a tarefa como concluída
                    }
                }
            }
            else
            {
                Debug.LogWarning("Componente Text não atribuído no Inspector.");
            }
        }
        else
        {
            Debug.LogError("dialoguePanel não foi atribuído no Inspector.");
        }
    }


    private void UpdateDialogueColor(int index)
    {
        // Define cores para cada linha
        if (index == 0)
        {
            dialogueText.color = Color.white; // Cor branca para o primeiro diálogo
        }
        else
        {
            dialogueText.color = new Color(1.0f, 0.0f, 0.5f); // Cor rosa para os outros
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("Jogador entrou na área do telefone.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("Jogador saiu da área do telefone.");
        }
    }
}
