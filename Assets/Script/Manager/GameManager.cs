using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player; // Tham chiếu đến đối tượng Player trong game

    private void Update()
    {
        // Kiểm tra nếu người chơi nhấn phím R
        if (Input.GetKeyDown(KeyCode.R))
        {
            player.ResetPlayer(); // Gọi hàm ResetPlayer để reset trạng thái nhân vật
        }
    }
}
