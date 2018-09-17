using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPiece
{
    public string PieceName;
    public int PieceLength;
    public int[,] Data;

    public LevelPiece(string pieceName, int pieceLength, int[,] data){
        PieceName = pieceName;
        PieceLength = pieceLength;
        Data = data;
    }

}
