using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[CreateAssetMenu(fileName = "FMODSoundAsset", menuName = "ScriptableObjects/Audio/FMODSoundAsset")]
public class FMODSoundAsset : ScriptableObject
{
    [EventRef]
    public string soundEvent = "";
    public FMODStudioEventEmitterManager emitterManager;

    public void PlayAudioAsMusic()
    {
        StudioEventEmitter emitter = emitterManager.GetMusicStudioEventEmitter();
        PlayAudioFromStudioEventEmitter(emitter);
    }

    public void PlayAudioAtAudioListener()
    {
        StudioEventEmitter emitter = emitterManager.Get2DEmitter();
        PlayAudioFromStudioEventEmitter(emitter);
    }

    public void PlayAudioFromStudioEventEmitter(StudioEventEmitter emitter)
    {
        emitter.Stop();
        emitter.ChangeEvent(soundEvent);
        emitter.Play();
    }
}
