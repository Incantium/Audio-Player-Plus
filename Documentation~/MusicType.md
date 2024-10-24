# [MusicType](../Runtime/MusicType.cs)

Enum in `Incantium.Audio` | Assembled in [`Incantium.Audio`](../README.md)

## Description

MusicType is an enum that denote how the [audio player](AudioPlayer.md) should play an audio clip.

## Variables

### :green_book: `MusicType` Smart

Uses a defined start and end boundary in the audio clip to seamlessly loop the main section. Use this type for advanced
audio clips with a distinct middle section that loops on its own.

### :green_book: `MusicType` Loop

Plays the entire audio clip in a loop. Use this type for general audio clips.

### :green_book: `MusicType` Once

Plays the audio clip in its entirety once. Use this type for sound effect audio clips.
