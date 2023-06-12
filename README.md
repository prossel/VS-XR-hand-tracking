# VS XR hand tracking

Access XR Hand tracking events using Visual scripting in Unity.

[https://github.com/prossel/VS-XR-hand-tracking](https://github.com/prossel/VS-XR-hand-tracking)

![screenshot](Screenshots/XRHandTrackingEvents.png)

## Requirements

This package is based on XR Hands, XR Interaction toolkit and OpenXR Plugin.

## Installation

This package is based on these packages. Install them first.

* OpenXR Plugin
  * Package manager, install `OpenXR Plugin` from Unity registry
  * Accept the Input manager question
* XR Interaction Toolkit
  * Package manager, install `XR Interaction Toolkit` from Unity registry
  * Import `Starter Assets` samples
  * Import `Hands Interaction Demo`
* XR Hands v 1.2
  * Package manager, install from + Add package by name: com.unity.xr.hands
  * Optional: Import `HandVisualizer` samples

For Oculus builds, switch to Android platform in Build Settings

Configure XR Plug-in management

* Project settings > XR Plug-in Management
  * install if necessary
  * Desktop tab
    * Enable OpenXR
  * Android tab
    * Enable OpenXR
* Project settings > XR Plug-in Management > OpenXR
  * Desktop tab
    * Add one interaction profile (For Quest: Meta Quest and Oculus Touch)
    * Select OpenXR Feature groups
      * Hand Tracking Subsystem
      * Meta Hand Tracking Aim
  * Android tab
    * Add one interaction profile (For Quest: Meta Quest and Oculus Touch)
    * Select OpenXR Feature groups
      * Hand Tracking Subsystem
      * Meta Hand Tracking Aim
      * Meta Quest Support
* Project settings > XR Plug-in Management > Project validation
  * Fix all problems, if any

Initialize Visual Scripting

* Project settings > Visual Scripting
  * If there is only one button [ Initialize Visual Scripting ], just do.
  * Node library
    * Add Unity.XR.Hands
  * 
Now install this package

* VS XR hand tracking
  * Package Manager
  * Click the [ + ] button at top left
  * Add Package from git URL: `https://github.com/prossel/VS-XR-hand-tracking.git`
  * To update the package later, use the [ Update ] button

## Getting started

### Try the demo

* Copy the scene Packages/VS XR Hand tracking/Scenes/HandEventDemo to your assets
* Open, build or run

### XR Hand Tracking Events

* Create an empty GameObject
* Add a Script Machine component
* Use an embedded graph or create a new one
* Edit your graph
* Right click to add a node: XRHandTrackingEventsBridge
* Connect the trigger outputs to your logic

## History

See [CHANGELOG.md](CHANGELOG.md)
