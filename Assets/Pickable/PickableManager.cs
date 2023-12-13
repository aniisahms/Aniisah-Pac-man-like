using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickableManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private ScoreManager scoreManager;

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
        Debug.Log("Pickable: " + pickableList.Count);
        scoreManager.SetMaxScore(pickableList.Count);
    }

    private void OnPickablePicked(Pickable pickable)
    {
        pickableList.Remove(pickable);

        if (scoreManager != null)
        {
            scoreManager.AddScore(1);
        }
        
        if (pickable.pickableType == PickableType.PowerUp)
        {
            player.PickPowerUp();
        }
        
        if (pickableList.Count <= 0)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }
}
