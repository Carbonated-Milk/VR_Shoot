using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool gameStart;
    public static int money;
    public static void StartGame()
    {
        FindObjectOfType<AudioManager>().Stop("Ambiant Nature");
        FindObjectOfType<AudioManager>().Play("Theme");
        FindObjectOfType<Lives>().enabled = true;
        gameStart = true;
        TurnOnShoot();
    }

    public static void TurnOnShoot()
    {
        foreach(FruitShoot fruitShooter in Resources.FindObjectsOfTypeAll(typeof(FruitShoot)) as FruitShoot[])
        {
            fruitShooter.TurnOn();
        }
        foreach(SawBladeThrower bladeShooter in Resources.FindObjectsOfTypeAll(typeof(SawBladeThrower)) as SawBladeThrower[])
        {
            bladeShooter.enabled = true;
        }
    }
    public static void TurnOffShoot()
    {
        foreach(FruitShoot fruitShooter in Resources.FindObjectsOfTypeAll(typeof(FruitShoot)) as FruitShoot[])
        {
            fruitShooter.TurnOff();
        }
        foreach(SawBladeThrower bladeShooter in Resources.FindObjectsOfTypeAll(typeof(SawBladeThrower)) as SawBladeThrower[])
        {
            bladeShooter.enabled = false;
        }
    }


}
