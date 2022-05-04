using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawnerController : MonoBehaviour
{
    [SerializeField] private List<ObjectPool> cardObjectPools;
    [SerializeField] private PlayerCardHandManager handManager;
    private List<GameObject> spawnedGameObjects;

    private void Start()
    {
        spawnedGameObjects = new List<GameObject>();
    }

    public void OnSpawnCard()
    {
        if (cardObjectPools.Count > 0 && handManager != null)
        {
            Debug.Log("Spawning Card!");
            ObjectPool cardObjectPool = cardObjectPools[Random.RandomRange(0, cardObjectPools.Count)];
            GameObject card = cardObjectPool.Instantiate(Vector3.zero, Quaternion.identity);
            handManager.AddCardToHand(card);
            spawnedGameObjects.Add(card);
        }
    }

    public void OnRemoveCard()
    {
        if (spawnedGameObjects.Count > 0)
        {
            spawnedGameObjects[0].transform.parent = null;
            Destroy(spawnedGameObjects[0]);
            spawnedGameObjects.RemoveAt(0);
        }
    }
}
