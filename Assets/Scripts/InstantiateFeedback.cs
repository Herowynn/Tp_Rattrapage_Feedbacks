using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateFeedback : GameFeedback
{
    public GameObject Prefab;

    public override IEnumerator Execute(GameEvent gameEvent)
    {
        GameObject.Instantiate(Prefab, gameEvent.GameObject.transform.position, Quaternion.identity);
        yield break;
    }
}
