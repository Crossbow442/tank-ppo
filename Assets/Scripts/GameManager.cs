using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TankController blueTank;
    [SerializeField] TankController redTank;
    [SerializeField] MazeGenerator mazeGenerator;
    [SerializeField] float roundDelay;

    private int blueScore;
    private int redScore;
    private List<BulletController> bullets;
    


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
