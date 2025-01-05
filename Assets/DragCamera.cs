using UnityEngine;

public class DragCamera : MonoBehaviour
{
    public Camera camera2D; // Camera 2D cần di chuyển
    public float dragSpeed = 1f; // Tốc độ kéo camera

    private Vector3 dragOrigin; // Lưu vị trí bắt đầu kéo
    private bool isDragging = false; // Kiểm tra có đang kéo không
    public Texture2D draggingCursor;
    public Texture2D normalCursor;

    void Update()
    {
        // Kiểm tra khi nhấn nút chuột trái
        if (Input.GetMouseButtonDown(1))
        {
            // Lưu vị trí chuột tại thời điểm bắt đầu kéo
            dragOrigin = camera2D.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
            Cursor.SetCursor(draggingCursor, Vector2.zero, CursorMode.Auto);
        }

        // Khi nhả nút chuột
        if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;
            Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
        }

        // Di chuyển camera khi đang kéo
        if (isDragging)
        {
            // Vị trí hiện tại của chuột
            Vector3 currentMousePosition = camera2D.ScreenToWorldPoint(Input.mousePosition);

            // Tính toán khoảng cách di chuyển
            Vector3 difference = dragOrigin - currentMousePosition;

            // Di chuyển camera ngược hướng với chuột
            camera2D.transform.position += difference;

            // Cập nhật lại vị trí chuột ban đầu để tránh giật
            dragOrigin = camera2D.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
