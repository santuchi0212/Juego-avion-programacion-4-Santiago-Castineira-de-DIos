using UnityEngine;

public class Scrap : MonoBehaviour
{
    public float speed = 10f;
    public int scrapValue = 25; 

    void Update()
    {

        transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);


        Destroy(gameObject, 8f);
    }

    void OnTriggerEnter(Collider other)
    {

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