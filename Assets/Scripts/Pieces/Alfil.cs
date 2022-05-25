using System.Collections.Generic;
using UnityEngine;

public class Alfil : Piece
{
    public override List<Vector2Int> getAvailableMoves(ref Piece[,] board, int squareCountX, int squareCountY){
        List<Vector2Int> result = new List<Vector2Int>();

        //arriba derecha
        for (int x = currentX + 1, y = currentY + 1; x < squareCountX && y < squareCountY; x++, y++)
        {
            if (board[x, y] == null){
                result.Add(new Vector2Int(x, y));
            }
            else{
                if (board[x, y].team != team){
                    result.Add(new Vector2Int(x, y));
                }
                break;
            }
        }

        //arriba izquierda
        for (int x = currentX - 1, y = currentY + 1; x >= 0 && y < squareCountY; x--, y++)
        {
            if (board[x, y] == null){
                result.Add(new Vector2Int(x, y));
            }
            else{
                if (board[x, y].team != team){
                    result.Add(new Vector2Int(x, y));
                }
                break;
            }
        }

        //abajo derecha
        for (int x = currentX + 1, y = currentY - 1; x < squareCountX && y >= 0; x++, y--)
        {
            if (board[x, y] == null){
                result.Add(new Vector2Int(x, y));
            }
            else{
                if (board[x, y].team != team){
                    result.Add(new Vector2Int(x, y));
                }
                break;
            }
        }

        //abajo izquierda
        for (int x = currentX - 1, y = currentY - 1; x >= 0 && y >= 0; x--, y--)
        {
            if (board[x, y] == null){
                result.Add(new Vector2Int(x, y));
            }
            else{
                if (board[x, y].team != team){
                    result.Add(new Vector2Int(x, y));
                }
                break;
            }
        }

        return result;
    }
}
