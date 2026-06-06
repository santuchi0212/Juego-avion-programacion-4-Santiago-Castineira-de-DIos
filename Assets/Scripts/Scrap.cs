using UnityEngine;

public class Scrap : MonoBehaviour
{
    public float speed = 10f;
    public int scrapValue = 25; // Cuánta chatarra da cada pieza

    void Update()
    {
        // Se mueve en la misma dirección que los enemigos (hacia la cámara)
        transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);

        // Se destruye después de 8 segundos si no la atrapas, perdiéndose en el vacío
        Destroy(gameObject, 8f);
    }

    void OnTriggerEnter(Collider other)
    {
        // Si choca contra el jugador, le suma la chatarra y desaparece
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.CollectScrap(scrapValue);
            }
            Destroy(gameObject);
        }
    }
}