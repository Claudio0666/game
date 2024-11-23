using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public Text timerText;  // Refer�ncia ao componente Text da UI
    public float timeRemaining = 60f;  // Tempo inicial em segundos
    private bool timerRunning = true;   // Controla o estado do temporizador

    public MonsterChase monster;  // Refer�ncia ao script do monstro

    void Start()
    {
        // Verifica se o timerText foi atribu�do e exibe o valor inicial
        if (timerText != null)
        {
            timerText.text = "Tempo restante: " + timeRemaining.ToString("F2") + "s";
        }

        // Certifique-se de que o monstro N�O esteja perseguindo ainda
        if (monster != null)
        {
            Debug.Log("Monstro aguardando o fim do temporizador.");
        }
    }

    void Update()
    {
        // Se o temporizador estiver rodando, diminui o tempo
        if (timerRunning)
        {
            timeRemaining -= Time.deltaTime;

            // Atualiza o texto do temporizador na tela
            if (timerText != null)
            {
                timerText.text = "Tempo restante: " + timeRemaining.ToString("F2") + "s";
            }

            // Quando o tempo acaba, para o temporizador
            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                timerRunning = false;
                OnTimerFinished();  // Chama a fun��o que trata o final do temporizador
            }
        }
    }

    // Fun��o chamada quando o tempo acaba
    void OnTimerFinished()
    {
        Debug.Log("Tempo esgotado!");
        if (monster != null)
        {
            monster.StartChasing();  // Faz o monstro come�ar a perseguir o jogador
            Debug.Log("Monstro come�ou a perseguir o jogador.");
        }
    }

    // Novo m�todo para parar o temporizador
    public void StopTimer()
    {
        timerRunning = false;
        Debug.Log("Temporizador parado.");
    }
}
