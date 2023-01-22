using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class InternalTools
{
    private static IEnumerator ActionDelayed(Action action, float dalay = 0f)
    {
        yield return new WaitForSeconds(dalay);
        action.Invoke();
    }
}
