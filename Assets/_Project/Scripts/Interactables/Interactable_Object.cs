using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is basically just a script that u can put to an Object, and itll be considered an Object that can be interacted with, hence the name Interactable_Object. It has a public string that can be set in the editor, and a public function that returns the string. This is used in the SelectionManager script to display the name of the object when the player looks at it.
public class Interactable_Object : MonoBehaviour
{
    [field: SerializeField] public string ItemName { get; private set; }
    /* So instead of the old C++ looking ahh code
    public string GetItemName()
    {
        return ItemName;
    }
    We write like above, less code, faster, and more optimized
    */
}