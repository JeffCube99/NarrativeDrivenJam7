using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimatorController : MonoBehaviour
{
    [SerializeField] private GameObjectRuntimeSet npcRuntimeSet;
    [SerializeField] private Animator animatorComponent;
    [SerializeField] private Transform rootAnimationTransform;
    [SerializeField] private Character character;
    [SerializeField] private float setupLerpSpeed;


    private GameObject GetCharacterObjectFromRuntimeSet()
    {
        foreach (GameObject characterObject in npcRuntimeSet.items)
        {
            CharacterComponent characterComponent = characterObject.GetComponent<CharacterComponent>();
            if (characterComponent != null && characterComponent.character == character)
            {
                return characterObject;
            }
        }
        return null;
    }

    public void RunAnimation(string animationName)
    {
        GameObject characterObject = GetCharacterObjectFromRuntimeSet();
        if (characterObject != null)
        {
            StopAllCoroutines();
            StartCoroutine(MoveToOriginThenStartAnimation(characterObject, animationName));
        }
    }

    IEnumerator MoveToOriginThenStartAnimation(GameObject characterObject, string animationName)
    {
        animatorComponent.Play(animationName);
        yield return new WaitForEndOfFrame();
        animatorComponent.enabled = false;
        Transform characterTransform = characterObject.transform;
        characterTransform.parent = rootAnimationTransform;
        while (characterTransform.localPosition.magnitude > 0.1 || Quaternion.Angle(characterTransform.localRotation, Quaternion.identity) > 0.1)
        {
            characterTransform.localPosition = Vector3.Lerp(characterTransform.localPosition, Vector3.zero, setupLerpSpeed);
            characterTransform.localRotation = Quaternion.Lerp(characterTransform.localRotation, Quaternion.identity, setupLerpSpeed);
            yield return null;
        }
        characterTransform.localPosition = Vector3.zero;
        characterTransform.localRotation = Quaternion.identity;
        animatorComponent.enabled = true;
    }

}
