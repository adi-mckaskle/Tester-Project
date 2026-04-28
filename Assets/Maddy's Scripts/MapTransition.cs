using System.Collections;
using UnityEngine;

public class MapTransition : MonoBehaviour
{
    [SerializeField] Transform teleportTargetPosition;
}

enum Direction { Up, Down, Left, Right, Teleport }
