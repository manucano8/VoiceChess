using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VRBoard : MonoBehaviour
{
    [Header("Arte")]
    [SerializeField] private Material squareMaterial;
    [SerializeField] private Vector3 boardCenter = Vector3.zero;
    [SerializeField] private float squareSize = 1.0f;
    [SerializeField] private float yOffset = 0.2f;
    [SerializeField] private float deathSpace = 0.1f;

    [Header("Prefabs")]
    [SerializeField] private GameObject[] whitePieces;
    [SerializeField] private GameObject[] blackPieces;

    private List<Piece> deadWhite = new List<Piece>();
    private List<Piece> deadBlack = new List<Piece>();
    private List<Vector2Int[]> moveList = new List<Vector2Int[]>();
    private Piece[,] pieces;
    private Piece currentlySelected;
    private const int square_count_x = 8;
    private const int square_count_y = 8;
    private GameObject[,] Chessboard;
    private Camera currentCamera;
    private Vector2Int currentHover;
    private Vector3 bounds;
    private List<Vector2Int> availableMoves = new List<Vector2Int>();
    public bool whiteTurn;
    private SpecialMove specialMove;

    private void Awake()
    {
        whiteTurn = true;
        createGrid(squareSize, square_count_x, square_count_y);
        spawnAllPieces();
        positionAllPieces();
    }

    public Piece choosePiece(int x, int y){

            if (pieces[x, y] != null){
                //turn?
                if ((pieces[x, y].team == 0 && whiteTurn) || (pieces[x, y].team == 1 && !whiteTurn)){
                    currentlySelected = pieces[x, y];
                    currentlySelected.setPosition(currentlySelected.transform.position + Vector3.up * 0.3f);

                    availableMoves = currentlySelected.getAvailableMoves(ref pieces, square_count_x, square_count_y);

                    specialMove = currentlySelected.getSpecialMoves(ref pieces, ref moveList, ref availableMoves);

                    preventCheck();

                    highlightMoves();
                }
            }
            return currentlySelected;
    }

    public void moveChosen(Piece currentlySelected, int x, int y){
            if (currentlySelected != null){
                Vector2Int  previousPosition = new Vector2Int(currentlySelected.currentX, currentlySelected.currentY);

                bool validMove = MoveTo(currentlySelected, x, y);
                if (!validMove){
                    currentlySelected.setPosition(getSquareCenter(previousPosition.x, previousPosition.y));
                }
                currentlySelected = null;
                removeHighlightMoves();
            }
    }

    //Creacion del tablero
    private void createGrid(float squareSize, int squareCountX, int squareCountY){
        yOffset += transform.position.y;
        bounds = new Vector3((squareCountX / 2) * squareSize, 0, (squareCountX / 2) * squareSize) + boardCenter;
        Chessboard = new GameObject[squareCountX, squareCountY];
        for (int x = 0; x < squareCountX; x++)
        {
            for (int y = 0; y < squareCountY; y++)
            {
                Chessboard[x, y] = createSquare(squareSize, x, y); 
            }
        }
    }

    private GameObject createSquare(float squareSize, int x, int y){
        GameObject squareObject = new GameObject(string.Format("X:{0}, Y:{1}", x, y));
        squareObject.transform.parent = transform;

        Mesh mesh = new Mesh();
        squareObject.AddComponent<MeshFilter>().mesh = mesh;
        squareObject.AddComponent<MeshRenderer>().material = squareMaterial;

        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(x * squareSize, yOffset, y * squareSize) - bounds;
        vertices[1] = new Vector3(x * squareSize, yOffset, (y + 1) * squareSize) - bounds;
        vertices[2] = new Vector3((x + 1) * squareSize, yOffset, y * squareSize) - bounds;
        vertices[3] = new Vector3((x + 1) * squareSize, yOffset, (y + 1) * squareSize) - bounds;

        int[] tris = new int[] {0, 1, 2, 1, 3, 2};

        mesh.vertices = vertices;
        mesh.triangles = tris;
        mesh.RecalculateNormals();

        squareObject.layer = LayerMask.NameToLayer("Square");

        squareObject.AddComponent<BoxCollider>();

        return squareObject;
    }

    //instanciar las piezas
    private void spawnAllPieces(){
        pieces = new Piece[square_count_x, square_count_y];

        //blancas
        pieces[0, 0] = spawnOnePiece(PieceType.Torre, 0);
        pieces[1, 0] = spawnOnePiece(PieceType.Caballo, 0);
        pieces[2, 0] = spawnOnePiece(PieceType.Alfil, 0);
        pieces[3, 0] = spawnOnePiece(PieceType.Reina, 0);
        pieces[4, 0] = spawnOnePiece(PieceType.Rey, 0);
        pieces[5, 0] = spawnOnePiece(PieceType.Alfil, 0);
        pieces[6, 0] = spawnOnePiece(PieceType.Caballo, 0);
        pieces[7, 0] = spawnOnePiece(PieceType.Torre, 0);
        for (int i = 0; i < square_count_x; i++){
            pieces[i, 1] = spawnOnePiece(PieceType.Peon, 0);
        }

        //negras
        pieces[0, 7] = spawnOnePiece(PieceType.Torre, 1);
        pieces[1, 7] = spawnOnePiece(PieceType.Caballo, 1);
        pieces[2, 7] = spawnOnePiece(PieceType.Alfil, 1);
        pieces[3, 7] = spawnOnePiece(PieceType.Reina, 1);
        pieces[4, 7] = spawnOnePiece(PieceType.Rey, 1);
        pieces[5, 7] = spawnOnePiece(PieceType.Alfil, 1);
        pieces[6, 7] = spawnOnePiece(PieceType.Caballo, 1);
        pieces[7, 7] = spawnOnePiece(PieceType.Torre, 1);
        for (int i = 0; i < square_count_x; i++){
            pieces[i, 6] = spawnOnePiece(PieceType.Peon, 1);
        }
    }

    private Piece spawnOnePiece(PieceType type, int team){
        if (team == 0){
            Piece p = Instantiate(whitePieces[(int)type - 1], transform).GetComponent<Piece>();
            p.type = type;
            p.team = team;

            return p;
        }
        else {
            Piece p = Instantiate(blackPieces[(int)type - 1], transform).GetComponent<Piece>();
            p.type = type;
            p.team = team;

            return p;
        }
    }

    private void positionAllPieces(){
        for (int x = 0; x < square_count_x; x++)
        {
            for (int y = 0; y < square_count_y; y++)
            {
                if (pieces[x, y] != null){
                    positionOnePiece(x, y, true);
                }
            }
        }
    }

    private void positionOnePiece(int x, int y, bool force = false){
        pieces[x, y].currentX = x;
        pieces[x, y].currentY = y;
        if (pieces[x, y].type == PieceType.Rey) {
            pieces[x, y].setPosition(getSquareCenter(x, y + 2), force);
        }
        else {
            pieces[x, y].setPosition(getSquareCenter(x, y), force);
            if (pieces[x, y].team == 0){
                pieces[x, y].transform.Rotate(0f, -90f, 0f, Space.World);
            }
            else{
                pieces[x, y].transform.Rotate(0f, 90f, 0f, Space.World);
            }
        }
    }

    private Vector3 getSquareCenter(int x, int y) {
        return new Vector3(x * squareSize, yOffset, y * squareSize) - bounds + new Vector3(squareSize / 2, 0, squareSize / 2);
    }
    
    private void highlightMoves(){
        for (int i = 0; i < availableMoves.Count; i++)
        {
            Chessboard[availableMoves[i].x, availableMoves[i].y].layer = LayerMask.NameToLayer("Highlight");
        }
    }

    private void removeHighlightMoves(){
        for (int i = 0; i < availableMoves.Count; i++)
        {
            Chessboard[availableMoves[i].x, availableMoves[i].y].layer = LayerMask.NameToLayer("Square");
        }

        availableMoves.Clear();
    }

    //movimientos especiales
    private void processSpecialMove() {
        if (specialMove == SpecialMove.Pasante){
            var newMove = moveList[moveList.Count - 1];
            Piece miPeon = pieces[newMove[1].x, newMove[1].y];
            var posicionPeonObjetivo = moveList[moveList.Count - 2];
            Piece peonEnemigo = pieces[posicionPeonObjetivo[1].x, posicionPeonObjetivo[1].y];

            if (miPeon.currentX == peonEnemigo.currentX){
                if (miPeon.currentY == peonEnemigo.currentY - 1 || miPeon.currentY == peonEnemigo.currentY + 1){
                    if (peonEnemigo.team == 0){
                        deadWhite.Add(peonEnemigo);
                        peonEnemigo.setScale(peonEnemigo.desiredScale / 3);
                        peonEnemigo.setPosition(new Vector3(8 * squareSize, yOffset, -1 * squareSize) - bounds + new Vector3(squareSize / 2, 0, squareSize / 2) + (Vector3.forward * deathSpace) * deadWhite.Count);
                    }
                    else {
                        deadBlack.Add(peonEnemigo);
                        peonEnemigo.setScale(peonEnemigo.desiredScale / 3);
                        peonEnemigo.setPosition(new Vector3(-1 * squareSize, yOffset, 8 * squareSize) - bounds + new Vector3(squareSize / 2, 0, squareSize / 2) + (Vector3.back * deathSpace) * deadBlack.Count);
                    }
                    pieces[peonEnemigo.currentX, peonEnemigo.currentY] = null;
                }
            }
        }
    
        if (specialMove == SpecialMove.Enroque) {
            Vector2Int[] lastMove = moveList[moveList.Count - 1];

            //torre izquierda 
            if (lastMove[1].x == 2) {
                if (lastMove[1].y == 0) {
                    Piece torre = pieces[0, 0];
                    pieces[3, 0] = torre;
                    positionOnePiece(3, 0);
                    pieces[0, 0] = null;
                }
                else if (lastMove[1].y == 7) {
                    Piece torre = pieces[0, 7];
                    pieces[3, 7] = torre;
                    positionOnePiece(3, 7);
                    pieces[0, 7] = null;
                }
            }
            else if (lastMove[1].x == 6) {
                if (lastMove[1].y == 0) {
                    Piece torre = pieces[7, 0];
                    pieces[5, 0] = torre;
                    positionOnePiece(5, 0);
                    pieces[7, 0] = null;
                }
                else if (lastMove[1].y == 7) {
                    Piece torre = pieces[7, 7];
                    pieces[5, 7] = torre;
                    positionOnePiece(5, 7);
                    pieces[7, 7] = null;
                }  
            }
        }

        if (specialMove == SpecialMove.Promocion) {
            Vector2Int[] lastMove = moveList[moveList.Count - 1];
            Piece peon = pieces[lastMove[1].x, lastMove[1].y];

            if (peon.type == PieceType.Peon){
                if (peon.team == 0 && lastMove[1].y == 7) {
                    Piece nuevaReina = spawnOnePiece(PieceType.Reina, 0);
                    nuevaReina.transform.position = pieces[lastMove[1].x, lastMove[1].y].transform.position;
                    Destroy(pieces[lastMove[1].x, lastMove[1].y].gameObject);
                    pieces[lastMove[1].x, lastMove[1].y] = nuevaReina;
                    positionOnePiece(lastMove[1].x, lastMove[1].y, true);
                }

                if (peon.team == 1 && lastMove[1].y == 0) {
                    Piece nuevaReina = spawnOnePiece(PieceType.Reina, 1 );
                    nuevaReina.transform.position = pieces[lastMove[1].x, lastMove[1].y].transform.position;
                    Destroy(pieces[lastMove[1].x, lastMove[1].y].gameObject);
                    pieces[lastMove[1].x, lastMove[1].y] = nuevaReina;
                    positionOnePiece(lastMove[1].x, lastMove[1].y, true);
                }
            }
        }
    }

    private void preventCheck() {
        Piece rey = null;
        for (int x = 0; x < square_count_x ; x++)
        {
            for (int y = 0; y < square_count_y; y++)
            {
                if (pieces[x, y] != null){
                    if (pieces[x, y].type == PieceType.Rey) {
                        if (pieces[x, y].team == currentlySelected.team) {
                            rey = pieces[x, y];
                        }
                    }
                }
            }
        }

        simulateMoveOnePiece(currentlySelected, ref availableMoves, rey);
    }
    
    private void simulateMoveOnePiece(Piece p, ref List<Vector2Int> moves, Piece rey) {
        //guardamos valores para resetear despues de la llamada
        int actualX = p.currentX;
        int actualY = p.currentY;
        List<Vector2Int> movesToRemove = new List<Vector2Int>();

        //simulamos todos los movimientos y comprobamos si hay check
        for (int i = 0; i < moves.Count; i++)
        {
            int simX = moves[i].x;
            int simY = moves[i].y;

            Vector2Int posicionReySimulada = new Vector2Int(rey.currentX, rey.currentY);
            if (p.type == PieceType.Rey) {
                posicionReySimulada = new Vector2Int(simX, simY);
            }

            Piece[,] simulation = new Piece[square_count_x, square_count_y];
            List<Piece> simAttackingPieces = new List<Piece>();
            for (int x = 0; x < square_count_x; x++)
            {
                for (int y = 0; y < square_count_y; y++)
                {
                    if (pieces[x, y] != null) {
                        simulation[x, y] = pieces[x, y];
                        if (simulation[x, y].team != p.team) {
                            simAttackingPieces.Add(simulation[x, y]);
                        }
                    }                    
                }
            }

            //simulamos el movimiento
            simulation[actualX, actualY] = null;
            p.currentX = simX;
            p.currentY = simY;
            simulation[simX, simY] = p;

            //alguna pieza fue tomada duranta la simulacion?
            var deadPiece = simAttackingPieces.Find(c => c.currentX == simX && c.currentY == simY);
            if (deadPiece != null) {
                simAttackingPieces.Remove(deadPiece);
            }

            //recoger todos los movimientos simulados
            List<Vector2Int> simMoves = new List<Vector2Int>();
            for (int a = 0; a < simAttackingPieces.Count; a++)
            {
                var piecesMoves = simAttackingPieces[a].getAvailableMoves(ref simulation, square_count_x, square_count_y);
                for (int b = 0; b < piecesMoves.Count; b++)
                {
                    simMoves.Add(piecesMoves[b]);
                }
            }

            //si el rey esta en problemas, borramos el movimiento
            if (containsValidMove(ref simMoves, posicionReySimulada)) {
                movesToRemove.Add(moves[i]);
            }

            p.currentX = actualX;
            p.currentY = actualY;
        }

        //borramos de los movimientos posibles
        for (int i = 0; i < movesToRemove.Count; i++)
        {
            moves.Remove(movesToRemove[i]);
        }
    }

    private bool checkForCheckmate() {
        var lastMove = moveList[moveList.Count - 1];
        int targetTeam = (pieces[lastMove[1].x, lastMove[1].y].team == 0) ? 1 : 0;
        List<Piece> attackingPieces = new List<Piece>();
        List<Piece> defendingPieces = new List<Piece>();
        Piece rey = null;

        for (int x = 0; x < square_count_x ; x++)
        {
            for (int y = 0; y < square_count_y; y++)
            {
                if (pieces[x, y] != null){
                    if (pieces[x, y].team == targetTeam) {
                        defendingPieces.Add(pieces[x, y]);
                        if (pieces[x, y].type == PieceType.Rey) {
                            rey = pieces[x, y];
                        }
                    }
                    else {
                        attackingPieces.Add(pieces[x, y]);
                    }
                }
            }
        }
        //estan atacando al rey?
        List<Vector2Int> currentAvailableMoves = new List<Vector2Int>();
        for (int i = 0; i < attackingPieces.Count; i++)
        {
            var piecesMoves = attackingPieces[i].getAvailableMoves(ref pieces, square_count_x, square_count_y);
            for (int b = 0; b < piecesMoves.Count; b++)
            {
                currentAvailableMoves.Add(piecesMoves[b]);
            }
        }
        //estamos en check?
        if (containsValidMove(ref currentAvailableMoves, new Vector2Int(rey.currentX, rey.currentY))) {
            //comprobamos si podemos salvar al rey
            for (int i = 0; i < defendingPieces.Count; i++)
            {
                List<Vector2Int> defendingMoves = defendingPieces[i].getAvailableMoves(ref pieces, square_count_x, square_count_y);
                simulateMoveOnePiece(defendingPieces[i], ref defendingMoves, rey);

                if (defendingMoves.Count != 0) {
                    return false;
                }   
            }

            return true;
        }

        return false;
    }

    //Operaciones
    private void checkMate(int team){
        displayVictory(team);
    }

    private void displayVictory(int winner){
        if (winner == 0){
            SceneManager.LoadScene("MenuGanadorBlancas");
        }
        else {
            SceneManager.LoadScene("MenuGanadorNegras");
        }
    }

    private bool containsValidMove(ref List<Vector2Int> moves, Vector2Int pos){
        for (int i = 0; i < moves.Count; i++)
        {
            if (moves[i].x == pos.x && moves[i].y == pos.y){
                return true;
            }
        }
        return false;
    }

    private bool MoveTo(Piece p, int x, int y){
        if (!containsValidMove(ref availableMoves, new Vector2Int(x, y))){
            return false;
        }

        Vector2Int previousPosition = new Vector2Int(p.currentX, p.currentY);

        //comprobar si hay una pieza en esa posicion
        if (pieces[x, y] != null){
            Piece auxp = pieces[x, y];
            if (p.team == auxp.team){
                return false;
            }
            //piezas de distinto color
            if (auxp.team == 0){
                if (auxp.type == PieceType.Rey){
                    checkMate(1);
                }
                deadWhite.Add(auxp);
                auxp.setScale(auxp.desiredScale / 3);
                auxp.setPosition(new Vector3(8 * squareSize, yOffset, -1 * squareSize) - bounds + new Vector3(squareSize / 2, 0, squareSize / 2) + (Vector3.forward * deathSpace) * deadWhite.Count);
            }
            else {
                if (auxp.type == PieceType.Rey){
                    checkMate(0);
                }
                deadBlack.Add(auxp);
                auxp.setScale(auxp.desiredScale / 3);
                auxp.setPosition(new Vector3(-1 * squareSize, yOffset, 8 * squareSize) - bounds + new Vector3(squareSize / 2, 0, squareSize / 2) + (Vector3.back * deathSpace) * deadBlack.Count);

            }
        }
        pieces[x, y] = p;
        if (pieces[x, y].team == 0){
            pieces[x, y].transform.Rotate(0f, 90f, 0f, Space.World);
        }
        else{
            pieces[x, y].transform.Rotate(0f, -90f, 0f, Space.World);
        }
        pieces[previousPosition.x, previousPosition.y] = null;

        positionOnePiece(x, y);
        moveList.Add(new Vector2Int[] { previousPosition, new Vector2Int(x, y)});

        processSpecialMove();
        if (checkForCheckmate()) {
            checkMate(p.team);
        }
        
        return true;
    }

    private Vector2Int searchSquareIndex(GameObject hitInfo){
        for (int x = 0; x < square_count_x; x++)
        {
            for (int y = 0; y < square_count_y; y++)
            {
                if (Chessboard[x, y] == hitInfo){
                    return new Vector2Int(x, y);
                }
            }
        }
        return -Vector2Int.one;
    }

}
