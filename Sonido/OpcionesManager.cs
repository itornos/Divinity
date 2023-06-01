using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class OpcionesManager : MonoBehaviour
{
    [SerializeField] AudioMixer controlador;

    [SerializeField] AudioMixerGroup masterGroup;
    [SerializeField] AudioMixerGroup musicGroup;
    [SerializeField] AudioMixerGroup effectsGroup;

    [SerializeField] Dropdown selectorResoluciones;
    [SerializeField] Resolution[] resoluciones;
    [SerializeField] Resolution resolution;

    private void Start()
    {
        resoluciones = Screen.resolutions;
        selectorResoluciones.ClearOptions();

        int currentResolucion = 0;

        List<string> opciones  = new List<string>();

        for (int i = 0; i < resoluciones.Length; i++) {
            string opcion = resoluciones[i].width + "x" + resoluciones[i].height;
            opciones.Add(opcion);

            if (resoluciones[i].width == Screen.currentResolution.width && resoluciones[i].height == Screen.currentResolution.height) {
                currentResolucion = i;
            }
        }

        selectorResoluciones.AddOptions(opciones);
        selectorResoluciones.value = currentResolucion;
        resolution = resoluciones[currentResolucion];
        selectorResoluciones.RefreshShownValue();

        SetEffectsVolume(-1); SetMusicVolume(-2); SetMasterVolume(-3);
    }

    public void SetMasterVolume(float volume)
    {
        controlador.SetFloat(masterGroup.name + "Volume", volume);
    }


    public void SetMusicVolume(float volume)
    {
        controlador.SetFloat(musicGroup.name + "Volume", volume);
    }

    // Método para ajustar el volumen de los efectos de sonido
    public void SetEffectsVolume(float volume)
    {
        controlador.SetFloat(effectsGroup.name + "Volume", volume);
    }

    public void setCalidad(int valor) {
        QualitySettings.SetQualityLevel(valor);
    }

    public void setResolucion(int valor)
    {
        resolution = resoluciones[valor];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void setModoVentana(int valor) {
        switch (valor) {
            case 0:
                setFullScreen();
                return;
            case 1:
                setVentana();
                return;
            case 2:
                setVentana();
                return;
        }
    }

    private void setFullScreen() {
        Screen.fullScreen = true;
    }

    private void setVentana() {
        int screenWidth = Screen.currentResolution.width;
        int screenHeight = Screen.currentResolution.height;
        Screen.SetResolution(screenWidth, screenHeight, false);
    }
}
