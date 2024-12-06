using UnityEngine;

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

    void Start()
    {
        animator = GetComponent<Animator>(); // Lấy Animator từ nhân vật
        footstepAudio = GetComponent<AudioSource>(); // Lấy AudioSource trên nhân vật

        EquipSword(); // Mặc định sử dụng kiếm
    }

    void Update()
    {
        // Nhấn chuột phải để di chuyển
        if (Input.GetMouseButtonDown(1))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0; // Giữ Z ở mức 0
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

        // Nhấn Q để đổi vũ khí
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isUsingBow)
                EquipSword();
            else
                EquipBow();
        }

        // Tấn công bằng cung (bắn mũi tên)
        if (Input.GetMouseButtonDown(0) && isUsingBow) // Nếu nhấn chuột trái và đang sử dụng cung
        {
            // Gọi hàm bắn mũi tên
            FindObjectOfType<PlayerAttack>().ShootArrow(); // Kiểm tra xem bạn có gọi đúng hàm bắn mũi tên hay không
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
        if (direction > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Quay phải
        }
        else if (direction < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Quay trái
        }
    }

    void HandleFootstepSound()
    {
        if (isMoving)
        {
            if (!footstepAudio.isPlaying) // Nếu âm thanh chưa phát
            {
                footstepAudio.Play(); // Phát âm thanh bước chân
            }
        }
        else
        {
            if (footstepAudio.isPlaying) // Nếu âm thanh đang phát
            {
                footstepAudio.Stop(); // Dừng âm thanh bước chân
            }
        }
    }

    void EquipSword()
    {
        isUsingBow = false;
        sword.SetActive(true);  // Bật kiếm
        bow.SetActive(false);   // Tắt cung
        animator.SetBool("isUsingBow", false); // Cập nhật Animator
    }

    void EquipBow()
    {
        isUsingBow = true;
        sword.SetActive(false); // Tắt kiếm
        bow.SetActive(true);    // Bật cung
        animator.SetBool("isUsingBow", true);  // Cập nhật Animator
    }

}


