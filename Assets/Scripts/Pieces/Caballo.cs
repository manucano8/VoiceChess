using System.Collections.Generic;
using UnityEngine;

public class Caballo : Piece
{
    public override List<Vector2Int> getAvailableMoves(ref Piece[,] board, int squareCountX, int squareCountY){
        List<Vector2Int> result = new List<Vector2Int>();

        //arriba derecha
        int x = currentX + 1;
        int y = currentY + 2;
        if (x < squareCountX && y < squareCountY){
            if (board[x, y] == null  || board[x, y].team != team){
                result.Add(new Vector2Int(x, y));
            }
        }

        x = currentX + 2;
        y = currentY + 1;
        if (x < squareCountX && y < squareCountY){
            if (board[x, y] == null  || board[x, y].team != team){
                result.Add(new Vector2Int(x, y));
            }
        }

        //arriba izquierda
        x = currentX - 1;
        y = currentY + 2;
        if (x >= 0 && y < squareCountY){
            if (board[x, y] == null  || board[x, y].team != team){
                result.Add(new Vector2Int(x, y));
            }
        }

        x = currentX - 2;
        y = currentY + 1;
        if (x >= 0 && y < squareCountY){
            if (board[x, y] == null  || board[x, y].team != team){
                result.Add(new Vector2Int(x, y));
            }
        }       

        //abajo derecha
        x = currentX + 1;
        y = currentY - 2;
        if (x < squareCountX && y >= 0){
            if (board[x, y] == null  || board[x, y].team != team){
                result.Add(new Vector2Int(x, y));
            }
        }

        x = currentX + 2;
        y = currentY - 1;
        if (x < squareCountX && y >= 0){
            if (board[x, y] == null  || board[x, y].team != team){
                result.Add(new Vector2Int(x, y));
            }
        } 

        //abajo izquierda
        x = currentX - 1;
        y = currentY - 2;
        if (x >= 0 && y >= 0){
            if (board[x, y] == null  || board[x, y].team != team){
                result.Add(new Vector2Int(x, y));
            }
        }

        x = currentX - 2;
        y = currentY - 1;
        if (x >= 0 && y >= 0){
            if (board[x, y] == null  || board[x, y].team != team){
                result.Add(new Vector2Int(x, y));
            }
        }

        return result;
    }
}
