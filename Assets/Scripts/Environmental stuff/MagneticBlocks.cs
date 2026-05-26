using UnityEngine;
using UnityEngine.InputSystem;

public class MagneticBlocks : MonoBehaviour
{
    //Note: This script will have to be changed if we are going through with this mechanic, everything marked between "------" will have to be changed / removed
    
    bool IsPlusPolarity;

    [SerializeField]
    Collider SpehereCollider;

    [SerializeField]
    GameObject PlusPolarityVisual;
    [SerializeField]
    GameObject MinusPolarityVisual;

    public Globalvariables PolarityVar;

    private void Update()
    {

        IsPlusPolarity = PolarityVar.PlusPolarity;

        if (IsPlusPolarity)
        {
            PlusPolarityVisual.SetActive(true);
            MinusPolarityVisual.SetActive(false);
        }
        else
        {
            PlusPolarityVisual.SetActive(false);
            MinusPolarityVisual.SetActive(true);
        }
    }




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

