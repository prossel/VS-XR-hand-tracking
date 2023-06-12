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
        public Handedness handedness = Handedness.Invalid;

        public XRHand hand;
        public bool handIsTracked => handTrackingEvents ? handTrackingEvents.handIsTracked : false;

        XRHandTrackingEvents handTrackingEvents;

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

                handTrackingEvents = null;
            }
        }

        private void Update()
        {
            // Wait for a valid handedness value
            if (handTrackingEvents == null && handedness != Handedness.Invalid)
            {
                // Try to use exisiting with matching handedness
                foreach (var comp in GetComponents<XRHandTrackingEvents>())
                {
                    if (comp.handedness == handedness)
                    {
                        handTrackingEvents = comp;
                        break;
                    }
                } 

                // If not found, add one
                if (handTrackingEvents == null)
                {
                    handTrackingEvents = gameObject.AddComponent<XRHandTrackingEvents>();
                    handTrackingEvents.handedness = handedness;
                }

                // Register event handlers
                //Debug.Log("registering event handers");
                handTrackingEvents.poseUpdated.AddListener(OnPoseUpdated);
                handTrackingEvents.jointsUpdated.AddListener(OnJointsUpdated);
                handTrackingEvents.trackingAcquired.AddListener(OnTrackingAcquired);
                handTrackingEvents.trackingLost.AddListener(OnTrackingLost);
                handTrackingEvents.trackingChanged.AddListener(OnTrackingChanged);
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