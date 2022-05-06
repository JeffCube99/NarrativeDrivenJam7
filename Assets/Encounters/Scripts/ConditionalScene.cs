using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ConditionalScene", menuName = "ScriptableObjects/Scenes/ConditionalScene")]
public class ConditionalScene : BaseScene
{
    [System.Serializable]
    public struct ConditionalOutcome
    {
        public List<Character> requiredCharacters;
        public BaseScene resultScene;
    }
    public PlayerState playerState;
    public List<ConditionalOutcome> possibleOutcomes;
    public BaseScene defaultOutcomeScene;

    public override void StartScene()
    {
        foreach (ConditionalOutcome outcome in possibleOutcomes)
        {
            int count = 0;
            foreach (Character character in outcome.requiredCharacters)
            {
                if (playerState.partyMembers.Contains(character))
                {
                    count += 1;
                }
            }
            if (count == outcome.requiredCharacters.Count)
            {
                outcome.resultScene.StartScene();
                return;
            }
        }
        defaultOutcomeScene.StartScene();
    }
}
