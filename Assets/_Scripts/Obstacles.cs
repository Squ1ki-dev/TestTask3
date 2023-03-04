using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    private Settings _settings;

    private bool _infected;
    public bool Infected => _infected;
    
    private Material defaultMaterial;
    [SerializeField] private Material infectionMaterial;
    [SerializeField] private MeshRenderer meshRenderer;
    
    private void Start() 
    {
        GameManager.Instance.obstacles.Add(this);
        defaultMaterial = meshRenderer.material;
    }

    private void Boom() 
    {
        GameManager.Instance.trackLine.obstaclesInRange.Remove(this);
        Destroy(gameObject);
    } 

    public void Infection()
    {
        _infected = true;
        meshRenderer.material = infectionMaterial;
        Invoke(nameof(Boom), 1.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.GetComponent<Bullet>();
        if (bullet)
            Infection();
    }
}
