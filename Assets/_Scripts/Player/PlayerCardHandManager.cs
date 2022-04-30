using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardHandManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> cardHolders;
    [SerializeField] private int maxCards;
    [Range(0.1f, Mathf.PI)] [SerializeField] private float maxAngle;
    [SerializeField] private float radius;

    private List<GameObject> fullCardHolders;
    private Queue<GameObject> emptyCardHolders;

    private void Start()
    {
        emptyCardHolders = new Queue<GameObject>();
        foreach (GameObject cardHolder in cardHolders)
        {
            emptyCardHolders.Enqueue(cardHolder);
        }
        fullCardHolders = new List<GameObject>();
    }
    public void OnCardAddedToHand(GameObject card)
    {
        if (emptyCardHolders.Count > 0)
        {
            GameObject cardHolder = emptyCardHolders.Dequeue();
            card.transform.parent = cardHolder.transform;
            card.transform.localPosition = Vector3.zero;
            card.transform.localRotation = Quaternion.identity;
            fullCardHolders.Add(cardHolder);
        }
        OnHandModified();
    }

    public void OnCardRemovedFromHand()
    {
        for (int i = fullCardHolders.Count - 1; i >= 0; i--)
        {
            Debug.Log($"Scanning item {i}");
            if (fullCardHolders[i].transform.childCount == 0)
            {
                Debug.Log($"Child count reads 0 for item {i}");
                emptyCardHolders.Enqueue(fullCardHolders[i]);
                fullCardHolders.RemoveAt(i);
            }
        }
        OnHandModified();
    }

    private void OnHandModified()
    {
        SpreadOutHand();
    }

    private void SpreadOutHand()
    {
        foreach (GameObject cardHolder in cardHolders)
        {
            cardHolder.transform.localPosition = Vector3.zero;
            cardHolder.transform.localRotation = Quaternion.identity;
        }

        int numberOfCards = fullCardHolders.Count;
        float partialAngle = (maxAngle / maxCards)*numberOfCards;
        float minAngle = (Mathf.PI - partialAngle) / 2;
        for (int i = 0; i < fullCardHolders.Count; i++)
        {
            float angle = ((i+1) * partialAngle / (numberOfCards+1)) + minAngle;
            Vector3 newPosition = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius - radius, 0) + transform.position;
            fullCardHolders[i].transform.position = newPosition;
            fullCardHolders[i].transform.eulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg - 90); 
        }
    }
}
