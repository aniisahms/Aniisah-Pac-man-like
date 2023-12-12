using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
{
    [SerializeField] private Player player;

    private List<Pickable> pickableList = new List<Pickable>();
    void Start()
    {
        InitPickableList();
    }

    private void InitPickableList()
    {
        Pickable[] pickableObjects = GameObject.FindObjectsOfType<Pickable>();

        for (int i = 0; i < pickableObjects.Length; i++)
        {
            pickableList.Add(pickableObjects[i]);
            pickableObjects[i].OnPicked += OnPickablePicked;
        }
    }

    private void OnPickablePicked(Pickable pickable)
    {
        pickableList.Remove(pickable);
        
        if (pickable.pickableType == PickableType.PowerUp)
        {
            player.PickPowerUp();
        }
        
        if (pickableList.Count <= 0)
        {
            Debug.Log("Win!");
        }
    }
}
