using UnityEngine;

public class AIController : MonoBehaviour
{
    public GameController gameController;

    private const int WinLength = 5;

    public void PlayTurn()
    {
        if (gameController.CurrentPlayer == 2)
        {
            int bestX = -1, bestY = -1;
            int bestScore = int.MinValue;

            // Duyệt qua toàn bộ bàn cờ
            for (int x = 0; x < Board.Size; x++)
            {
                for (int y = 0; y < Board.Size; y++)
                {
                    if (gameController.Board.Grid[x, y] == 0) // Nếu ô trống
                    {
                        int aiScore = EvaluatePosition(x, y, 2);  // Điểm cho AI
                        int playerScore = EvaluatePosition(x, y, 1); // Điểm để chặn người chơi
                        int score = Mathf.Max(aiScore, playerScore); // Chọn điểm cao nhất

                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestX = x;
                            bestY = y;
                        }
                    }
                }
            }

            // Đặt marker tại vị trí tốt nhất
            if (bestX != -1 && bestY != -1)
            {
                gameController.Board.PlaceMarker(bestX, bestY, 2);
                gameController.DrawFlag(bestX, bestY, 2);
                gameController.EndTurn(bestX, bestY);
            }
        }
    }

    private int EvaluatePosition(int x, int y, int player)
    {
        int score = 0;

        // Đánh giá theo 4 hướng
        score += EvaluateDirection(x, y, player, 1, 0);  // Ngang
        score += EvaluateDirection(x, y, player, 0, 1);  // Dọc
        score += EvaluateDirection(x, y, player, 1, 1);  // Chéo \
        score += EvaluateDirection(x, y, player, 1, -1); // Chéo /

        return score;
    }

    private int EvaluateDirection(int x, int y, int player, int dx, int dy)
    {
        int count = 1; // Đếm quân cờ của player
        int block = 0; // Đếm số ô bị chặn (ra ngoài bàn hoặc gặp đối thủ)
        int nx = x + dx, ny = y + dy;

        // Đếm về một phía
        while (nx >= 0 && nx < Board.Size && ny >= 0 && ny < Board.Size)
        {
            if (gameController.Board.Grid[nx, ny] == player)
            {
                count++;
            }
            else if (gameController.Board.Grid[nx, ny] != 0)
            {
                block++;
                break;
            }
            else
            {
                break;
            }
            nx += dx;
            ny += dy;
        }

        nx = x - dx;
        ny = y - dy;

        // Đếm về phía ngược lại
        while (nx >= 0 && nx < Board.Size && ny >= 0 && ny < Board.Size)
        {
            if (gameController.Board.Grid[nx, ny] == player)
            {
                count++;
            }
            else if (gameController.Board.Grid[nx, ny] != 0)
            {
                block++;
                break;
            }
            else
            {
                break;
            }
            nx -= dx;
            ny -= dy;
        }

        // Tính điểm
        if (count >= WinLength)
            return 10000; // Thắng
        if (block == 2) // Bị chặn hai đầu
            return 0;

        return count * count; // Tăng điểm theo số lượng quân liên tiếp
    }
}
