using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]  
    private float _speed; // Tốc độ di chuyển của kẻ địch

    [SerializeField]  
    private float _rotationSpeed; // Tốc độ xoay của kẻ địch

    [SerializeField]
    private float _screenBorder; // Khoảng cách từ mép màn hình để đổi hướng

    [SerializeField]
    private float _obstacleCheckCircleRadius; // Bán kính kiểm tra vật cản

    [SerializeField]
    private float _obstacleCheckDistance; // Khoảng cách kiểm tra vật cản

    [SerializeField]
    private LayerMask _obstacleLayerMask; // Lớp dùng để phát hiện vật cản

    private Rigidbody2D _rigidbody; // Thành phần Rigidbody2D của kẻ địch
    private PlayerAwarenessController _playerAwarenessController; // Kiểm tra nhận thức của kẻ địch về người chơi
    private Vector2 _targetDirection; // Hướng di chuyển mục tiêu hiện tại
    private float _changeDirectionCooldown; // Thời gian chờ trước khi đổi hướng ngẫu nhiên
    private Camera _camera; // Camera chính trong cảnh
    private RaycastHit2D[] _obstacleCollisions; // Mảng lưu kết quả va chạm với vật cản
    private float _obstacleAvoidanceCooldown; // Thời gian chờ trước khi thực hiện tránh vật cản lần tiếp theo
    private Vector2 _obstacleAvoidanceTargetDirection; // Hướng tạm thời để tránh vật cản

    private void Awake()
    {
        // Gán các thành phần cần thiết
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
        _targetDirection = transform.up; // Hướng ban đầu
        _camera = Camera.main; // Lấy camera chính
        _obstacleCollisions = new RaycastHit2D[10]; // Khởi tạo mảng chứa kết quả kiểm tra vật cản
    }

    private void FixedUpdate()
    {
        // Cập nhật hướng mục tiêu, xoay và di chuyển
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }

    private void UpdateTargetDirection()
    {
        HandleRandomDirectionChange(); // Đổi hướng ngẫu nhiên sau một khoảng thời gian
        HandlePlayerTargeting(); // Đổi hướng về phía người chơi nếu nhận thấy người chơi
        HandleObstacles(); // Tránh vật cản
        HandleEnemyOffScreen(); // Điều chỉnh hướng nếu sắp ra ngoài màn hình
    }

    private void HandleRandomDirectionChange()
    {
        _changeDirectionCooldown -= Time.deltaTime; // Giảm thời gian chờ

        if (_changeDirectionCooldown <= 0) // Nếu hết thời gian chờ
        {
            float angleChange = Random.Range(-90f, 90f); // Tạo góc ngẫu nhiên trong khoảng -90 đến 90 độ
            Quaternion rotation = Quaternion.AngleAxis(angleChange, transform.forward); // Tạo phép quay quanh trục z
            _targetDirection = rotation * _targetDirection; // Cập nhật hướng mục tiêu
            _changeDirectionCooldown = Random.Range(1f, 5f); // Đặt lại thời gian chờ ngẫu nhiên
        }
    }

    private void HandlePlayerTargeting()
    {
        if (_playerAwarenessController.AwareOfPlayer) // Nếu nhận biết được người chơi
        {
            _targetDirection = _playerAwarenessController.DirectionToPlayer; // Chuyển hướng về phía người chơi
        }
    }

    private void HandleEnemyOffScreen()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position); // Lấy vị trí của kẻ địch trên màn hình

        // Nếu sắp ra khỏi mép màn hình, đảo ngược hướng di chuyển theo trục x hoặc y
        if ((screenPosition.x < _screenBorder && _targetDirection.x < 0) ||
            (screenPosition.x > _camera.pixelWidth - _screenBorder && _targetDirection.x > 0))
        {
            _targetDirection = new Vector2(-_targetDirection.x, _targetDirection.y);
        }
        if ((screenPosition.y < _screenBorder && _targetDirection.y < 0) ||
            (screenPosition.y > _camera.pixelHeight - _screenBorder && _targetDirection.y > 0))
        {
            _targetDirection = new Vector2(_targetDirection.x, -_targetDirection.y);
        }
    }

    private void HandleObstacles()
    {
        _obstacleAvoidanceCooldown -= Time.deltaTime; // Giảm thời gian chờ tránh vật cản

        var contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(_obstacleLayerMask); // Áp dụng lớp để kiểm tra vật cản

        // Kiểm tra va chạm bằng CircleCast
        int numberOfCollisions = Physics2D.CircleCast(
            transform.position,
            _obstacleCheckCircleRadius,
            transform.up,
            contactFilter,
            _obstacleCollisions,
            _obstacleCheckDistance);

        for (int index = 0; index < numberOfCollisions; index++)
        {
            var obstacleCollision = _obstacleCollisions[index];

            if (obstacleCollision.collider.gameObject == gameObject) // Bỏ qua chính đối tượng kẻ địch
            {
                continue;
            }

            if (_obstacleAvoidanceCooldown <= 0) // Nếu hết thời gian chờ tránh vật cản
            {
                _obstacleAvoidanceTargetDirection = obstacleCollision.normal; // Lấy hướng ngược lại từ vật cản
                _obstacleAvoidanceCooldown = 0.5f; // Đặt lại thời gian chờ
            }

            var targetRotation = Quaternion.LookRotation(transform.forward, _obstacleAvoidanceTargetDirection); // Tính góc xoay mới
            var rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            _targetDirection = rotation * Vector2.up; // Cập nhật hướng mục tiêu
            break; // Chỉ xử lý va chạm đầu tiên
        }
    }

    private void RotateTowardsTarget()
    {
        // Tạo góc xoay mục tiêu về hướng hiện tại
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        _rigidbody.SetRotation(rotation); // Cập nhật góc quay của Rigidbody2D
    }

    private void SetVelocity()
    {
        // Cập nhật vận tốc theo hướng hiện tại và tốc độ
        _rigidbody.velocity = transform.up * _speed;
    }
}
