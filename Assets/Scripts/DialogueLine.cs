using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public Sprite portrait;
    public string charSFX;

    [TextArea]
    public string text;
}