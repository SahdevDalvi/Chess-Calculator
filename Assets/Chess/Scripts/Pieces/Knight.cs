using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Knight : ChessPiece
{
    public override void GetLegalMoves()
    {
        ChessBoardPlacementHandler.Instance.ClearHighlights();
        string myTag = gameObject.tag;

        // All possible knight moves
        TryMove(row + 2, column + 1, myTag);
        TryMove(row + 2, column - 1, myTag);
        TryMove(row - 2, column + 1, myTag);
        TryMove(row - 2, column - 1, myTag);
        TryMove(row + 1, column + 2, myTag);
        TryMove(row + 1, column - 2, myTag);
        TryMove(row - 1, column + 2, myTag);
        TryMove(row - 1, column - 2, myTag);
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