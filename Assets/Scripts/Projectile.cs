using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public string targetTag = "Enemy"; // "Enemy" para balas del jugador, "Player" para balas enemigas

    void Update()
    {
        // Dirección de la bala en base a quién va dirigida
        if (targetTag == "Enemy")
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World); // Al fondo
        }
        else
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World); // Al frente
        }

        Destroy(gameObject, 3f); // Autodestrucción para optimizar
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            // SI LA BALA ERA PARA EL ENEMIGO
            if (targetTag == "Enemy")
            {
                Enemy enemigo = other.GetComponent<Enemy>();
                if (enemigo != null) enemigo.Die();
            }
            // SI LA BALA ERA PARA EL JUGADOR
            else if (targetTag == "Player")
            {
                PlayerController jugador = other.GetComponent<PlayerController>();
                if (jugador != null) jugador.TakeDamage(1); // La bala quita 1 vida
            }

            Destroy(gameObject); // Destruir la bala tras impactar
        }
    }
}