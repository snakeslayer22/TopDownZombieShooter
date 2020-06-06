using UnityEngine;
using System.Collections;

public class Wepon : MonoBehaviour{

    public GameObject bullet;
    public Transform shootPoint;

    //pistol
    public bool pistol;

    public float MNpistolShootRotation = -3;
    public float MXpistolShootRotation = 3;

    //assultrifle
    public bool assultrifle;

    public float MNassultrifleShootRotation = -8;
    public float MXassultrifleShootRotation = 8;

    private float assultrifleTimer = 0;
    public float assultrifleTime = 0.1f;

    //sniper
    public bool sniper;

    public float MNsniperShootRotation = 0;
    public float MXsniperShootRotation = 0;

    private float sniperTimer = 0;
    public float sniperTime = 1f;

    //LMG
    public bool LMG;

    public float MNLMGShootRotation = -12;
    public float MXLMGShootRotation = 12;

    private float LMGTimer = 0;
    public float LMGTime = 0.03f;

    void Update(){
        //pistol
        if (pistol == true)
        {
            BulletMove.pistol = true;
            BulletMove.assultrifle = false;
            BulletMove.sniper = false;
            BulletMove.LMG = false;
        }

        if (pistol == true && Input.GetButtonDown("Fire1"))
        {
            shootPistol();
        }
        //assultrifle
        else if (assultrifle == true)
        {
            assultrifleTimer -= Time.deltaTime;

            BulletMove.pistol = false;
            BulletMove.assultrifle = true;
            BulletMove.sniper = false;
            BulletMove.LMG = false;

            if (assultrifle == true && Input.GetButtonDown("Fire1"))
            {
                assultrifleTimer = assultrifleTime;
            }

            if (assultrifle == true && Input.GetButton("Fire1") && assultrifleTimer < 0)
            {
                assultrifleTimer = assultrifleTime;
                shootAssultrifle();
            }
        }
        //sniper
        else if (sniper == true)
        {
            sniperTimer -= Time.deltaTime;

            BulletMove.pistol = false;
            BulletMove.assultrifle = false;
            BulletMove.sniper = true;
            BulletMove.LMG = false;

            if (sniper == true && Input.GetButtonDown("Fire1") && sniperTimer < 0)
            {
                sniperTimer = sniperTime;
                shootSniper();
            }
        }
        //LMG
        else if (LMG == true)
        {
            LMGTimer -= Time.deltaTime;

            BulletMove.pistol = false;
            BulletMove.assultrifle = false;
            BulletMove.sniper = false;
            BulletMove.LMG = true;

            if (LMG == true && Input.GetButtonDown("Fire1"))
            {
                LMGTimer = LMGTime;
            }

            if (LMG == true && Input.GetButton("Fire1") && LMGTimer < 0)
            {
                LMGTimer = LMGTime;
                shootLMG();
            }
        }
    }

    private void NewMethod(Vector3 randomRotation)
    {
        Instantiate(bullet, shootPoint.position, shootPoint.rotation * Quaternion.Euler(randomRotation));
    }

    void shootPistol()
    {
        Vector3 randomRotation = new Vector3(0, 0, Random.Range(MNpistolShootRotation, MXpistolShootRotation));
        NewMethod(randomRotation);
    }

    void shootAssultrifle()
    {
        Vector3 randomRotation = new Vector3(0, 0, Random.Range(MNassultrifleShootRotation, MXassultrifleShootRotation));
        NewMethod(randomRotation);
    }

    void shootSniper()
    {
        Vector3 randomRotation = new Vector3(0, 0, Random.Range(MNsniperShootRotation, MXsniperShootRotation));
        NewMethod(randomRotation);
    }

    void shootLMG()
    {
        Vector3 randomRotation = new Vector3(0, 0, Random.Range(MNLMGShootRotation, MXLMGShootRotation));
        NewMethod(randomRotation);
    }
}
