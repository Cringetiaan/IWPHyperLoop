using UnityEngine;
using UnityEngine.InputSystem;

public class MagneticBlocks : MonoBehaviour
{
    //Note: This script will have to be changed if we are going through with this mechanic, everything marked between "------" will have to be changed / removed
    
    public bool IsPlusPolarity;

    [SerializeField]
    Collider SpehereCollider;

   


    private void OnTriggerStay(Collider other)
    {
       
        if (other.CompareTag("Player"))
        {
            //float Force = (gameObject.transform.position - other.transform.position).magnitude;
            //Debug.Log((gameObject.transform.position - other.transform.position).magnitude);
            if (IsPlusPolarity)
                {
                    other.GetComponent<Rigidbody>().AddForce((transform.position - other.transform.position).normalized * 50, ForceMode.Acceleration);
                }
                else
                {
                    other.GetComponent<Rigidbody>().AddForce((other.transform.position - transform.position).normalized * 50, ForceMode.Acceleration);
                }
            }
        }
    }

