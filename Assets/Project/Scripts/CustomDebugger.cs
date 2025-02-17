using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomDebugger : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI textUI;

    static CustomDebugger instance;

    public static void log(string s)
    {
        instance.textUI.text += "\n" + s;
    }
    private void Start()
    {
        instance = this;
    }

}
