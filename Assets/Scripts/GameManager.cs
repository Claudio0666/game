using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Text gameOverText;
    public Button restartButton;

    private bool isGameOver = false;  // Verifica se o jogo já acabou

    void Start()
    {
        // Inicializa os elementos da UI
        gameOverPanel.SetActive(false);
        restartButton.gameObject.SetActive(false);
        restartButton.onClick.AddListener(RestartGame);
    }

    // Função chamada quando o jogador perde
    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        Debug.Log("Game Over chamado");
        gameOverPanel.SetActive(true);
        Debug.Log("Painel de Game Over ativado");
        restartButton.gameObject.SetActive(true);
        Debug.Log("Botão Restart ativado");
    }


    // Função para reiniciar a cena
    public void RestartGame()
    {
        // Desabilita o botão após o clique
        restartButton.gameObject.SetActive(false);

        // Recarrega a cena atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
