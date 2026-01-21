using UnityEngine;

public class Board : MonoBehaviour
{
    public const int Size = 50;
    private int[,] grid = new int[Size, Size];

    public int[,] Grid => grid;

    public bool PlaceMarker(int x, int y, int player)
    {
        if (x >= 0 && x < Size && y >= 0 && y < Size && grid[x, y] == 0)
        {
            grid[x, y] = player;
            return true;
        }
        return false;
    }

    public bool CheckWin(int x, int y, int player)
    {
        return CheckDirection(x, y, player, 1, 0) || // Horizontal
               CheckDirection(x, y, player, 0, 1) || // Vertical
               CheckDirection(x, y, player, 1, 1) || // Diagonal (\)
               CheckDirection(x, y, player, 1, -1);  // Diagonal (/)
    }

    private bool CheckDirection(int x, int y, int player, int dx, int dy)
    {
        int count = 1;
        count += CountInDirection(x, y, player, dx, dy);
        count += CountInDirection(x, y, player, -dx, -dy);
        return count >= 5;
    }

    private int CountInDirection(int x, int y, int player, int dx, int dy)
    {
        int count = 0;
        int nx = x + dx, ny = y + dy;
        while (nx >= 0 && nx < Size && ny >= 0 && ny < Size && grid[nx, ny] == player)
        {
            count++;
            nx += dx;
            ny += dy;
        }
        return count;
    }
}
