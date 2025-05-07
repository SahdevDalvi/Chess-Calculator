using UnityEngine;
using System;
using System.Collections.Generic;

public class King : ChessPiece
{
    public override void GetLegalMoves()
    {
        ChessBoardPlacementHandler.Instance.ClearHighlights();
        string myTag = gameObject.tag;

        
        TryMove(row + 1, column, myTag);     // Up
        TryMove(row - 1, column, myTag);     // Down
        TryMove(row, column + 1, myTag);     // Right
        TryMove(row, column - 1, myTag);     // Left
        TryMove(row + 1, column + 1, myTag); // Up-Right
        TryMove(row + 1, column - 1, myTag); // Up-Left
        TryMove(row - 1, column + 1, myTag); // Down-Right
        TryMove(row - 1, column - 1, myTag); // Down-Left
    }

    private void TryMove(int r, int c, string myTag)
    {
        if (!IsInsideBoard(r, c)) return;

        if (!ChessBoardPlacementHandler.Instance.IsOccupied(r, c))
        {
            ChessBoardPlacementHandler.Instance.Highlight(r, c);
        }
        else if (ChessBoardPlacementHandler.Instance.IsEnemyPiece(r, c, myTag))
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