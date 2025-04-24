using System;
using UnityEngine;


[Serializable]
public class FSMstate
{
    public string id; // ID định danh cho trạng thái
    public FSMaction[] actions; // Danh sách hành động sẽ được thực hiện trong trạng thái này
    public FSMtransition[] transitions; // Danh sách các điều kiện chuyển trạng thái

    // Hàm cập nhật trạng thái, gọi mỗi frame
    public void UpdateState(EnemyBrain enemyBrain)
    {
        ChayLenh(); // Thực hiện các hành động trong trạng thái hiện tại
        ChayChuyenDoi(enemyBrain); // Kiểm tra và thực hiện chuyển đổi trạng thái nếu có
    }

    // Gọi tất cả các hành động trong danh sách actions
    private void ChayLenh()
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(); // Gọi phương thức Act() của mỗi hành động
        }

    }

    // Kiểm tra điều kiện chuyển trạng thái và thực hiện nếu thỏa mãn
    private void ChayChuyenDoi(EnemyBrain enemyBrain)
    {
        // Nếu danh sách chuyển đổi không có hoặc rỗng thì không làm gì
        if (transitions == null || transitions.Length <= 0)
            return;

        for (int i = 0; i < transitions.Length; i++)
        {
            bool value = transitions[i].Quyetdinh.Decide(); // Kiểm tra điều kiện quyết định
            if (value)
            {
                enemyBrain.ChangeState(transitions[i].TrueState); // Chuyển sang trạng thái nếu điều kiện đúng
            }
            else
            {
                enemyBrain.ChangeState(transitions[i].FalseState); // Chuyển sang trạng thái khác nếu điều kiện sai
            }
        }

    }
}