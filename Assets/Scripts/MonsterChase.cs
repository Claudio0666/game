using UnityEngine;

public class MonsterChase : MonoBehaviour
{
    public Transform player;  // Referência ao jogador
    public float speed = 3f;  // Velocidade de perseguição

    private bool isChasing = false;  // Controla se o monstro está perseguindo
    private GameManager gameManager;  // Referência ao GameManager

    void Start()
    {
        // Encontrar o GameManager automaticamente no início
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        // Verifica se o monstro deve perseguir o jogador
        if (isChasing)
        {
            // Move o monstro em direção ao jogador
            Vector3 direction = (player.position - transform.position).normalized;  // Calcula a direção
            transform.position += direction * speed * Time.deltaTime;  // Move o monstro
        }
    }

    // Função chamada quando o tempo acaba e o monstro deve começar a perseguir
    public void StartChasing()
    {
        isChasing = true;
    }

    // Detecta colisão com o jogador
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Colisão detectada com: " + other.gameObject.name);
        Debug.Log("Colidiu com: " + other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Chamando Game Over no GameManager.");
            gameManager.GameOver();
        }
    }


}
