using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Rook : ChessPiece
{
    public override void GetLegalMoves()
    {
        ChessBoardPlacementHandler.Instance.ClearHighlights();

        string myTag = gameObject.tag;

        // Four directions: up, down, right, left
        TryDirection(1, 0, myTag);    // Up
        TryDirection(-1, 0, myTag);   // Down
        TryDirection(0, 1, myTag);    // Right
        TryDirection(0, -1, myTag);   // Left
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
                {
                    ChessBoardPlacementHandler.Instance.HighlightCapture(r, c);
                }
                else if (ChessBoardPlacementHandler.Instance.IsFriendlyPiece(r, c, myTag))
                {
                    ChessBoardPlacementHandler.Instance.HighlightBlocked(r, c);
                }
                break; // Stop at first occupied square
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