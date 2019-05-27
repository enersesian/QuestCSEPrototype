using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Parent class for creating cutom events
/// </summary>
public class HCInvestigatorEvent : MonoBehaviour {

    /// <summary>
    /// The name of the event
    /// </summary>
    public string eventName;
	
    /// <summary>
    /// Register event with the HCInvestigatorManager
    /// </summary>
    /// <param name="method">A UnityAction that has callback functions associated with it</param>
    protected void RegisterToManager(UnityAction method)
    {
        HCInvestigatorManager.instance.AddToDictionary(eventName);
        HCInvestigatorManager.instance.AddActionToDictionary(eventName, method);
    } 
}
