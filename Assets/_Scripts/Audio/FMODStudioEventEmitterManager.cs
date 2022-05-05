using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[CreateAssetMenu(fileName = "FMODStudioEventEmitterManager", menuName = "ScriptableObjects/Audio/FMODStudioEventEmitterManager")]
public class FMODStudioEventEmitterManager : ScriptableObject
{
    public int maxEmitters;
    public GameObject musicStudioEventEmitterPrefab;
    private Queue<StudioEventEmitter> studioEventEmitters2D;
    private StudioEventEmitter musicStudioEventEmitter;

    private StudioEventEmitter InitializeMusicEmitter()
    {
        GameObject musicGameObject = Instantiate(musicStudioEventEmitterPrefab);
        return musicGameObject.GetComponent<StudioEventEmitter>();
    }

    public StudioEventEmitter GetMusicStudioEventEmitter()
    {
        if (musicStudioEventEmitter == null)
        {
            musicStudioEventEmitter = InitializeMusicEmitter();
        }
        return musicStudioEventEmitter;
    }

    private void Initialize2DEmitters()
    {
        StudioListener listener = FindObjectOfType<StudioListener>();
        if (listener == null)
        {
            Debug.LogError("Cannot find a audio listener in the scene!");
            return;
        }

        GameObject audioListenerGameObject = listener.gameObject;

        if (studioEventEmitters2D == null)
        {
            studioEventEmitters2D = new Queue<StudioEventEmitter>();
        }
        else
        {
            foreach (StudioEventEmitter queueSource in studioEventEmitters2D)
            {
                if (queueSource != null)
                {
                    Destroy(queueSource);
                }
            }
            studioEventEmitters2D.Clear();
        }

        for (int i = 0; i < maxEmitters; i++)
        {
            StudioEventEmitter source = audioListenerGameObject.AddComponent(typeof(StudioEventEmitter)) as StudioEventEmitter;
            studioEventEmitters2D.Enqueue(source);
        }
    }

    public StudioEventEmitter Get2DEmitter()
    {
        // If no 2d audiosources exist we create them
        if (studioEventEmitters2D == null || studioEventEmitters2D.Count == 0)
        {
            Initialize2DEmitters();
        }

        StudioEventEmitter emitter = studioEventEmitters2D.Dequeue();

        // If a source in the queue is null it is likely the previous
        // listener we attach our sources to has been destroyed so we re initialize the 
        // 2d audio sources
        if (emitter == null)
        {
            Initialize2DEmitters();
            emitter = studioEventEmitters2D.Dequeue();
        }
        studioEventEmitters2D.Enqueue(emitter);
        return emitter;
    }
}
