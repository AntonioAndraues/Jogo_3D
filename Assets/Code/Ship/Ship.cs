//
// Copyright (c) Brian Hernandez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.
//
//REFERENCIA_https://github.com/brihernandez/ArcadeSpaceFlightExample
using UnityEngine;

/// <summary>
/// Ties all the primary ship components together.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ShipPhysics))]
[RequireComponent(typeof(ShipInput))]

public class Ship : MonoBehaviour
{
    public GameObject bullet;

    public float fireDelta = 0.5F;
    private float nextFire = 0.5F;
    private float myTime = 0.0F;

    public bool isPlayer = false;

    private ShipInput input;
    private ShipPhysics physics;    

    // Keep a static reference for whether or not this is the player ship. It can be used
    // by various gameplay mechanics. Returns the player ship if possible, otherwise null.
    public static Ship PlayerShip { get { return playerShip; } }
    private static Ship playerShip;

    // Getters for external objects to reference things like input.
    public bool UsingMouseInput { get { return input.useMouseInput; } }
    public Vector3 Velocity { get { return physics.Rigidbody.velocity; } }
    public float Throttle { get { return input.throttle; } }

    private void Awake()
    {
        input = GetComponent<ShipInput>();
        physics = GetComponent<ShipPhysics>();
    }

    private void Update()
    {
        // Pass the input to the physics to move the ship.
        physics.SetPhysicsInput(new Vector3(input.strafe, 0.0f, input.throttle), new Vector3(input.pitch, input.yaw, input.roll));

        // If this is the player ship, then set the static reference. If more than one ship
        // is set to player, then whatever happens to be the last ship to be updated will be
        // considered the player. Don't let this happen.
        if (isPlayer)
            playerShip = this;

        myTime = myTime + Time.deltaTime;

        if (Input.GetButton("Fire1") && myTime > nextFire)
        {
            nextFire = myTime + fireDelta;
            // MIRA é para distancias medias/longas
            GameObject instancia = Instantiate(bullet, transform.position + (transform.forward * 10), transform.rotation ) as GameObject;
            instancia.transform.Rotate(new Vector3(0, 10, 0));
            instancia.GetComponent<Rigidbody>().velocity = 20.0f * transform.forward;
            Destroy(instancia, 5.0f); // Destroi o tiro depois de 5 segundos
            nextFire = nextFire - myTime;
            myTime = 0.0F;
        }
    }
}
