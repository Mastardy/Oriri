using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class HackerController : MonoBehaviour
{
    [SerializeField] private RowColumn rowColumnGame;
    
    [SerializeField] private float maxEnergy = 10;
    [SerializeField] private RectTransform energyUI;
    [SerializeField] private TextMeshProUGUI energyUIText;
    private float energy;

    [SerializeField] private InputAction restoreEnergy;
    [SerializeField] private InputAction killRobot;

    [SerializeField] private GameObject player;

    [SerializeField] private GameObject hackerCube;
    
    private Platform closestPlatform;
    private Robot closestRobot;

    private bool playing;

    private float lastRobotKill;

    private void Awake()
    {
        restoreEnergy.performed += GenerateGame;

        restoreEnergy.Enable();
        killRobot.Enable();

        rowColumnGame.GameCompleted += RestoreEnergy;
        RestoreEnergy();
    }

    private void Update()
    {
        energyUIText.text = energy + " / " + maxEnergy;
        energyUI.localScale = new Vector3(energy/maxEnergy, 1, 1);
        
        if (!closestRobot)
        {
            hackerCube.transform.position = new Vector3(3000, -3000, 3000);
            return;
        }

        hackerCube.transform.position = closestRobot.transform.position + Vector3.up * 2;
        
        if (energy < 1 || playing) return;
        
        if (killRobot.WasPressedThisFrame())
        {
            lastRobotKill = Time.time;
            energy -= 1;
            closestRobot.Kill();
        }
    }

    private void FixedUpdate()
    {
        Collider[] results = new Collider[256];
        var size = Physics.OverlapSphereNonAlloc(player.transform.position, 15, results, Physics.AllLayers);

        var platforms = new List<Platform>();
        var robots = new List<Robot>();
        
        for (int i = 0; i < size; i++)
        {
            var result = results[i];

            if (result.TryGetComponent(out Platform platform))
            {
                platforms.Add(platform);
            }

            if (result.TryGetComponent(out Robot robot))
            {
                robots.Add(robot);
            }
        }

        if (platforms.Count == 0) closestPlatform = null;
        else
        {
            closestPlatform = platforms[0];
            for(int i = 1; i < platforms.Count; i++)
            {
                var platform = platforms[i];
                var closestPlatformDistance = Vector3.Distance(player.transform.position, closestPlatform.transform.position);
                
                if (Vector3.Distance(platform.transform.position, player.transform.position) < closestPlatformDistance)
                {
                    closestPlatform = platform;
                }
            }
        }
        
        if (robots.Count == 0) closestRobot = null;
        else
        {
            closestRobot = robots[0];
            for(int i = 1; i < robots.Count; i++)
            {
                var robot = robots[i];
                var closestRobotDistance = Vector3.Distance(player.transform.position, closestRobot.transform.position);
                
                if (Vector3.Distance(robot.transform.position, player.transform.position) < closestRobotDistance)
                {
                    closestRobot = robot;
                }
            }
        }
    }
    
    private void GenerateGame(InputAction.CallbackContext context)
    {
        if (!(energy < maxEnergy * 0.75)) return;
        
        rowColumnGame.GenerateGame();
        playing = true;
    }
    
    private void RestoreEnergy()
    {
        energy = maxEnergy;
        playing = false;
    }
}