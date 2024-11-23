using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ObjectivesManager : MonoBehaviour
{
    public GameObject objectivesPanel;
    public Text objectivesText;

    // Painel de vitória e botão de reinício
    public GameObject victoryPanel;
    public Button restartButton;

    public string instructions = "Pressione 'P' para mostrar/ocultar a lista de objetivos.";
    public string doorInstructions = "Pressione 'R' para trancar/destrancar a porta. Pressione 'E' para abrir/fechar.";

    private List<string> objectives = new List<string>();
    private bool isPanelVisible = false;
    private bool canTogglePanel = false;

    public int totalWeeds = 10;
    public int collectedWeeds = 0;
    private bool chickenInArea = false;

    public int totalClothes = 4;
    public int collectedClothes = 0;
    private bool clothesInWashingMachine = false;

    private bool clothesMessageShown = false;
    private bool clothesMessageCompleted = false;

    private bool phoneAnswered = false;

    public Light directionalLight; // Referência para a luz direcional

    // Referência ao TimerManager para parar o tempo quando os objetivos forem completados
    public TimerManager timerManager;

    void Start()
    {
        if (objectivesPanel != null)
        {
            objectivesPanel.SetActive(false);
        }

        if (objectivesText != null)
        {
            objectivesText.gameObject.SetActive(false);
        }

        // Inicializar painel de vitória
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }

        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }

        AddObjective(instructions);
        AddObjective(doorInstructions);
        AddObjective("Atenda o telefone.");
        AddObjective("Corte as ervas daninhas. 0/10");
        AddObjective("Coloque a galinha na área.");
        AddObjective("Colete 4 roupas e coloque na máquina de lavar.");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && canTogglePanel)
        {
            TogglePanel();
        }

        if (chickenInArea)
        {
            AddObjective("Você completou o objetivo de colocar a galinha na área!");
        }

        if (collectedClothes >= totalClothes && !clothesMessageShown)
        {
            AddObjective("Você coletou todas as roupas, agora coloque-as na máquina de lavar.");
            clothesMessageShown = true;
        }

        if (clothesInWashingMachine && !clothesMessageCompleted)
        {
            AddObjective("Você completou o objetivo de lavar as roupas!");
            clothesMessageCompleted = true;
        }

        if (phoneAnswered && !objectives.Contains("Você completou o objetivo de atender o telefone!"))
        {
            AddObjective("Você completou o objetivo de atender o telefone!");
            phoneAnswered = false; // Reset após mostrar a mensagem
        }

        // Verifica se todos os objetivos foram completados
        if (AllObjectivesCompleted() && !objectives.Contains("Parabéns! Você completou todas as tarefas!"))
        {
            SetNightMode();
            AddObjective("Parabéns! Você completou todas as tarefas!");
            ShowVictoryScreen(); // Exibe a tela de vitória
            StopTimer(); // Para o temporizador quando todos os objetivos forem concluídos
        }
    }

    private bool AllObjectivesCompleted()
    {
        Debug.Log("Verificando objetivos:");
        Debug.Log("Weeds: " + collectedWeeds + "/" + totalWeeds);
        Debug.Log("Clothes: " + collectedClothes + "/" + totalClothes);
        Debug.Log("Clothes in Washing Machine: " + clothesInWashingMachine);
        Debug.Log("Chicken in Area: " + chickenInArea);
        Debug.Log("Phone Answered: " + phoneAnswered);

        return collectedWeeds >= totalWeeds &&
               collectedClothes >= totalClothes &&
               clothesInWashingMachine &&
               chickenInArea &&
               phoneAnswered;
    }

    private void StopTimer()
    {
        if (timerManager != null)
        {
            Debug.Log("Chamando StopTimer...");
            timerManager.StopTimer(); // Chama o método de parar o timer no TimerManager
        }
    }

    public void AddObjective(string objective)
    {
        objectives.Add(objective);
        UpdateObjectivesUI();
    }

    public void ShowPanel()
    {
        if (objectivesPanel != null)
        {
            objectivesPanel.SetActive(true);
            objectivesText.gameObject.SetActive(true);
            isPanelVisible = true;
            canTogglePanel = true;
        }
    }

    void TogglePanel()
    {
        if (objectivesPanel != null)
        {
            isPanelVisible = !isPanelVisible;
            objectivesPanel.SetActive(isPanelVisible);
            objectivesText.gameObject.SetActive(isPanelVisible);
        }
    }

    void UpdateObjectivesUI()
    {
        objectivesText.text = "";
        foreach (string obj in objectives)
        {
            objectivesText.text += "- " + obj + "\n";
        }
    }

    public void UpdateWeedsObjective()
    {
        collectedWeeds++;
        objectives[3] = "Corte as ervas daninhas. " + collectedWeeds + "/" + totalWeeds;
        UpdateObjectivesUI();

        if (collectedWeeds >= totalWeeds)
        {
            AddObjective("Você completou o objetivo de cortar as ervas daninhas!");
        }
    }

    public void SetChickenInArea(bool inArea)
    {
        chickenInArea = inArea;
        Debug.Log("SetChickenInArea: " + chickenInArea); // Log para verificar quando o valor está mudando
    }

    public void CollectClothes()
    {
        collectedClothes++;
        objectives[3] = "Colete 4 roupas e coloque na máquina de lavar. " + collectedClothes + "/" + totalClothes;
        UpdateObjectivesUI();

        if (collectedClothes >= totalClothes && !clothesMessageShown)
        {
            AddObjective("Você coletou todas as roupas, agora coloque-as na máquina de lavar.");
            clothesMessageShown = true;
        }
    }

    public void SetClothesInWashingMachine(bool inMachine)
    {
        clothesInWashingMachine = inMachine;

        if (clothesInWashingMachine && !clothesMessageCompleted)
        {
            AddObjective("Você completou o objetivo de lavar as roupas!");
            clothesMessageCompleted = true;
        }
    }

    public void AnswerPhone()
    {
        if (!phoneAnswered)
        {
            phoneAnswered = true;
            AddObjective("Você completou o objetivo de atender o telefone!");
        }
    }

    private void ShowVictoryScreen()
    {
        Debug.Log("Exibindo tela de vitória...");
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void SetNightMode()
    {
        if (directionalLight != null)
        {
            directionalLight.intensity = 0.2f;
            directionalLight.color = new Color(0.1f, 0.1f, 0.3f);
        }
    }
}
