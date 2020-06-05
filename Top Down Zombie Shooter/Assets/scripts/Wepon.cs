using UnityEngine;
using System.Collections;

public class Wepon : MonoBehaviour{

    public bool pistol;
    public bool machinegun;
    public bool sniper;

    public GameObject bullet;
    public Transform shootPoint;

    //pistol
    public float MNpistolShootRotation = -3;
    public float MXpistolShootRotation = 3;

    //machinegun
    public float MNmachinegunShootRotation = -8;
    public float MXmachinegunShootRotation = 8;

    private float machinegunTimer = 0;
    public float machinegunTime = 0.1f;

    //sniper
    public float MNsniperShootRotation = 0;
    public float MXsniperShootRotation = 0;

    void Update(){
        if (pistol == true && Input.GetButtonDown("Fire1"))
        {
            shootPistol();
        }

        machinegunTimer -= Time.deltaTime;

        if (machinegun == true && Input.GetButtonDown("Fire1"))
        {
            machinegunTimer = machinegunTime;
        }

        if (machinegun == true && Input.GetButton("Fire1") && machinegunTimer < 0) 
        {
            machinegunTimer = machinegunTime;
            shootMachinegun();
        }
    }

    void shootPistol()
    {
        Vector3 randomRotation = new Vector3(0, 0, Random.Range(MNpistolShootRotation, MXpistolShootRotation));

        Instantiate(bullet, shootPoint.position, shootPoint.rotation * Quaternion.Euler(randomRotation));
    }

    void shootMachinegun()
    {
        Vector3 randomRotation = new Vector3(0, 0, Random.Range(MNmachinegunShootRotation, MXmachinegunShootRotation));

        Instantiate(bullet, shootPoint.position, shootPoint.rotation * Quaternion.Euler(randomRotation));
    }
}
