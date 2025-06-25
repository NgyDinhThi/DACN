using System;
using UnityEngine;

[CreateAssetMenu]
// Lớp lưu trữ thông tin các item có sẵn trong game
public class GameContents : ScriptableObject
{
    public InventoryItems[] GameItems; // Mảng chứa các item có sẵn trong game
}
