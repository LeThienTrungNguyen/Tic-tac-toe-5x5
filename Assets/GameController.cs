using UnityEngine;

public class GameController : MonoBehaviour
{
    public Board board;
    public PlayerController playerController;
    public AIController aiController;
    public Transform BG_Prefab;
    public Transform BD_Prefab_H;
    public Transform BD_Prefab_V;
    public Transform X_Prefabs;
    public Transform O_Prefabs;
    public int CurrentPlayer = 1;

    public Board Board => board;

    private void Start()
    {
        playerController.gameController = this;
        aiController.gameController = this;
        DrawBoard(board);
    }
    public void DrawBoard(Board board)
    {
        // draw bg
        for (int i = 0;i< board.Grid.GetLength(0); i++)
        {
            for(int j = 0;j< board.Grid.GetLength(1); j++)
            {
                var position = new Vector3(i, j);
                var BG = Instantiate(BG_Prefab, position, Quaternion.identity, board.transform.GetChild(0));

            }
        }
        // draw border
        for (int i = 0; i< board.Grid.GetLength(0) - 1; i++)
        {
            var position = new Vector3(i, board.Grid.GetLength(0) / 2);
            var BD = Instantiate(BG_Prefab, position+ (Vector3)(Vector2.down / 2+Vector2.right/2), Quaternion.identity, board.transform.GetChild(1));
            BD.transform.localScale = new Vector3(BD.transform.localScale.x*0.1f, board.Grid.GetLength(0));
            BD.transform.GetComponent<SpriteRenderer>().sortingOrder = 10;
            BD.transform.GetComponent<SpriteRenderer>().color = Color.green;
        }

        for (int i = 0; i < board.Grid.GetLength(0) - 1; i++)
        {
            var position = new Vector3(board.Grid.GetLength(0) / 2,i);
            var BD = Instantiate(BG_Prefab, position + (Vector3)(Vector2.up / 2 + Vector2.left / 2), Quaternion.identity, board.transform.GetChild(1));
            BD.transform.localScale = new Vector3(board.Grid.GetLength(0),BD.transform.localScale.y * 0.1f);
            BD.transform.GetComponent<SpriteRenderer>().sortingOrder = 10;
            BD.transform.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
    public void DrawFlag(int x,int y , int player)
    {
        var pos = new Vector3(x, y);
        var rot = Quaternion.identity;
        Instantiate(player== 1 ? X_Prefabs:O_Prefabs, pos, rot, board.transform.GetChild(2));
    }
    public void EndTurn(int x, int y)
    {
        if (board.CheckWin(x, y, CurrentPlayer))
        {
            Debug.Log($"Player {CurrentPlayer} wins!");
            // Reset game or show winning screen
        }
        else
        {
            CurrentPlayer = 3 - CurrentPlayer; // Toggle between 1 and 2
            if (CurrentPlayer == 2)
            {
                aiController.PlayTurn();
            }
        }
    }
}
