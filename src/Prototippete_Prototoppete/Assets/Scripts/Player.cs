using System;
using UnityEngine;

public class Player : ARotableEntity
{
    [SerializeField] private Transform _centerPoint;
    [SerializeField] private float _touchRange = 2f; // Quanto è la distanza che delimita se il player sta toccando terreno
    [SerializeField] private float _gravityTime = 0.5f; // Quanto tempo impiega la gravità ad avere effeto una volta ruotato il mondo
    [SerializeField] private float _gravity = 1f;
    [SerializeField] private Vector3 _gravityDirection = Vector3.down;

    private bool _hasRotated = false;
    private bool _isFalling = false;

    private float _timer = 0f;

    private void Update()
    {
        
        CheckFall();
        
        if(_isFalling)
            Fall();
        
        
        // _timer += Time.deltaTime;
        //
        // if(_timer <= _gravityTime)
        //     return;
        //
        // _timer = 0f;
        // _hasRotated = false;
        
        
    }

    private void CheckFall()
    {
        RaycastHit hit;
        if (Physics.Raycast(_centerPoint.position, _gravityDirection, out hit, _touchRange))
        {
            _isFalling = false;
            //transform.SetParent(hit.transform);
            return;
        } 
        _isFalling = true;
        transform.SetParent(null);
        
    }
    
    
    
    


    private void Fall()
    {
        transform.position += _gravityDirection * (_gravity * Time.deltaTime);
    }
    
    // protected override void Rotate(Vector3 direction)
    // {
    //     //base.Rotate(direction);
    //     _gravityDirection = direction * -1;
    //     _hasRotated = true;
    // }
    //
    //
    //
    //
    // protected override void ResetRotation()
    // {
    //     base.ResetRotation();
    //     _gravityDirection = Vector3.down;
    //     _hasRotated = true;
    // }
    
    
    
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.grey;
        Gizmos.DrawSphere(_centerPoint.position, 0.2f);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_centerPoint.position, _gravityDirection * _touchRange);
        
    }
}
