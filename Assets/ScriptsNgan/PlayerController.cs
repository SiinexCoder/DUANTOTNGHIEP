using UnityEngine;
using System.Collections.Generic; 
using System.Collections;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Tốc độ di chuyển
    private Vector2 moveDirection; // Hướng di chuyển
    private Vector3 targetPosition; // Vị trí mục tiêu
    private bool isMoving = false; // Trạng thái di chuyển
    private Animator animator; // Animator của nhân vật
    private AudioSource footstepAudio; // Âm thanh bước chân

    public GameObject sword; // Kiếm
    public GameObject bow;   // Cung
    private bool isUsingBow = false; // Trạng thái vũ khí hiện tại
    public QuestManager questManager;
    public ParticleSystem runDustEffect; // Tham chiếu đến hiệu ứng bụi

    private PlayerAttack playerAttack; // Tham chiếu đến PlayerAttack

    

    void Start()
    {
        animator = GetComponent<Animator>();
        footstepAudio = GetComponent<AudioSource>();
        playerAttack = GetComponent<PlayerAttack>(); // Lấy tham chiếu PlayerAttack

        EquipSword(); // Mặc định sử dụng kiếm
    }

    void Update()
    {
        // Kiểm tra chuột phải để di chuyển
        if (Input.GetMouseButtonDown(1))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0;
            moveDirection = (targetPosition - transform.position).normalized;
            isMoving = true;
        }

        // Kiểm tra nếu đã tới vị trí mục tiêu
        if (isMoving && Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;
        }

        // Xoay nhân vật
        if (isMoving)
        {
            RotatePlayer(targetPosition);
        }

        // Phát âm thanh bước chân
        HandleFootstepSound();

        // Cập nhật Animator
        animator.SetBool("isRunning", isMoving);

        // Đổi vũ khí với phím Q
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isUsingBow)
                EquipSword();
            else
                EquipBow();
        }

        // Tấn công bằng cung
        if (Input.GetMouseButtonDown(0) && isUsingBow) // Bắn mũi tên
        {
            if (playerAttack != null)
            {
                playerAttack.ShootArrow(); // Gọi hàm bắn mũi tên từ PlayerAttack
            }
        }
        
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            transform.position += (Vector3)moveDirection * moveSpeed * Time.fixedDeltaTime;
        }
    }

    void RotatePlayer(Vector3 targetPosition)
    {
        float direction = targetPosition.x - transform.position.x;
        transform.localScale = new Vector3(direction > 0 ? 1 : -1, 1, 1); // Quay trái/phải
    }

    // Xử lý âm thanh bước chân
    void HandleFootstepSound()
    {
        if (isMoving)
        {
            if (!footstepAudio.isPlaying) footstepAudio.Play();
            if (!runDustEffect.isPlaying) runDustEffect.Play(); // Bật hiệu ứng bụi
        }
        else
        {
            if (footstepAudio.isPlaying) footstepAudio.Stop();
            if (runDustEffect.isPlaying) runDustEffect.Stop(); // Tắt hiệu ứng bụi
        }
    }

    void EquipSword()
    {
        isUsingBow = false;
        sword.SetActive(true); // Bật kiếm
        bow.SetActive(false);  // Tắt cung
        animator.SetBool("isUsingBow", false); // Cập nhật Animator
    }

    void EquipBow()
    {
        isUsingBow = true;
        sword.SetActive(false); // Tắt kiếm
        bow.SetActive(true);    // Bật cung
        animator.SetBool("isUsingBow", true); // Cập nhật Animator
    }
void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Enemy"))
    {
        // Giảm máu nhân vật
        PlayerHealth playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(10); // Ví dụ: giảm 10 máu
        }

        // Kích hoạt animation bị thương
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Hurt"); // Giả sử bạn đã tạo trigger "Hurt" trong Animator
        }
    }
}


    

    void OnTriggerEnter2D(Collider2D other)
    {
        // Truy cập danh sách nhiệm vụ của scene hiện tại thông qua questManager
        List<QuestManager.Quest> currentQuests = questManager.currentSceneQuests;

        // Kiểm tra danh sách nhiệm vụ có phần tử không
        if (currentQuests.Count > 0)
        {
            if (other.CompareTag("DiamondBlue"))
            {
                Destroy(other.gameObject);
                questManager.UpdateQuestProgress("Thu thập kim cương xanh", currentQuests[0].currentItemCount + 1);
            }
            if (other.CompareTag("DiamondRed"))
            {
                Destroy(other.gameObject);
                questManager.UpdateQuestProgress("Thu thập kim cương đỏ", currentQuests[1].currentItemCount + 1);
            }
            else if (other.CompareTag("SecretItem"))
            {
                Destroy(other.gameObject);
                questManager.UpdateQuestProgress("Tìm vật phẩm bí ẩn", currentQuests[2].currentItemCount + 1);
            }else if (other.CompareTag("Leaf"))
            {
                Destroy(other.gameObject);
                questManager.UpdateQuestProgress("Thu thập lá thuốc", currentQuests[3].currentItemCount + 1);
            }
            // Thêm các điều kiện cho các nhiệm vụ khác tại đây
        }
    }
    
}






