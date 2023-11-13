using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaitFeedback : GameFeedback
{
    [SerializeField] private float _delay;

    public override IEnumerator Execute(GameEvent gameEvent, GameObject gameObject)
    {
        yield return new WaitForSeconds(_delay);
    }
}
