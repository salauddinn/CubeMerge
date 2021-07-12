    using UnityEngine;

    public class CubeCollision : MonoBehaviour
    {
         Cube cube;

        void Awake()
        {
            cube = GetComponent<Cube>();
        }
        void OnCollisionEnter(Collision collision)
        {
            Cube otherCube = collision.gameObject.GetComponent<Cube>();
            if (otherCube != null && cube.cubeId>otherCube.cubeId)
            {
                if (otherCube.CubeNumber == cube.CubeNumber)
                {
                    Vector3 contactPoint = collision.contacts[0].point;

                    // check if cubes number less than max number in CubeSpawner:
                    if (otherCube.CubeNumber < CubeSpawner.Instance.maxCubeNumber)
                    {
                        // spawn a new cube as a result
                        Cube newCube = CubeSpawner.Instance.Spawn(cube.CubeNumber * 2, contactPoint + Vector3.up * 1.6f);
                        //push the new cube up and forward:
                        float pushForce = 2.5f;
                        newCube.CubeRigidBody.AddForce(new Vector3(0, .3f, 1f) * pushForce, ForceMode.Impulse);

                        // add some torque:
                        float randomValue = Random.Range(-20f, 20f);
                        Vector3 randomDirection = Vector3.one * randomValue;
                        newCube.CubeRigidBody.AddTorque(randomDirection);
                    }
                    Collider[] surroundedCubes = Physics.OverlapSphere(contactPoint, 2f);
                    float explosionForce = 400f;
                    float explosionRadius = 1.5f;


                    foreach (Collider coll in surroundedCubes)
                    {
                        if (coll.attachedRigidbody != null)
                            coll.attachedRigidbody.AddExplosionForce(explosionForce, contactPoint, explosionRadius);
                    }
                if (FX.Instance != null)
                {

                    FX.Instance.PlayCubeExplosionFX(contactPoint, cube.CubeColor);

                }

                CubeSpawner.Instance.DestroyCube(cube);
                    CubeSpawner.Instance.DestroyCube(otherCube);

                }
            }
        }
    }
