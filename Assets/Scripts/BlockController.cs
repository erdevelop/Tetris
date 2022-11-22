using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public List<Transform> ListPiece => listPiece;
    [SerializeField] private List<Transform> listPiece = new List<Transform>();

    private void Start()
    {
        
        StartCoroutine(MoveDown());
    }

    IEnumerator MoveDown()
    {
        while(true)
        {
            var delay = GameManager.Instance.GameSpeed;
            yield return new WaitForSeconds(delay);

            var isMovable = GameManager.Instance.IsInside(GetPreviewPosition());
            if (isMovable)
                Move();
            else
            {
                foreach (var piece in listPiece)
                {
                    int x = Mathf.RoundToInt(piece.position.x);
                    int y = Mathf.RoundToInt(piece.position.y);

                    GameManager.Instance.Grid[x, y] = true;
                }

                GameManager.Instance.Spawn();

                break;
            }

            Move();
        }
        
    }
    private List<Vector2> GetPreviewPosition()
    {
        var result = new List<Vector2>();

        foreach (var piece in listPiece)
        {
            var position = piece.position;
            position.y--;
            result.Add(position);
        }
        return result;
    }

    private void Move()
    {
        var position = transform.position;
        position.y--;
        transform.position = position;
    }


}

