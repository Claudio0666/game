using UnityEngine;

public class MonsterChase : MonoBehaviour
{
    public Transform player;  // Refer�ncia ao jogador
    public float speed = 3f;  // Velocidade de persegui��o

    private bool isChasing = false;  // Controla se o monstro est� perseguindo
    private GameManager gameManager;  // Refer�ncia ao GameManager

    void Start()
    {
        // Encontrar o GameManager automaticamente no in�cio
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        // Verifica se o monstro deve perseguir o jogador
        if (isChasing)
        {
            // Move o monstro em dire��o ao jogador
            Vector3 direction = (player.position - transform.position).normalized;  // Calcula a dire��o
            transform.position += direction * speed * Time.deltaTime;  // Move o monstro
        }
    }

    // Fun��o chamada quando o tempo acaba e o monstro deve come�ar a perseguir
    public void StartChasing()
    {
        isChasing = true;
    }

    // Detecta colis�o com o jogador
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Colis�o detectada com: " + other.gameObject.name);
        Debug.Log("Colidiu com: " + other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Chamando Game Over no GameManager.");
            gameManager.GameOver();
        }
    }


}
