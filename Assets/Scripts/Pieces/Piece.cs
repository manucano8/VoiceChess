using System.Collections.Generic;
using UnityEngine;

public enum PieceType {
    None = 0,
    Peon = 1,
    Torre = 2,
    Caballo = 3,
    Alfil = 4,
    Reina = 5,
    Rey = 6
}
public class Piece : MonoBehaviour
{
    public int team;
    public int currentX;
    public int currentY;
    public PieceType type;

    private Vector3 desiredPosition;
    public Vector3 desiredScale;

    private void Update(){
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 10);
        transform.localScale = Vector3.Lerp(transform.localScale, desiredScale, Time.deltaTime * 10);
    }

    public virtual List<Vector2Int> getAvailableMoves(ref Piece[,] board, int squareCountX, int squareCountY){
        List<Vector2Int> result = new List<Vector2Int>(); 

        return result;
    }

    public virtual SpecialMove getSpecialMoves(ref Piece[,] board, ref List<Vector2Int[]> moveList, ref List<Vector2Int> availableMoves){
        return SpecialMove.None;
    }

    public virtual void setPosition(Vector3 position, bool force = false){
        desiredPosition = position;
        if (force){
            transform.position = desiredPosition;
        }
    }

    public virtual void setScale(Vector3 scale, bool force = false){
        desiredScale = scale;
        if (force){
            transform.localScale = desiredScale;
        }
    }
}
