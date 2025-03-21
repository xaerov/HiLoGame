# Introduction

This is a solution for a web-based HiLo game

# Details

## Objective

Players take turns guessing a hidden number within a predefined range. The goal is to guess the correct number in the fewest attempts.

## Game rules

The game is played by 2 players.

A random number is generated by the system within a predefined range (1 to 5).

Players take turns guessing the number.

The system provides feedback after each guess:

"Hi" if the guess is greater than the target number.

"Lo" if the guess is less than the target number.

The first player who guesses the number wins.

## Remarks

I've spent just a couple of hours on the solution. Thus, it's quite raw, with no attention paid to the details, but still, it could demonstrate the overall design and architecture.

# Getting Started

1. Runtime

   ASP.NET Core 8 + React 19 + SignalR (websockets)

2. Core Settings

   No configuration is needed

3. DB
   The solution uses an in-memory database

# Build and Test

- Built under Visual Studio 2022
- Tests are written by XUnit

# Run

    Open the solution via the Visual Studio 2022. Ensure that Startup Projects are set to multiple ones (hilogame.client and HiLoGame.Server). Then, press the Run button
