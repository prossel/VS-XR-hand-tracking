using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Hands;

namespace com.prossel.XR.Hands
{
    // Visual scripting bridge for XRHandTrackingEvents component. Used by XRHandTrackingEventsBridge subgraph.
    // Auto register events, exposes XRHand hand to VS and forward events as UnityEvents.
    // TODO: Eventually replace by a Custom Scripting Event node which would merge this component and the subgraph.
    public class XRHandTrackingEventsVSBridge : MonoBehaviour
    {
        public XRHand hand;
        public bool handIsTracked => handTrackingEvents ? handTrackingEvents.handIsTracked : false;

        XRHandTrackingEvents handTrackingEvents;


        private void OnEnable()
        {
            //Debug.Log("OnEnable");
            handTrackingEvents = GetComponent<XRHandTrackingEvents>();
            if (handTrackingEvents == null)
            {
                //Debug.Log("add XRHandTrackingEvents component");
                handTrackingEvents = gameObject.AddComponent<XRHandTrackingEvents>();
            }

            if (handTrackingEvents != null)
            {
                //Debug.Log("registering event handers");
                handTrackingEvents.poseUpdated.AddListener(OnPoseUpdated);
                handTrackingEvents.jointsUpdated.AddListener(OnJointsUpdated);
                handTrackingEvents.trackingAcquired.AddListener(OnTrackingAcquired);
                handTrackingEvents.trackingLost.AddListener(OnTrackingLost);
                handTrackingEvents.trackingChanged.AddListener(OnTrackingChanged);
            }
        }

        private void OnDisable()
        {
            if (handTrackingEvents != null)
            {
                //Debug.Log("unregistering event handers");
                handTrackingEvents.poseUpdated.RemoveListener(OnPoseUpdated);
                handTrackingEvents.jointsUpdated.RemoveListener(OnJointsUpdated);
                handTrackingEvents.trackingAcquired.RemoveListener(OnTrackingAcquired);
                handTrackingEvents.trackingLost.RemoveListener(OnTrackingLost);
                handTrackingEvents.trackingChanged.RemoveListener(OnTrackingChanged);
            }
        }

        private void OnTrackingChanged(bool handIsTracked)
        {
            CustomEvent.Trigger(gameObject, "TrackingChanged", handIsTracked);
        }

        private void OnTrackingLost()
        {
            CustomEvent.Trigger(gameObject, "TrackingLost");
        }

        private void OnTrackingAcquired()
        {
            CustomEvent.Trigger(gameObject, "TrackingAcquired");
        }

        private void OnJointsUpdated(XRHandJointsUpdatedEventArgs args)
        {
            CustomEvent.Trigger(gameObject, "JointsUpdated", args.hand);
        }

        private void OnPoseUpdated(Pose pose)
        {
            CustomEvent.Trigger(gameObject, "PoseUpdated", pose);
        }
    }
}