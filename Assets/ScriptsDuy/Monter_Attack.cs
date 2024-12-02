using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monter_Attack : StateMachineBehaviour
{
 public float attackDuration = 2f; // Thời gian tấn công

    private float attackTimer;

    // Gọi khi trạng thái tấn công được khởi tạo
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackTimer = attackDuration; // Đặt thời gian tấn công
    }

    // Gọi liên tục khi quái tấn công
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackTimer -= Time.deltaTime; // Giảm thời gian tấn công

        if (attackTimer <= 0f)
        {
            animator.SetBool("IsAttack", false); // Sau khi tấn công xong, quay lại trạng thái chase
            animator.SetBool("IsRun", true); // Tiếp tục đuổi theo player
        }
    }

    // Gọi khi thoát trạng thái tấn công
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Có thể thêm hành động khi thoát trạng thái tấn công (nếu cần)
    }
}
