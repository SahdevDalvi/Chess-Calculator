using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChessPiece : MonoBehaviour {
    public int row, column; 

    
    public abstract void GetLegalMoves();
    private void Start()
    {
         ChessBoardPlacementHandler.Instance.RegisterPiece(this, row, column);
    }

}

