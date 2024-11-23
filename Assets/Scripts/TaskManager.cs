using UnityEngine;

public class TaskManager : MonoBehaviour
{
    private bool isPhoneAnswered = false; // Status da tarefa de atender o telefone

    public void AnswerPhone()
    {
        if (!isPhoneAnswered)
        {
            isPhoneAnswered = true;
            Debug.Log("Tarefa conclu�da: O telefone foi atendido.");
        }
        else
        {
            Debug.Log("O telefone j� foi atendido anteriormente.");
        }
    }

    public bool IsPhoneAnswered()
    {
        return isPhoneAnswered;
    }

    // Adicione mais m�todos para outras tarefas, se necess�rio
    public void ResetTasks()
    {
        isPhoneAnswered = false;
        Debug.Log("Todas as tarefas foram resetadas.");
    }
}
