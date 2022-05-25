using System.Collections.Generic;
using UnityEngine;

public class Reina : Piece
{
    public override List<Vector2Int> getAvailableMoves(ref Piece[,] board, int squareCountX, int squareCountY){
        List<Vector2Int> result = new List<Vector2Int>();

        //abajo
        for (int i = currentY - 1; i >= 0; i--)
        {   
            if (board[currentX, i] == null){
                result.Add(new Vector2Int(currentX, i));
            }
            else {
                if (board[currentX, i].team != team){
                    result.Add(new Vector2Int(currentX, i));
                }
                break;
            }
        }

        //arriba
        for (int i = currentY + 1; i < squareCountY; i++)
        {   
            if (board[currentX, i] == null){
                result.Add(new Vector2Int(currentX, i));
            }
            else {
                if (board[currentX, i].team != team){
                    result.Add(new Vector2Int(currentX, i));
                }
                break;
            }
        }

        //derecha
        for (int i = currentX + 1; i < squareCountX; i++)
        {   
            if (board[i, currentY] == null){
                result.Add(new Vector2Int(i, currentY));
            }
            else {
                if (board[i, currentY].team != team){
                    result.Add(new Vector2Int(i, currentY));
                }
                break;
            }
        }

        //izquierda
        for (int i = currentX - 1; i >= 0; i--)
        {   
            if (board[i, currentY] == null){
                result.Add(new Vector2Int(i, currentY));
            }
            else {
                if (board[i, currentY].team != team){
                    result.Add(new Vector2Int(i, currentY));
                }
                break;
            }
        }

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
