using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Text gameOverText;
    public Button restartButton;

    private bool isGameOver = false;  // Verifica se o jogo j� acabou

    void Start()
    {
        // Inicializa os elementos da UI
        gameOverPanel.SetActive(false);
        restartButton.gameObject.SetActive(false);
        restartButton.onClick.AddListener(RestartGame);
    }

    // Fun��o chamada quando o jogador perde
    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        Debug.Log("Game Over chamado");
        gameOverPanel.SetActive(true);
        Debug.Log("Painel de Game Over ativado");
        restartButton.gameObject.SetActive(true);
        Debug.Log("Bot�o Restart ativado");
    }


    // Fun��o para reiniciar a cena
    public void RestartGame()
    {
        // Desabilita o bot�o ap�s o clique
        restartButton.gameObject.SetActive(false);

        // Recarrega a cena atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
