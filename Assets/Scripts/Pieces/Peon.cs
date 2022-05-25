using System.Collections.Generic;
using UnityEngine;

public class Peon : Piece
{
    public override List<Vector2Int> getAvailableMoves(ref Piece[,] board, int squareCountX, int squareCountY){
    List<Vector2Int> result = new List<Vector2Int>();

    int direction = (team == 0) ? 1 : -1;

    if (board[currentX, currentY + direction] == null){
        result.Add(new Vector2Int(currentX, currentY + direction));
    }

    if (board[currentX, currentY + direction] == null){
        if (team == 0 && currentY == 1 && board[currentX, currentY + (direction * 2)] == null){
            result.Add(new Vector2Int(currentX, currentY + (direction * 2)));
        }
        if (team == 1 && currentY == 6 && board[currentX, currentY + (direction * 2)] == null){
            result.Add(new Vector2Int(currentX, currentY + (direction * 2)));
        }
    }

    if (currentX != squareCountX - 1){
        if (board[currentX + 1, currentY + direction] != null && board[currentX + 1, currentY + direction].team != team){
            result.Add(new Vector2Int(currentX + 1, currentY + direction));
        }
    }
    if(currentX != 0){
        if (board[currentX - 1, currentY + direction] != null && board[currentX - 1, currentY + direction].team != team){
            result.Add(new Vector2Int(currentX - 1, currentY + direction));
        }
    }

    return result;
}

    public override SpecialMove getSpecialMoves(ref Piece[,] board, ref List<Vector2Int[]> moveList, ref List<Vector2Int> availableMoves)
    {
        int direction = (team == 0) ? 1 : -1;
        
        if ((team == 0 && currentY == 6) || (team == 1 && currentY == 1)) {
            return SpecialMove.Promocion;
        }
        
        if (moveList.Count > 0) {
            Vector2Int[] lastMove = moveList[moveList.Count - 1];
            //la ultima pieza que se movio fue un peon
            if (board[lastMove[1].x, lastMove[1].y].type == PieceType.Peon){
                //el ultimo movimiento fueron 2 casillas avanzadas
                if (Mathf.Abs(lastMove[0].y - lastMove[1].y) == 2){
                    //el ultimo movimiento fue del otro equipo
                    if (board[lastMove[1].x, lastMove[1].y].team != team){
                        //los dos peones estan en la misma fila
                        if (lastMove[1].y == currentY){
                            if (lastMove[1].x == currentX - 1){
                                availableMoves.Add(new Vector2Int(currentX - 1, currentY + direction));
                                return SpecialMove.Pasante;
                            }
                            if (lastMove[1].x == currentX + 1){
                                availableMoves.Add(new Vector2Int(currentX + 1, currentY + direction));
                                return SpecialMove.Pasante;
                            }
                        }
                    }

                }
            }
        }

        return SpecialMove.None;
    }
}
