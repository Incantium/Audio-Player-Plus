# Audio Player Plus

`Unity 2022.3`
`.NET Standard 2.1`
`C# 9.0`

## Overview

The Audio Player Plus is a custom-made Unity Package specifically made to make it easier to loop audio clip and control 
their volume.

## Workflow

### 1. Create music clips

Firstly, it is important to create a new [music clip](Documentation~/MusicClip.md) instance for each audio clip in you 
game. Audio Player Plus requires you to use this new data structure for more control over the audio settings per audio clip. 
This process is done by right-clicking on an audio clip in Unity and then selecting "Create → Data → Music Clip".

Furthermore, it is possible to make the [music clip](Documentation~/MusicClip.md) much better with its new settings,
which can be read [here](Documentation~/MusicClip.md).

### 2. The audio player

The Audio Player Plus package contains an [audio player](Documentation~/AudioPlayer.md) that is the main control point 
for updating the music that should be played. 

At its core, this [audio player](Documentation~/AudioPlayer.md) is a singleton and should not be added as a component to
any game object. This package will, instead, automatically instantiate an [audio player](Documentation~/AudioPlayer.md) 
for you at the start of playing the game. It is advisable to make use of its automatic instance of the 
[audio player](Documentation~/AudioPlayer.md) into the game. This automatic instantiation can be turned off in the 
[audio player settings](Documentation~/AudioPlayerSettings.md) if you choose to do so.

### 3. Start the music

Now, it is finally possible to start the music. [Here](Documentation~/AudioPlayer.md) are the methods you can use to 
start the music. And here below is a code snippet how this package can be used.

```csharp
using Incantium.Audio;
using UnityEngine;

public class ExampleClass : MonoBehaviour
{
    [SerializeField]
    private MusicClip music;

    private void Start()
    {
        music.Play(2f, FadeType.CrossFade);
    }
}
```

## References

| Class                                                        | Description                                                                                                                  |
|--------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------|
| [AudioPlayer](Documentation~/AudioPlayer.md)                 | The audio player of the Audio Player Plus package.                                                                           |
| [AudioPlayerSettings](Documentation~/AudioPlayerSettings.md) | The settings for the audio player.                                                                                           |
| [FadeType](Documentation~/FadeType.md)                       | Different types of fading between audio clip.                                                                                |
| [MusicClip](Documentation~/MusicClip.md)                     | An audio clip with more control over looping and volume.                                                                     |
| [MusicType](Documentation~/MusicType.md)                     | The type of audio clip to play.                                                                                              |
| [SingleBehaviour](Documentation~/SingleBehaviour.md)         | The singleton pattern for [MonoBehaviour](https://docs.unity3d.com/6000.0/Documentation/ScriptReference/MonoBehaviour.html). |

## Frequently Asked Questions

### Does the Audio Player Plus have a solution for 3D sound?

Simple said: It unfortunately hasn't.

Audio Player Plus is specifically made for looping background music and 2D sound effect. It does not have a mechanism
to detain at which position in world space an audio clip needs to be played. Audio clips that need to be played in a 3D 
environment is not within the scope of this package.
