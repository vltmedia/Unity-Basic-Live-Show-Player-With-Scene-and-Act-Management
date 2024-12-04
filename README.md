# Unity Basic Live Show Player With Scene and Act Management

This project implements a basic Unity-based video playback and transition system with dynamically managed scenes and acts. The pipeline includes smooth fade transitions, black hold durations, and seamless integration with a modular UI. Video is output via `Spout` or `Syphon` to external applications for live shows or installations, the name is `ShowComposite`.

## Project Data

The project files are located at `Assets/LiveShow` folder. The project includes the following assets:

- Media
  - The `Media` folder contains video files for testing.
- Prefabs
  - The Prefabs folder contains UI prefabs for act and scene buttons, along with other managers.
- Scenes
  - Holds an example scene with the full pipeline `SceneActManager` and `VideoManager` set up at `Assets/LiveShow/Scenes/Example`.
- Scripts
  - The Scripts folder contains the main scripts for act and scene management, video playback, and UI integration.
- ShowObjects
  - This is where the example `ActData` and `SceneData` ScriptableObjects are located.

## Example

The example scene `Assets/LiveShow/Scenes/Example` demonstrates the full pipeline with act and scene buttons.

![image](https://github.com/vltmedia/Unity-Basic-Live-Show-Player-With-Scene-and-Act-Management/blob/main/Docs/images/PreviewImage.png?raw=true)

## Features

- **Dynamic Act and Scene Management**:

  - Acts and scenes are dynamically instantiated based on ScriptableObjects.
  - Acts are now loaded using the `ShowData` ScriptableObject, which organizes a collection of acts.
  - Each act has its own list of scenes, which are displayed as buttons.
- **Video Playback with Transitions**:

  - Smooth fade-in and fade-out transitions for videos.
  - Configurable black hold duration to eliminate blank video frames during loading.
  - Preloading ensures videos are ready before display.
- **UI Integration**:

  - Buttons for acts and scenes dynamically populate the UI.
  - Each act and scene have indicators for their active state.

## How It Works

1. **ScriptableObjects**:

   - `ShowData`: Represents the overall show, including its name and a list of acts (`ActData`).
   - `ActData`: Defines an act, including its name and a list of `SceneData`.
   - `SceneData`: Represents a scene with its name, input video (`videoIn`), and optional transition video (`videoOut`).
2. **VideoManager**:

   - Centralized control for video playback.
   - Uses a `RenderTexture` to display videos on UI or 3D objects.
   - Handles fade transitions and ensures videos are preloaded.
3. **SceneActManager**:

   - Loads data from the `ShowData` ScriptableObject.
   - Manages transitions between scenes and acts.
   - Dynamically generates act and scene buttons based on ScriptableObjects.
4. **ActButtonUI**:

   - Handles UI for individual act buttons.
   - Toggles the visibility of scenes belonging to the act.
5. **SceneButtonUI**:

   - Handles UI for individual scene buttons.
   - Triggers video transitions when clicked.

## Setup Instructions

### 1. Create ScriptableObjects

- Create a `ShowData` ScriptableObject to represent your overall show.
- Add `ActData` ScriptableObjects to the `acts` list in the `ShowData` object.
- Populate `ActData` with `SceneData` references.

### 2. Set Up the UI

- Create a `Canvas` for the UI.
- Add prefabs for:
  - **Act Button**: Includes `TextMeshProUGUI`, `Image` for indicator, and a toggleable scene container.
  - **Scene Button**: Includes `TextMeshProUGUI` and `Button`.

### 3. Configure VideoManager

- Add the `VideoManager` script to a GameObject in your scene.
- Assign the following references in the Inspector:
  - `VideoPlayer`: The Unity VideoPlayer component.
  - `RawImage`: The UI RawImage to display videos.
  - `FadeOverlay`: An `Image` for fade transitions.

### 4. Add SceneActManager

- Add the `SceneActManager` script to your scene.
- Assign references to `VideoManager` and act button prefabs.
- Set the `showData` field in `SceneActManager` to the appropriate `ShowData` ScriptableObject.

### 5. Test the System

- Play the scene in Unity.
- Click on act buttons to toggle scenes.
- Click on scene buttons to play videos with transitions.

## Customization

- **Fade Duration**: Set `fadeDuration` in `VideoManager` to adjust the fade transition time.
- **Black Hold Duration**: Set `blackHoldDuration` in `VideoManager` to control how long to hold the screen black before loading the next video.

## Example Workflow

1. Add a new `ShowData` ScriptableObject for your project.
2. Create `ActData` ScriptableObjects for acts and link them to the `ShowData` object.
3. Create `SceneData` objects for each scene in the acts.
4. Assign the `ShowData` object to the `SceneActManager`.
5. Play the project to see the dynamic UI and video transitions.

## Requirements

- Unity 2020.3 or later.
- TextMeshPro package installed.

## License

This project is licensed under the MIT License. See `LICENSE` for details.
