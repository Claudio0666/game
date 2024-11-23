using UnityEngine;

public class TaskManager : MonoBehaviour
{
    private bool isPhoneAnswered = false; // Status da tarefa de atender o telefone

    public void AnswerPhone()
    {
        if (!isPhoneAnswered)
        {
            isPhoneAnswered = true;
            Debug.Log("Tarefa concluída: O telefone foi atendido.");
        }
        else
        {
            Debug.Log("O telefone já foi atendido anteriormente.");
        }
    }

    public bool IsPhoneAnswered()
    {
        return isPhoneAnswered;
    }

    // Adicione mais métodos para outras tarefas, se necessário
    public void ResetTasks()
    {
        isPhoneAnswered = false;
        Debug.Log("Todas as tarefas foram resetadas.");
    }
}
