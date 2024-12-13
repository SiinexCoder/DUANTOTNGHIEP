using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 moveDirection;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private Animator animator;
    private AudioSource footstepAudio;
    public GameObject sword;
    public GameObject bow;
    private bool isUsingBow = false;
    public QuestManager questManager;
    public ParticleSystem runDustEffect;
    private PlayerAttack playerAttack;
    private bool isNearTreasureChest = false;  // Kiểm tra nếu nhân vật gần rương kho báu
    private GameObject nearbyTreasureChest;  // Đối tượng rương kho báu gần nhất


    void Start()
    {
        animator = GetComponent<Animator>();
        footstepAudio = GetComponent<AudioSource>();
        playerAttack = GetComponent<PlayerAttack>();
        EquipSword();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0;
            moveDirection = (targetPosition - transform.position).normalized;
            isMoving = true;
        }

        if (isMoving && Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;
        }

        if (isMoving)
        {
            RotatePlayer(targetPosition);
        }

        HandleFootstepSound();
        animator.SetBool("isRunning", isMoving);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isUsingBow)
                EquipSword();
            else
                EquipBow();
        }

        if (Input.GetMouseButtonDown(0) && isUsingBow)
        {
            if (playerAttack != null)
            {
                playerAttack.ShootArrow();
            }
        }
        if (Input.GetKeyDown(KeyCode.F) && isNearTreasureChest)
        {
            OpenTreasureChest();  // Gọi phương thức mở rương
        }
    }
    void OpenTreasureChest()
{
    if (questManager != null)
    {
        // Cập nhật tiến trình nhiệm vụ "Tìm kiếm kho báu"
        questManager.UpdateQuestProgress("Tìm kiếm kho báu", 1);

        // Thực hiện hành động mở rương (ví dụ, ẩn rương, phát hiệu ứng, v.v.)
        if (nearbyTreasureChest != null)
        {

            // Xóa rương kho báu
            Destroy(nearbyTreasureChest);  // Xóa rương kho báu
            Debug.Log("Rương kho báu đã được mở!");
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
        transform.localScale = new Vector3(direction > 0 ? 1 : -1, 1, 1);
    }

    void HandleFootstepSound()
    {
        // Kiểm tra nếu nhân vật đang di chuyển và hiệu ứng bụi chưa được bật
        if (isMoving)
        {
            if (!footstepAudio.isPlaying)
                footstepAudio.Play();
            if (!runDustEffect.isPlaying) // Bật hiệu ứng bụi khi di chuyển
                runDustEffect.Play();
        }
        else
        {
            // Nếu không di chuyển, tắt âm thanh và hiệu ứng bụi
            if (footstepAudio.isPlaying)
                footstepAudio.Stop();
            if (runDustEffect.isPlaying) // Tắt hiệu ứng bụi khi không di chuyển
                runDustEffect.Stop();
        }
    }


    public void EquipSword()
    {
        sword.SetActive(true);
        bow.SetActive(false);
        isUsingBow = false;
        animator.SetBool("isUsingBow", false);
    }

    public void EquipBow()
    {
        sword.SetActive(false);
        bow.SetActive(true);
        isUsingBow = true;
        animator.SetBool("isUsingBow", true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Treasure Chest"))
        {
            isNearTreasureChest = true;
            nearbyTreasureChest = other.gameObject;  // Lưu đối tượng rương kho báu
        }
        if (other.CompareTag("Diamond Blue"))
        {
            Destroy(other.gameObject);
            questManager.UpdateQuestProgress("Nhặt kim cương xanh", 1);
        }
        if (other.CompareTag("Diamond Red"))
        {
            Destroy(other.gameObject);
            questManager.UpdateQuestProgress("Nhặt kim cương đỏ", 1);
        }
        if (other.CompareTag("Secret Item"))
        {
            Destroy(other.gameObject);
            questManager.UpdateQuestProgress("Nhặt vật phẩm bí ẩn", 1);
        }
        if (other.CompareTag("Leaf"))
        {
            Destroy(other.gameObject);
            questManager.UpdateQuestProgress("Nhặt lá thuốc", 1);
        }
        if (other.CompareTag("Potion"))
        {
            Destroy(other.gameObject);
            questManager.UpdateQuestProgress("Nhặt bình thuốc", 1);
        }
        if (other.CompareTag("Healing"))
        {
            Destroy(other.gameObject);
            questManager.UpdateQuestProgress("Nhặt thuốc hồi phục", 1);
        }
        if (other.CompareTag("Antidote"))
        {
            Destroy(other.gameObject);
            questManager.UpdateQuestProgress("Nhặt thuốc giải", 1);
        }
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            questManager.UpdateQuestProgress("Nhặt đồng vàng", 1);
        }
        if (other.CompareTag("Star"))
        {
            Destroy(other.gameObject);
            questManager.UpdateQuestProgress("Nhặt ngôi sao", 1);
        }

    }
    void OnTriggerExit2D(Collider2D other)
    {
        // Kiểm tra khi người chơi ra khỏi khu vực rương kho báu
        if (other.CompareTag("Treasure Chest"))
        {
            isNearTreasureChest = false;
            nearbyTreasureChest = null;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            PlayerHealth playerHealth = GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10);
            }

            Animator animator = GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("Hurt");
            }

        }
    }
}


