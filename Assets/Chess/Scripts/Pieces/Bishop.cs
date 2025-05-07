using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : ChessPiece
{
    public override void GetLegalMoves()
    {
        ChessBoardPlacementHandler.Instance.ClearHighlights();
        string myTag = gameObject.tag;

        // Four diagonals
        TryDirection(1, 1, myTag);    // Up-Right
        TryDirection(1, -1, myTag);   // Up-Left
        TryDirection(-1, 1, myTag);   // Down-Right
        TryDirection(-1, -1, myTag);  // Down-Left
    }

    private void TryDirection(int rowDelta, int colDelta, string myTag)
    {
        int r = row + rowDelta;
        int c = column + colDelta;

        while (IsInsideBoard(r, c))
        {
            if (!ChessBoardPlacementHandler.Instance.IsOccupied(r, c))
            {
                ChessBoardPlacementHandler.Instance.Highlight(r, c);
            }
            else
            {
                if (ChessBoardPlacementHandler.Instance.IsEnemyPiece(r, c, myTag))
                    ChessBoardPlacementHandler.Instance.HighlightCapture(r, c);
                else if (ChessBoardPlacementHandler.Instance.IsFriendlyPiece(r, c, myTag))
                    ChessBoardPlacementHandler.Instance.HighlightBlocked(r, c);
                break;
            }
            r += rowDelta;
            c += colDelta;
        }
    }

    private bool IsInsideBoard(int row, int col)
    {
        return row >= 0 && row < 8 && col >= 0 && col < 8;
    }
}