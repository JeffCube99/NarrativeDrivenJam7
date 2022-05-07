using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "FMODBusMixerManager", menuName = "ScriptableObjects/Audio/FMODBusMixerManager")]
public class FMODBusMixerManager : ScriptableObject
{
    public string busPath;

    private FMOD.Studio.Bus GetBus()
    {
        return RuntimeManager.GetBus(busPath);
    }

    private float DecibelToLinear(float dB)
    {
        float linear = Mathf.Pow(10.0f, dB / 20f);
        return linear;
    }

    public void setBusLevel(float dB)
    {
        FMOD.Studio.Bus bus = GetBus();
        bus.setVolume(DecibelToLinear(dB));
    }

    public void setSliderValue(Slider slider)
    {
        FMOD.Studio.Bus bus = GetBus();
        float volume;
        bus.getVolume(out volume);
        slider.value = volume;
    }
}
