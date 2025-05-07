using System;
using UnityEngine;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public sealed class ChessBoardPlacementHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] _rowsArray;
    [SerializeField] private GameObject _highlightPrefab;
    [SerializeField] private GameObject _blockedHighlightPrefab;
    [SerializeField] private GameObject _captureHighlightPrefab;
    private GameObject[,] _chessBoard;

    internal static ChessBoardPlacementHandler Instance;

    private void Awake()
    {
        Instance = this;
        GenerateArray();
    }

    private void GenerateArray()
    {
        _chessBoard = new GameObject[8, 8];
        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 8; j++)
            {
                _chessBoard[i, j] = _rowsArray[i].transform.GetChild(j).gameObject;
            }
        }
    }

    internal GameObject GetTile(int i, int j)
    {
        try
        {
            return _chessBoard[i, j];
        }
        catch (Exception)
        {
            Debug.LogError("Invalid row or column.");
            return null;
        }
    }

    internal void Highlight(int row, int col)
    {
        var tile = GetTile(row, col).transform;
        if (tile == null)
        {
            Debug.LogError("Invalid row or column.");
            return;
        }

        Instantiate(_highlightPrefab, tile.transform.position, Quaternion.identity, tile.transform);
    }

    internal void ClearHighlights()
    {
        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 8; j++)
            {
                var tile = GetTile(i, j);
                if (tile.transform.childCount <= 0) continue;
                foreach (Transform childTransform in tile.transform)
                {
                    Destroy(childTransform.gameObject);
                }
            }
        }
    }

    internal (bool isOccupied, GameObject occupyingPiece) GetOccupant(int row, int col)
    {
        var tile = GetTile(row, col);
        if (tile == null || tile.transform.childCount == 0)
            return (false, null);

        foreach (Transform child in tile.transform)
        {
            if (child.CompareTag("Piece") || child.CompareTag("White") || child.CompareTag("Black"))
            {
                return (true, child.gameObject);
            }
        }

        return (false, null);
    }

    private ChessPiece[,] _occupants = new ChessPiece[8, 8];

    public void RegisterPiece(ChessPiece piece, int row, int col)
    {
        _occupants[row, col] = piece;
    }

    public void UnregisterPiece(int row, int col)
    {
        _occupants[row, col] = null;
    }

    public bool IsOccupied(int row, int col)
    {
        return _occupants[row, col] != null;
    }

    public bool IsEnemyPiece(int row, int col, string myTag)
    {
        var piece = _occupants[row, col];
        return piece != null && piece.tag != myTag;
    }

    public bool IsFriendlyPiece(int row, int col, string myTag)
    {
        var piece = _occupants[row, col];
        return piece != null && piece.tag == myTag;
    }

    private bool HasPieceOnTile(GameObject tile)
    {
        foreach (Transform child in tile.transform)
        {
            if (child.CompareTag("White") || child.CompareTag("Black"))
                return true;
        }
        return false;
    }

    public void HighlightBlocked(int row, int col)
    {
        var tile = GetTile(row, col).transform;
        Instantiate(_blockedHighlightPrefab, tile.position, Quaternion.identity, tile);
    }

    public void HighlightCapture(int row, int col)
    {
        var tile = GetTile(row, col).transform;
        Instantiate(_captureHighlightPrefab, tile.position, Quaternion.identity, tile);
    }
}