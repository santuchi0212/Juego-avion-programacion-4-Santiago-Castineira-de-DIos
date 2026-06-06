using UnityEngine;
using UnityEngine.UI; // Obligatorio para componentes UI
using UnityEngine.SceneManagement; // Obligatorio para reiniciar la escena

public class UIManager : MonoBehaviour
{
    [Header("Paneles de la Interfaz")]
    public GameObject panelInicio;
    public GameObject panelMejoras;
    public GameObject panelVictoria;
    public GameObject panelDerrota;

    [Header("Componentes de Texto y Barras")]
    public Slider barraProgreso;
    public Text textoVidas;
    public Text textoNivel;

    void Start()
    {
        // 1. Encendemos el menú de inicio y apagamos los de fin de juego
        if (panelInicio != null) panelInicio.SetActive(true);
        if (panelMejoras != null) panelMejoras.SetActive(false);
        if (panelVictoria != null) panelVictoria.SetActive(false);
        if (panelDerrota != null) panelDerrota.SetActive(false);

        // 2. NUEVO: Ocultamos los textos y la barra para que NO se vean en el menú de inicio
        if (textoVidas != null) textoVidas.gameObject.SetActive(false);
        if (textoNivel != null) textoNivel.gameObject.SetActive(false);
        if (barraProgreso != null) barraProgreso.gameObject.SetActive(false);

        Time.timeScale = 0f; // Pausa inicial
    }

    public void EmpezarJuego()
    {
        if (panelInicio != null) panelInicio.SetActive(false);

        // NUEVO: Mostramos la interfaz de juego ahora que el jugador va a despegando
        if (textoVidas != null) textoVidas.gameObject.SetActive(true);
        if (textoNivel != null) textoNivel.gameObject.SetActive(true);
        if (barraProgreso != null) barraProgreso.gameObject.SetActive(true);

        Time.timeScale = 1f;
    }

    public void PerderJuego()
    {
        if (panelDerrota != null) panelDerrota.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GanarJuego()
    {
        if (panelVictoria != null) panelVictoria.SetActive(true);
        Time.timeScale = 0f;
    }

    public void MostrarMenuMejoras()
    {
        if (panelMejoras != null) panelMejoras.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ElegirMejoraVelocidad()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null) player.speed += 5f;
        ReanudarTrasMejora();
    }

    public void ElegirMejoraDisparo()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null) player.fireRate = Mathf.Max(0.05f, player.fireRate - 0.08f);
        ReanudarTrasMejora();
    }

    private void ReanudarTrasMejora()
    {
        if (panelMejoras != null) panelMejoras.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ActualizarBarraNivel(int chatarraActual, int chatarraMaxima)
    {
        if (barraProgreso != null)
        {
            barraProgreso.maxValue = chatarraMaxima;
            barraProgreso.value = chatarraActual;
        }
    }

    public void ActualizarVidas(int vidasActuales)
    {
        if (textoVidas != null) textoVidas.text = "Vidas: " + vidasActuales;
    }

    public void ActualizarNivel(int nivelActual)
    {
        if (textoNivel != null) textoNivel.text = "Nivel: " + nivelActual;
    }

    public void VolverAlMenuInicio()
    {
        Time.timeScale = 1f;
        // Al recargar la escena, el Start() de arriba se volverá a ejecutar y ocultará todo automáticamente
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReintentarJuego()
    {
        if (panelDerrota != null) panelDerrota.SetActive(false);
        if (panelVictoria != null) panelVictoria.SetActive(false);

        // NUEVO: Nos aseguramos de que la interfaz se vuelva a activar si le das a Reintentar directamente
        if (textoVidas != null) textoVidas.gameObject.SetActive(true);
        if (textoNivel != null) textoNivel.gameObject.SetActive(true);
        if (barraProgreso != null) barraProgreso.gameObject.SetActive(true);

        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.lives = 5;
            player.currentScrap = 0;
            player.currentLevel = 1;

            ActualizarVidas(player.lives);
            ActualizarNivel(player.currentLevel);
            ActualizarBarraNivel(player.currentScrap, player.scrapToLevelUp);

            player.transform.position = new Vector3(0f, player.transform.position.y, player.transform.position.z);
        }

        Enemy[] enemigosViejos = FindObjectsOfType<Enemy>();
        foreach (Enemy e in enemigosViejos) Destroy(e.gameObject);

        Projectile[] balasViejas = FindObjectsOfType<Projectile>();
        foreach (Projectile b in balasViejas) Destroy(b.gameObject);

        Scrap[] chatarrasViejas = FindObjectsOfType<Scrap>();
        foreach (Scrap c in chatarrasViejas) Destroy(c.gameObject);

        Time.timeScale = 1f;
    }
}