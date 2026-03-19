using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using static UnityEngine.Rendering.DebugUI;

public class audioslider : MonoBehaviour
{
    [SerializeField]
    private AudioMixer Mixer;
    [SerializeField]
    private AudioSource AudioSource;
    [SerializeField]
    private TextMeshProUGUI ValueText;
    [SerializeField]
    private AudioMixMode MixMode;

    public void OnChangeSlider(float Value)
    {
        ValueText.SetText($"{Value.ToString("N4")}");
        {
            switch (MixMode)
            {
                case AudioMixMode.LinearAudioSourceVolume:
                    AudioSource.volume = Value;
                    break;
                case AudioMixMode.LinearmixerVolume:
                    Mixer.SetFloat("MusicVolume", (-80 + Value * 100));
                    break;
                case AudioMixMode.LogrithimicMixerVolume:
                    Mixer.SetFloat("Volume", Mathf.Log10(Value) * 20);
                    break;
            }
        }

    }

    public void SliderMainvol(float Volume)
    {
        Mixer.SetFloat("MusicVolume", Volume);
    }

    private void Start()
    {

    


        Mixer.SetFloat("Volume", Mathf.Log10(PlayerPrefs.GetFloat("Volume", 1) * 20));
    }
    public enum AudioMixMode
    {
        LinearAudioSourceVolume,
        LinearmixerVolume,
        LogrithimicMixerVolume

    }
}





