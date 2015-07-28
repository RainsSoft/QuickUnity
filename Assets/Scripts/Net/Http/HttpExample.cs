using QuickUnity.Net.Http;
using UnityEngine;

namespace QuickUnity.Examples.Net.Http
{
    /// <summary>
    /// Http request example.
    /// </summary>
    public class HttpExample : MonoBehaviour
    {
        // Use this for initialization
        private void Start()
        {
            URLLoader loader = new URLLoader();
            loader.AddEventListener(HttpEvent.COMPLETE, OnResponse);
            URLRequest request = new URLRequest("http://docs.unity3d.com/ScriptReference/index.html");
            loader.Load(request);
        }

        /// <summary>
        /// Called when [response].
        /// </summary>
        /// <param name="evt">The evt.</param>
        private void OnResponse(Events.Event evt)
        {
            HttpEvent httpEvent = (HttpEvent)evt;
            Debug.Log(httpEvent.data);
        }
    }
}