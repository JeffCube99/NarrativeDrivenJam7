using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardHandManager : MonoBehaviour
{
    [SerializeField] private ObjectPool cardHolderObjectPool;
    [SerializeField] private int maxCards;
    [Range(0.1f, Mathf.PI)] [SerializeField] private float maxAngle;
    [SerializeField] private float radius;
    [Range(0.1f, 1f)] [SerializeField] private float lerpSpeed;

    private List<GameObject> fullCardHolders;

    private void Start()
    {
        fullCardHolders = new List<GameObject>();
    }

    private void Update()
    {
        // remove card holders from fullCardHolders that are not holding cards.
        for (int i = fullCardHolders.Count - 1; i >= 0; i--)
        {
            if (fullCardHolders[i].transform.childCount == 0)
            {
                fullCardHolders[i].SetActive(false);
                fullCardHolders.RemoveAt(i);
            }
        }
        SpreadOutHand();
    }

    public void AddCardToHand(GameObject card)
    {
        if (fullCardHolders.Count < maxCards)
        {
            GameObject cardHolder = cardHolderObjectPool.Instantiate(Vector3.zero, Quaternion.identity, transform);
            card.transform.parent = cardHolder.transform;
            card.transform.localPosition = Vector3.zero;
            card.transform.localRotation = Quaternion.identity;
            fullCardHolders.Add(cardHolder);
        }
    }

    private void SpreadOutHand()
    {
        int numberOfCards = fullCardHolders.Count;
        float partialAngle = (maxAngle / maxCards)*numberOfCards;
        float minAngle = (Mathf.PI - partialAngle) / 2;
        for (int i = 0; i < fullCardHolders.Count; i++)
        {
            float angle = ((i+1) * partialAngle / (numberOfCards+1)) + minAngle;
            Vector3 newPosition = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius - radius, 0) + transform.position;
            fullCardHolders[i].transform.position = Vector3.Lerp(fullCardHolders[i].transform.position, newPosition, lerpSpeed);
            Quaternion newRotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg - 90);
            fullCardHolders[i].transform.rotation = Quaternion.Lerp(fullCardHolders[i].transform.rotation, newRotation, lerpSpeed); 
        }
    }
}