using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pawn : ChessPiece
{
   public override void GetLegalMoves()
{
    ChessBoardPlacementHandler.Instance.ClearHighlights();

    string myTag = gameObject.tag;
    int direction = myTag == "White" ? -1 : 1; // White moves up, Black moves down

    // One step forward
    TryForwardMove(row + direction, column, myTag);

    // Two steps forward (only from starting row)
    if ((myTag == "Black" && row == 1) || (myTag == "White" && row == 6))
    {
        if (!ChessBoardPlacementHandler.Instance.IsOccupied(row + direction, column))
        {
            TryForwardMove(row + 2 * direction, column, myTag);
        }
    }

    // Capture diagonally
    TryCaptureTile(row + direction, column - 1, myTag);
    TryCaptureTile(row + direction, column + 1, myTag);
}

    private void TryForwardMove(int r, int c, string myTag)
    {
        if (!IsInsideBoard(r, c)) return;

        if (!ChessBoardPlacementHandler.Instance.IsOccupied(r, c))
        {
            ChessBoardPlacementHandler.Instance.Highlight(r, c);
        }
        else
        {
            ChessBoardPlacementHandler.Instance.HighlightBlocked(r, c);
        }
    }

    private void TryCaptureTile(int r, int c, string myTag)
    {
        if (!IsInsideBoard(r, c)) return;

        if (ChessBoardPlacementHandler.Instance.IsEnemyPiece(r, c, myTag))
        {
            ChessBoardPlacementHandler.Instance.HighlightCapture(r, c);
        }
        else if (ChessBoardPlacementHandler.Instance.IsFriendlyPiece(r, c, myTag))
        {
            ChessBoardPlacementHandler.Instance.HighlightBlocked(r, c);
        }
    }

    private bool IsInsideBoard(int row, int col)
    {
        return row >= 0 && row < 8 && col >= 0 && col < 8;
    }
}
