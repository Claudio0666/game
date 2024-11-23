using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenBehavior : MonoBehaviour
{
    public float detectionRadius = 5f; // Dist�ncia para detectar o jogador
    public float speed = 3f;           // Velocidade da galinha
    public Transform player;           // Refer�ncia ao jogador
    public Collider2D targetArea;      // �rea de destino para a galinha

    private Vector3 runDirection;

    private float initialZPosition; // Para manter o valor original de Z da galinha

    void Start()
    {
        // Armazena a posi��o Z inicial da galinha
        initialZPosition = transform.position.z;
    }

    void Update()
    {
        if (player == null) return;

        // Calcular a dist�ncia entre o jogador e a galinha
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius)
        {
            // Galinha corre na dire��o oposta ao jogador
            runDirection = (transform.position - player.position).normalized;

            // Move a galinha na dire��o oposta ao jogador com suaviza��o
            transform.position = Vector3.MoveTowards(transform.position, transform.position + runDirection, speed * Time.deltaTime);

            // Se a galinha estiver indo para a esquerda, faz o flip, se estiver indo para a direita, desfaz o flip
            if (runDirection.x < 0)
            {
                // Flip horizontal (virar a galinha para a esquerda)
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (runDirection.x > 0)
            {
                // Flip horizontal (virar a galinha para a direita)
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }

        // Assegura que a galinha n�o se mova no eixo Z
        transform.position = new Vector3(transform.position.x, transform.position.y, initialZPosition);

        // Travar a rota��o no eixo Z para que a galinha n�o gire
        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.z);
    }

    // Verifica se a galinha entrou na �rea de destino
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other == targetArea)
        {
            // Informa o ObjectivesManager que a galinha est� na �rea
            ObjectivesManager objectivesManager = FindObjectOfType<ObjectivesManager>();
            if (objectivesManager != null)
            {
                objectivesManager.SetChickenInArea(true);
            }
        }
    }

    // Verifica se a galinha saiu da �rea de destino
    void OnTriggerExit2D(Collider2D other)
    {
        if (other == targetArea)
        {
            // Informa que a galinha saiu da �rea (n�o completou o objetivo)
            ObjectivesManager objectivesManager = FindObjectOfType<ObjectivesManager>();
            if (objectivesManager != null)
            {
                objectivesManager.SetChickenInArea(false);
            }
        }
    }
}
