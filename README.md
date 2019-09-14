<h1 align="center">Welcome to FixTheOD 👋</h1>
<p>
  <img alt="Version" src="https://img.shields.io/badge/version-1.0-blue.svg?cacheSeconds=2592000" />
  <a href="https://github.com/ertugrul013/CultFiction/CultFiction/README.md">
    <img alt="Documentation" src="https://img.shields.io/badge/documentation-yes-brightgreen.svg" target="_blank" />
  </a>
  <a href="https://github.com/ertugrul013/CultFictio/LICENSE">
    <img alt="License: MIT" src="https://img.shields.io/badge/License-MIT-yellow.svg" target="_blank" />
  </a>
</p>

> People are having overdoses FIX by stabbing a siringe in their heart. Aim carefully or you will be put in jail

### 🏠 [Homepage](https://github.com/ertugrul013/CultFiction)

## Idea

In the scene of pulp fiction where the girl has an overdose.
> see link: https://youtu.be/tLCTSMBUfmE?t=16 

Your job in this game is help you junkies tot stay alive by stabbing a needle in their heart. But be careful all the junkies have a different hit area so aim carefully or they will die.

## 🌊 Script Flow

### Main script

Main script is called GameController. This script will handle all the different communications between all the parts of the game.

### 😰 The Junkies

The junkies have script called PatientController that is dirived from the Patient class.
These classes handle the following:
  * Spawning 
  * Deleting
  * State manegment
  * Hit area creation

### 😊  The Player

The playerController script handels all the interactions the player makes or is going to make.
This is all the thing the player keeps handels
  * Shooting raycast to check if the patient has been hit.
  * Calls the UI function to add points on the screen.

### UI

The UIController handels showing score and handel the overal menu.

  * Play/Pause
  * scene switching
  * score display
  
## Usage

```
Import the project in unity and you will be able to play the development build or download a build
```

## Author

👤 **ertugrul013**

* Github: [@ertugrul013](https://github.com/ertugrul013)

## 📝 License

Copyright © 2019 [ertugrul013](https://github.com/ertugrul013).<br />
This project is [MIT](https://github.com/ertugrul013/CultFictio/LICENSE) licensed.