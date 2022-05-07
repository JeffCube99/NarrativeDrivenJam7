using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ConditionalPartyCountScene", menuName = "ScriptableObjects/Scenes/ConditionalPartyCountScene")]
public class ConditionalPartyCountScene : BaseScene
{
    public PlayerState playerState;
    public List<ConditionalCountOutcome> possibleOutcomes;
    public BaseScene defaultOutcomeScene;

    [System.Serializable]
    public struct ConditionalCountOutcome
    {
        public int minPartyMembers;
        public BaseScene resultScene;
    }

    public override void StartScene()
    {
        foreach (ConditionalCountOutcome outcome in possibleOutcomes)
        {
            if (playerState.partyMembers.Count >= outcome.minPartyMembers)
            {
                outcome.resultScene.StartScene();
                return;
            }
        }
        defaultOutcomeScene.StartScene();
    }
}
