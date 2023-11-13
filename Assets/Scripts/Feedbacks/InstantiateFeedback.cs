using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InstantiateFeedback : GameFeedback
{
    public GameObject Prefab;

	public override IEnumerator Execute(GameEvent gameEvent, GameObject gameObject)
    {
        GameObject.Instantiate(Prefab, new Vector3(0, 0, 0), Quaternion.identity);
		yield break;
    }
}
