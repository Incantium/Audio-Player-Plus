# [FadeType](../Runtime/FadeType.cs)

Enum in `Incantium.Audio` | Assembled in [`Incantium.Audio`](../README.md)

## Description

FadeType is an enum that denote how the [audio player](AudioPlayer.md) should change between the current and a new audio
clip.

## Variables

### :green_book: `FadeType` Wait

Waits until the last audio clip has faded out before fading in the new clip.

### :green_book: `FadeType` CrossFade

Directly fading in the new audio clip while fading out the last clip.
