using UnityEngine;

public class PhoneInteraction : MonoBehaviour
{
    public TaskManager taskManager;
    public ObjectivesManager objectivesManager; // Adiciona a refer�ncia ao ObjectivesManager
    public GameObject dialoguePanel;
    public UnityEngine.UI.Text dialogueText;

    private bool isPlayerNear = false;
    private bool isPhoneAnswered = false;

    // Di�logos
    private string[] dialogues = {
        "Al�, quem est� falando?",
        "Oi! E a mam�e",
        "Certifique-se de completar todas as tarefas!",
        "N�o volto para casa hoje"
    };

    private int currentDialogueIndex = 0;

    void Update()
    {
        // Verifica se o jogador est� perto e pressionou E
        if (Input.GetKeyDown(KeyCode.E) && isPlayerNear)
        {
            Debug.Log("Jogador pressionou a tecla E perto do telefone.");
            HandleDialogue();
        }
    }

    private void HandleDialogue()
    {
        // Atende o telefone apenas se ainda n�o foi atendido
        if (!isPhoneAnswered)
        {
            AnswerPhone();
        }
        else
        {
            Debug.Log("O telefone j� foi atendido.");
        }
    }

    private void AnswerPhone()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(true); // Exibe o painel de di�logo
            Debug.Log("Painel de di�logo ativado.");

            if (dialogueText != null)
            {
                if (currentDialogueIndex < dialogues.Length)
                {
                    // Atualiza o texto e cor do di�logo
                    dialogueText.text = dialogues[currentDialogueIndex];
                    UpdateDialogueColor(currentDialogueIndex);
                    Debug.Log($"Texto atualizado no painel: {dialogueText.text}");
                    currentDialogueIndex++;
                }
                else
                {
                    // Finaliza o di�logo
                    dialoguePanel.SetActive(false); // Esconde o painel
                    Debug.Log("Finalizando o di�logo e escondendo o painel.");
                    isPhoneAnswered = true;

                    // Atualiza o objetivo no ObjectivesManager
                    if (objectivesManager != null)
                    {
                        objectivesManager.AnswerPhone(); // Chama o m�todo para marcar o objetivo como conclu�do
                    }

                    if (taskManager != null)
                    {
                        taskManager.AnswerPhone(); // Marca a tarefa como conclu�da
                    }
                }
            }
            else
            {
                Debug.LogWarning("Componente Text n�o atribu�do no Inspector.");
            }
        }
        else
        {
            Debug.LogError("dialoguePanel n�o foi atribu�do no Inspector.");
        }
    }


    private void UpdateDialogueColor(int index)
    {
        // Define cores para cada linha
        if (index == 0)
        {
            dialogueText.color = Color.white; // Cor branca para o primeiro di�logo
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
            Debug.Log("Jogador entrou na �rea do telefone.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("Jogador saiu da �rea do telefone.");
        }
    }
}
