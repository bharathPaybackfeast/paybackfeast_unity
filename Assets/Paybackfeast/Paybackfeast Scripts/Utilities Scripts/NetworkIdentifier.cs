
using UnityEngine;



/// <summary>
/// Checks for internet connection
/// </summary>
public class NetworkIdentifier : MonoBehaviour
{
    [SerializeField] float _checkDelay = 1f;

    string m_ReachabilityText;

    public NetworkIdentifier Instance { get; private set; }


    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance);
        }Instance = this;

    }

    private void Start()
    {
        InvokeRepeating(nameof(CheckInternetAvailability), 0f, _checkDelay);
    }

    void CheckInternetAvailability()
    {
        //Output the network reachability to the console window
        //Debug.Log("Internet : " + m_ReachabilityText);
        //Check if the device cannot reach the internet
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            //Change the Text
            m_ReachabilityText = "Not Reachable.";
        }
        //Check if the device can reach the internet via a carrier data network
        else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            m_ReachabilityText = "Reachable via carrier data network.";
        }
        //Check if the device can reach the internet via a LAN
        else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            m_ReachabilityText = "Reachable via Local Area Network.";
        }
    }

    
}
