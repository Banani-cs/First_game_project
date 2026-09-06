using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class HidingTheText : MonoBehaviour
{
    public void HideText(string text)
    {
        Invoke(nameof(DisableObject), 3f);
    }

    public void DisableObject()
    {
        gameObject.SetActive(false);
    }
}
