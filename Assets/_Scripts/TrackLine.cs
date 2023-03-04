using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackLine : MonoBehaviour
{
	public int EnemiesCount => obstaclesInRange.Count;
	public List<Obstacles> obstaclesInRange = new List<Obstacles>(); // Autofill
	

    [SerializeField] LineRenderer lineRenderer;
	[SerializeField] BoxCollider boxCollider;

	public void SetWidth(float width) 
    {
		boxCollider.size = new Vector3(boxCollider.size.x, width, boxCollider.size.z);
		lineRenderer.startWidth = lineRenderer.endWidth = width;
	}

	private void OnTriggerEnter(Collider other) 
	{
		Obstacles obstacles = other.GetComponent<Obstacles>();
		if (obstacles != null && !obstaclesInRange.Contains(obstacles)) 
			obstaclesInRange.Add(obstacles);
	}

	private void OnTriggerExit(Collider other) 
	{
		Obstacles obstacles = other.GetComponent<Obstacles>();
		if(obstacles != null && obstaclesInRange.Contains(obstacles)) 
			obstaclesInRange.Remove(obstacles);
	}
}
