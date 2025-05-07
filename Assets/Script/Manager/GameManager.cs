using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{ 

    [SerializeField] private Player player; // Tham chiếu đến đối tượng Player trong game

    public Player Player => player;

   

    public void ThemKN(float expamount)
    {
        PlayerExp playerExp = player.GetComponent<PlayerExp>();
        playerExp.AddExp(expamount);
    }

    private void Update()
    {
        // Kiểm tra nếu người chơi nhấn phím R
        if (Input.GetKeyDown(KeyCode.R))
        {
            player.ResetPlayer(); // Gọi hàm ResetPlayer để reset trạng thái nhân vật
        }
    }
}
