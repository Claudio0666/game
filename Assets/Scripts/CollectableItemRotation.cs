using UnityEngine;

public class CollectableItemRotation : MonoBehaviour
{
    public float rotationSpeed = 90f; // Velocidade de rotação (graus por segundo)

    void Update()
    {
        // Gira o objeto ao redor de seu eixo Z (pode ser alterado para X ou Y)
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
