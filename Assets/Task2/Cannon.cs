using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {
    public Vector3 TargetVeloctiy;
    public Vector3 TargetPosition;
    public GameObject Target;
    public float TimeToIntercept = 0;
    public Vector3 BulletVelocity;
    public GameObject Bullet;

    float Theta;


    // Use this for initialization
    void Start () {
        // get the Target Position and velocity.
        TargetPosition = Target.transform.localPosition;
        TargetVeloctiy = Target.GetComponent<TankMovement>().Velocity;
        // Fire cannon
        Fire();
    }

    // Update is called once per frame
    void Update () {

    }

    void Fire()
    {
        if (TimeToIntercept != 0)
        {
            // Get the Displacement of the tank after t seconds
            Vector3 deltaX = TargetPosition + TargetVeloctiy * TimeToIntercept;
            // get the vector from the cannon to the tank
            Vector3 u = deltaX - transform.localPosition;
            //Calculate the angle Theta from the cannon to the intercept.
            Theta = Mathf.Acos(u.x / u.magnitude);
            //rotate the cannon to the firing angle
            transform.rotation =  Quaternion.Euler(0, 0, Theta * Mathf.Rad2Deg);
            // Instantiate a bullet and parent it the the cannons parrent.
            GameObject clone = Instantiate(Bullet, transform.position, Quaternion.identity);
            clone.transform.parent = transform.parent;

            // calculate the bullet velocity.
            BulletVelocity = u / TimeToIntercept;
            clone.GetComponent<Bullet>().Velocity = BulletVelocity;

        }
    }
}
