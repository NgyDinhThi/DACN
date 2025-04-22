using System;
using UnityEngine;


[Serializable]
public class FSMstate
{
    public string id;
    public FSMaction[] actions;
    public FSMtransition[] transitions; 


    public void UpdateState(EnemyBrain enemyBrain)
    {
        ChayLenh();
        ChayChuyenDoi(enemyBrain);
    }

    private void ChayLenh()
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act();
        }

    }

    private void ChayChuyenDoi(EnemyBrain enemyBrain)
    {
        if (transitions == null || transitions.Length <=0)
            return;
        for (int i = 0; i < transitions.Length; i++)
        {
            bool value = transitions[i].Quyetdinh.Decide();
            if (value) 
            {
                enemyBrain.ChangeState(transitions[i].TrueState); 
            }
            else
            {
                
                enemyBrain.ChangeState(transitions[i].FalseState); 
            }
        }

    }




}