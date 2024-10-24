# [AudioPlayer](../Runtime/AudioPlayer.cs)

Class in `Incantium.Audio` | Assembled in [`Incantium.Audio`](../README.md)

Extends [`SingleBehaviour`](SingleBehaviour.md) |
Requires [`AudioSource`](https://docs.unity3d.com/ScriptReference/AudioSource.html)

## Description

The AudioPlayer is the core of the Audio Player Plus package. This singleton class is responsible for playing audio
clips, looping over them when necessary, and updating the volume.

The AudioPlayer will always be automatically instantiated at the startup of the game if the 
[audio player settings](AudioPlayerSettings.md) are not changed. As long as it is instantiated at some point, the
audio player can be called by using `AudioPlayer.instance`.

## Variables

### :lock: [`MusicClip`](MusicClip.md) music

The audio clip currently in play.

### :lock: `bool` playOnAwake

Whether to play an audio clip at awake.

## Methods

### :green_book: `void` Play([`MusicClip`](MusicClip.md), `float`, [`FadeType`](FadeType.md))

Start playing a new audio clip with an amount of seconds between audio clips and a fade typing. How the audio clip will
be played is determined by its [type](MusicType.md).

### :green_book: `void` Stop(`float`)

Stop the audio player from playing the current audio clip. Required an amount of seconds to fade out the last playing 
audio clip. Setting this value to 0 will result in an instant stop.

### :green_book: `void` Pause()

Pauses the audio player from playing the current audio clip. This, opposite to [stopping](#green_book-void-stopfloat) 
the audio player, makes it possible to [resume](#green_book-void-resume) the audio player at a later moment.

### :green_book: `void` Resume()

Resumes the audio player at the latest point it was [paused](#green_book-void-pause).
