using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ObjectivesManager : MonoBehaviour
{
    public GameObject objectivesPanel;
    public Text objectivesText;

    // Painel de vit�ria e bot�o de rein�cio
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

    public Light directionalLight; // Refer�ncia para a luz direcional

    // Refer�ncia ao TimerManager para parar o tempo quando os objetivos forem completados
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

        // Inicializar painel de vit�ria
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
        AddObjective("Coloque a galinha na �rea.");
        AddObjective("Colete 4 roupas e coloque na m�quina de lavar.");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && canTogglePanel)
        {
            TogglePanel();
        }

        if (chickenInArea)
        {
            AddObjective("Voc� completou o objetivo de colocar a galinha na �rea!");
        }

        if (collectedClothes >= totalClothes && !clothesMessageShown)
        {
            AddObjective("Voc� coletou todas as roupas, agora coloque-as na m�quina de lavar.");
            clothesMessageShown = true;
        }

        if (clothesInWashingMachine && !clothesMessageCompleted)
        {
            AddObjective("Voc� completou o objetivo de lavar as roupas!");
            clothesMessageCompleted = true;
        }

        if (phoneAnswered && !objectives.Contains("Voc� completou o objetivo de atender o telefone!"))
        {
            AddObjective("Voc� completou o objetivo de atender o telefone!");
            phoneAnswered = false; // Reset ap�s mostrar a mensagem
        }

        // Verifica se todos os objetivos foram completados
        if (AllObjectivesCompleted() && !objectives.Contains("Parab�ns! Voc� completou todas as tarefas!"))
        {
            SetNightMode();
            AddObjective("Parab�ns! Voc� completou todas as tarefas!");
            ShowVictoryScreen(); // Exibe a tela de vit�ria
            StopTimer(); // Para o temporizador quando todos os objetivos forem conclu�dos
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
            timerManager.StopTimer(); // Chama o m�todo de parar o timer no TimerManager
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
            AddObjective("Voc� completou o objetivo de cortar as ervas daninhas!");
        }
    }

    public void SetChickenInArea(bool inArea)
    {
        chickenInArea = inArea;
        Debug.Log("SetChickenInArea: " + chickenInArea); // Log para verificar quando o valor est� mudando
    }

    public void CollectClothes()
    {
        collectedClothes++;
        objectives[3] = "Colete 4 roupas e coloque na m�quina de lavar. " + collectedClothes + "/" + totalClothes;
        UpdateObjectivesUI();

        if (collectedClothes >= totalClothes && !clothesMessageShown)
        {
            AddObjective("Voc� coletou todas as roupas, agora coloque-as na m�quina de lavar.");
            clothesMessageShown = true;
        }
    }

    public void SetClothesInWashingMachine(bool inMachine)
    {
        clothesInWashingMachine = inMachine;

        if (clothesInWashingMachine && !clothesMessageCompleted)
        {
            AddObjective("Voc� completou o objetivo de lavar as roupas!");
            clothesMessageCompleted = true;
        }
    }

    public void AnswerPhone()
    {
        if (!phoneAnswered)
        {
            phoneAnswered = true;
            AddObjective("Voc� completou o objetivo de atender o telefone!");
        }
    }

    private void ShowVictoryScreen()
    {
        Debug.Log("Exibindo tela de vit�ria...");
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
