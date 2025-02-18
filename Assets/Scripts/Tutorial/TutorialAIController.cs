using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TrojanMouse.Inventory;
using Fungus;

/// <summary>
/// Class used for controlling the gruttels in the Tutorial scene, hardcoded for the sake of time
/// </summary>
public class TutorialAIController : MonoBehaviour
{
    public NavMeshAgent agent;
    public LayerMask litterLayerMask;
    [SerializeField] float pickupRange;
    public bool holdingLitter;
    public Animator animator;
    public bool sleeping;

    public Flowchart flowchart;

    private Inventory inventory; // reference to the equipper script
    private Equipper equipper; // reference to the equipper script
    Vector3 lastPosition;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        sleeping = true;
        inventory = GetComponent<Inventory>();
        equipper = GetComponent<Equipper>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        animator.SetBool("isMoving", ((transform.position - lastPosition).magnitude > 0) ? true : false);
        animator.SetBool("isSleeping", sleeping);
    }

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {
        lastPosition = transform.position;
    }

    /// <summary>
    /// Moves the gruttel to a point
    /// </summary>
    public void MoveToPoint(GameObject destination)
    {
        Debug.Log("Method Called, moving gruttel");

        StartCoroutine(Move(destination.transform.position));
    }

    public void Wait(float time)
    {
        StartCoroutine(Delay(time));
    }

    IEnumerator Move(Vector3 destination)
    {
        agent.SetDestination(destination);
        if (agent.remainingDistance <= .1f)
        {
            yield return new WaitForSeconds(.1f);
            Debug.Log("waited");
        }
    }

    IEnumerator Delay(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
    }

    public void PickupLitter()
    {
        // // if the inventory has slots left
        // if (inventory.HasSlotsLeft())
        // {
        //     Collider[] litter = Physics.OverlapSphere(transform.position, 15, litterLayerMask);
        //     LitterObject litterType = null;
        //     Transform litterObj = null;

        //     foreach (Collider obj in litter)
        //     {
        //         LitterObject type = obj.GetComponent<LitterObjectHolder>().type;
        //         bool cantPickup = powerUp.Type != type.type && type.type != PowerupType.NORMAL;

        //         if (!cantPickup)
        //         {
        //             agent.SetDestination(obj.transform.position);
        //             litterType = type;
        //             litterObj = obj.transform;
        //             break;
        //         }
        //     }
        //     if (litterType && Mathf.Abs((litterObj.position - transform.position).magnitude) <= pickupRange)
        //     {
        //         equipper.PickUp(litterObj, powerUp.Type, litterType);
        //     }
        // }
    }

    private void OnDrawGizmosSelected()
    {
        // JOSHS STUFF
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }

    public void SetGameObject(GameObject obj)
    {
        obj.SetActive(!obj.activeInHierarchy);
    }

    public void ToggleSleep(bool sleep)
    {
        sleeping = sleep;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "CommonLitter" && flowchart.GetBooleanVariable("directableToLitter"))
        {
            flowchart.ExecuteBlock("LitterCollecting");
        }
        else if (other.gameObject.name == "PrefabMachine" && flowchart.GetBooleanVariable("directableToMachine"))
        {
            flowchart.ExecuteBlock("MachineDepositing");
        }
    }

}
