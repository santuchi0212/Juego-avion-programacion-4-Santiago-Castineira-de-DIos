using UnityEngine;
using UnityEngine.UI;

public class ConfiguradorVolumen : MonoBehaviour
{
    public AudioData datosAudio;
    public Slider sliderVolumen;

    void Start()
    {
        if (datosAudio != null && sliderVolumen != null)
        {
            // Cargar el valor guardado
            sliderVolumen.value = datosAudio.volumenGeneral;
            AudioListener.volume = datosAudio.volumenGeneral;

            // Escuchar cambios del Slider en tiempo real
            sliderVolumen.onValueChanged.AddListener(CambiarVolumenGlobal);
        }
    }

    public void CambiarVolumenGlobal(float nuevoVolumen)
    {
        if (datosAudio != null)
        {
            datosAudio.volumenGeneral = nuevoVolumen;
            AudioListener.volume = nuevoVolumen;
        }
    }
}