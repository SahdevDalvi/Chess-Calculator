using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSelector : MonoBehaviour {
    public Camera mainCamera; 

    void Update() {
       
        if (Input.GetMouseButtonDown(0)) {
            
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        
            if (Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity)) {
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                
                ChessPiece piece = hit.collider.GetComponent<ChessPiece>();
                if (piece != null) {
                    piece.GetLegalMoves();  
                }
            }
        }
    }
}
