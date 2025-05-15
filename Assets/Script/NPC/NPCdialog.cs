using UnityEngine;


public enum InteractionType
{
    Quest,
    Shop,
    NormalTalk

}

[CreateAssetMenu(menuName ="NPC Dialog")]
public class NPCdialog : ScriptableObject
{
    [Header("Info")]
    public string Name;
    public Sprite Icon;


    [Header("Interaction")]
    public bool HasInteraction;
    public InteractionType InteractionType;

    [Header("Dialogue")]
    [TextArea] public string[] dialogue;


}
