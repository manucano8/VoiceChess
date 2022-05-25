using System.Collections.Generic;
using UnityEngine;

public class Rey : Piece
{
    public override List<Vector2Int> getAvailableMoves(ref Piece[,] board, int squareCountX, int squareCountY){
        List<Vector2Int> result = new List<Vector2Int>();

        //derecha
        if (currentX + 1 < squareCountX){
            if (board[currentX + 1, currentY] == null){
                result.Add(new Vector2Int(currentX + 1, currentY));
            }
            else if (board[currentX + 1, currentY].team != team){
                result.Add(new Vector2Int(currentX + 1, currentY));
            }
            //arriba derecha
            if (currentY + 1 < squareCountY){
                if (board[currentX + 1, currentY + 1] == null){
                    result.Add(new Vector2Int(currentX + 1, currentY + 1));
                }
                else if (board[currentX + 1, currentY + 1].team != team){
                    result.Add(new Vector2Int(currentX + 1, currentY + 1));
                }     
            }

            //abajo derecha
            if (currentY - 1 >= 0){
                if (board[currentX + 1, currentY - 1] == null){
                    result.Add(new Vector2Int(currentX + 1, currentY - 1));
                }
                else if (board[currentX + 1, currentY - 1].team != team){
                    result.Add(new Vector2Int(currentX + 1, currentY - 1));
                }     
            }
        }

        //izquierda
        if (currentX - 1 >= 0){
            if (board[currentX - 1, currentY] == null){
                result.Add(new Vector2Int(currentX - 1, currentY));
            }
            else if (board[currentX - 1, currentY].team != team){
                result.Add(new Vector2Int(currentX - 1, currentY));
            }
            //arriba izquierda
            if (currentY + 1 < squareCountY){
                if (board[currentX - 1, currentY + 1] == null){
                    result.Add(new Vector2Int(currentX - 1, currentY + 1));
                }
                else if (board[currentX - 1, currentY + 1].team != team){
                    result.Add(new Vector2Int(currentX - 1, currentY + 1));
                }     
            }

            //abajo izquierda
            if (currentY - 1 >= 0){
                if (board[currentX - 1, currentY - 1] == null){
                    result.Add(new Vector2Int(currentX - 1, currentY - 1));
                }
                else if (board[currentX - 1, currentY - 1].team != team){
                    result.Add(new Vector2Int(currentX - 1, currentY - 1));
                }     
            }
        }

        //arriba
        if (currentY + 1 < squareCountY){
            if (board[currentX, currentY + 1] == null || board[currentX, currentY + 1].team != team){
                result.Add(new Vector2Int(currentX, currentY + 1));
            }
        }

        //abajo
        if (currentY - 1 >= 0){
            if (board[currentX, currentY - 1] == null || board[currentX, currentY - 1].team != team){
                result.Add(new Vector2Int(currentX, currentY - 1));
            }
        }
        return result;
    }

    public override SpecialMove getSpecialMoves(ref Piece[,] board, ref List<Vector2Int[]> moveList, ref List<Vector2Int> availableMoves)
    {
        SpecialMove r = SpecialMove.None;

        var ReyMove = moveList.Find(m => m[0].x == 4 && m[0].y == ((team == 0) ? 0 : 7));
        var torreIzq = moveList.Find(m => m[0].x == 0 && m[0].y == ((team == 0) ? 0 : 7));
        var torreDer = moveList.Find(m => m[0].x == 7 && m[0].y == ((team == 0) ? 0 : 7));

        if (ReyMove == null && currentX == 4) {
            //blancas
            if (team == 0){
                //enroque con torre izquierda
                if (torreIzq == null){
                    if (board[0, 0].type == PieceType.Torre){
                        if (board[0, 0].team == 0) {
                            if (board[3, 0] == null) {
                                if (board[2, 0] == null) {
                                    if (board[1, 0] == null) {
                                        availableMoves.Add(new Vector2Int(2, 0));
                                        r = SpecialMove.Enroque;
                                    }
                                }
                            }
                        }
                    }
                }
                //enroque con torre derecha
                if (torreDer == null){
                    if (board[7, 0].type == PieceType.Torre){
                        if (board[7, 0].team == 0) {
                            if (board[5, 0] == null) {
                                if (board[6, 0] == null) {
                                    availableMoves.Add(new Vector2Int(6, 0));
                                    r = SpecialMove.Enroque; 
                                }
                            }
                        }
                    }
                }
            }
            //negras
            else {
                if (torreIzq == null){
                    if (board[0, 7].type == PieceType.Torre){
                        if (board[0, 7].team == 1) {
                            if (board[3, 7] == null) {
                                if (board[2, 7] == null) {
                                    if (board[1, 7] == null) {
                                        availableMoves.Add(new Vector2Int(2, 7));
                                        r = SpecialMove.Enroque;
                                    }
                                }
                            }
                        }
                    }
                }

                if (torreDer == null){
                    if (board[7, 7].type == PieceType.Torre){
                        if (board[7, 7].team == 1) {
                            if (board[5, 7] == null) {
                                if (board[6, 7] == null) {
                                    availableMoves.Add(new Vector2Int(6, 7));
                                    r = SpecialMove.Enroque; 
                                }
                            }
                        }
                    }
                }
            }
        }
        

        return r;
    }
}
