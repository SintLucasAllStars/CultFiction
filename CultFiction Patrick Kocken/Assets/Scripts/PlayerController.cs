using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private PathFinding _pathfinding;
    [SerializeField] private Player _player;
    [SerializeField] private Grid _grid;
    [SerializeField] private MouseScript _mouse;

    public bool PlayerIsNotMoving = true;
    private List<Vector3> _pathPositions;

	public void StartWalking(Vector3 targetPos){

        StartCoroutine(PlayerMovement(targetPos));
    }

    private IEnumerator PlayerMovement(Vector3 targetPos)
    {

        //Resets Grid for Pathfinding
        _grid.StartPathFinding();
		PlayerIsNotMoving = false;

		//Starts finding a path from the StartPos to the TargetPos 
        _pathfinding.FindPath(_player.transform.position, targetPos);
        _pathPositions = _pathfinding.PathPositions;

        int pathPositionsIndex = 0;

		// If there are more PathSteps the player will move towards them
        while (pathPositionsIndex < _pathPositions.Count && _mouse.enabled)
        {
            _player.WalkStep(_pathPositions[pathPositionsIndex++]);
            yield return new WaitForSeconds(1);
        }

        PlayerIsNotMoving = true;

    }
}
