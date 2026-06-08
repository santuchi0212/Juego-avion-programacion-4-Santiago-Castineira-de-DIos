using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public string targetTag = "Enemy"; 

    void Update()
    {

        if (targetTag == "Enemy")
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World); 
        }
        else
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World); 
        }

        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {

            if (targetTag == "Enemy")
            {
                Enemy enemigo = other.GetComponent<Enemy>();
                if (enemigo != null) enemigo.Die();
            }

            else if (targetTag == "Player")
            {
                PlayerController jugador = other.GetComponent<PlayerController>();
                if (jugador != null) jugador.TakeDamage(1); // La bala quita 1 vida
            }

            Destroy(gameObject); 
        }
    }
}