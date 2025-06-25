using UnityEngine;

public enum InteractionType
{
    Quest,
    Shop,
    NormalTalk,
    Crafting
}

[CreateAssetMenu(menuName = "NPC Dialog")]
public class NPCdialog : ScriptableObject
{
    public string Name;         // tên NPC
    public Sprite Icon;         // icon đại diện cho NPC

    public bool HasInteraction;             // xác định NPC có tương tác đặc biệt không
    public InteractionType InteractionType; // kiểu tương tác: mua bán, nhiệm vụ, nói chuyện...

    public string Greeting;     // câu thoại chào ban đầu
    [TextArea] public string[] dialogue; // danh sách câu thoại sẽ hiển thị theo thứ tự
}