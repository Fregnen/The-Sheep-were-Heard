using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFence : MonoBehaviour
{
    private GameObject fencePrefab;
    private GameObject fence;    

    [SerializeField]
    private Vector3 startPosition;
    Vector3 size;

    private void Awake() {
        
        fencePrefab = Resources.Load("Fence Type1 01") as GameObject;

    }
 
    void Start()
    {
        
        // Resize prefab
        size = new Vector3(
            fencePrefab.transform.localScale.x/transform.localScale.x,
            fencePrefab.transform.localScale.y/transform.localScale.y,
            fencePrefab.transform.localScale.z/transform.localScale.z 
        );
        Debug.Log("size = " + size);

        // Get the field object
        startPosition = new Vector3(
            transform.position.x-(transform.localScale.x/2),
            transform.position.y,
            transform.position.z-(transform.localScale.z/2)   
        );
        Debug.Log("startPosition = " + startPosition);

        BuildFence();


    }

    
    void BuildFence()
    {

        float currentX = startPosition.x;
        float endX = startPosition.x + transform.localScale.x;
        float currentZ = startPosition.z;
        float endZ = startPosition.z + transform.localScale.z;

        for(int i = 0; i < 2; i++)
        {
            
            int j = 1;
            // from start: (x = -50, z = -50) to end: (x = 50, z = -50) 
            //// while currentX < endX --> build fence
            while(currentX < endX)
            {
                currentX = startPosition.x + j*2f;
                fence = Instantiate(fencePrefab, new Vector3 (currentX, startPosition.y, currentZ), Quaternion.identity, transform);
                fence.transform.localScale = size;
                
                fence.layer = 6;
                j++;
            }
            
            currentX = startPosition.x;
            currentZ = endZ;
 
        }    

        for(int i = 0; i < 2; i++)
        {  
            currentZ = startPosition.z;
            int j = 0;
            // from start: (x = -50, z = -50) to end: (x = 50, z = -50) 
            //// while currentX < endX --> build fence
            while(currentZ < endZ-2)
            {
                currentZ = startPosition.z + j*2f;
                fence = Instantiate(fencePrefab, new Vector3 (currentX, startPosition.y, currentZ), Quaternion.identity, transform);
                fence.transform.Rotate(new Vector3(0f, 90f, 0f));
                fence.transform.localScale = size;
                
                fence.layer = 6;
                j++;
            }
            
            
            currentX = endX;
 
        } 

    }


}