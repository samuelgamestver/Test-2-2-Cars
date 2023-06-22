using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    [SerializeField] CarPhisic[] Cars;

    [SerializeField] Transform[] CamTargets;

    [SerializeField] SmoothFollow Camera;

    [SerializeField] Transform SpeedArrow;

    int SelectCar;

    public void PrevCar()
    {
        if (SelectCar > 0)
            SelectCar--;
        else
            SelectCar = Cars.Length - 1;

        Camera.target = CamTargets[SelectCar];
    }

    public void NextCar()
    {
        if (SelectCar < Cars.Length - 1)
            SelectCar++;
        else
            SelectCar = 0;

        Camera.target = CamTargets[SelectCar];
    }

    public void Accel(int value)
    {
        Cars[SelectCar].Accel = value;
    }

    public void Brake(int value)
    {
        Cars[SelectCar].Brake = value;
    }

    public void Steer(int value)
    {
        Cars[SelectCar].Steer = value;
    }


    private void Update()
    {
        SpeedArrow.eulerAngles = new Vector3(0, 0, -Cars[SelectCar].Speed * 1.085f);

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
