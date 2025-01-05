using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameController gameController;
    public int x,y;
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        x = Mathf.RoundToInt(mousePos.x);
        y = Mathf.RoundToInt(mousePos.y);
        if (gameController.CurrentPlayer == 1 && Input.GetMouseButtonDown(0))
        {
            

            if (gameController.Board.PlaceMarker(x, y, 1))
            {
                gameController.DrawFlag(x, y, 1);
                gameController.EndTurn(x, y);
            }
        }
    }
}
