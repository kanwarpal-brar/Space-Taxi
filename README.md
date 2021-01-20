# Space-Taxi
Space Taxi is a game about exploring a planet in your tiny triangle-shaped spaceship: fighting against gravity and avoiding mountains, in order to help the tiny people of the planet get from once place to another.

The key feature of Space Taxi is a basic simulation of real physics. I got the idea in Physics class, when learning about [Newton's Law of Universal Gravitation](https://en.wikipedia.org/wiki/Newton%27s_law_of_universal_gravitation), and basic equation for this concept is implemented in the code of the game.

Space Taxi makes use of a variety of Unity's features, notably:
* Particle System
* Shader/Lighting System/Post-Processing
* Physics System (For movement by adding forces to a simulated RigidBody)
* Cinemachine

There's a release available [here](https://github.com/JustColdToast/Space-Taxi/releases)

## Take a look at some gameplay:
Takeoff takes time as gravity fights back. The ship's acceleration is adjustable in code, but is intended to be low
<p>
  <img src="https://thumbs.gfycat.com/SpanishBarrenIndochinahogdeer-size_restricted.gif">
 </p>
 
 Be careful when flying around, you will slowly move towards the ground. The particle trail behind you helps you keep track of the direction and magnitude of your velocity, so you can aim in the correct direction to slow down.
<p>
  <img src="https://thumbs.gfycat.com/GenuineJitteryGalapagossealion-size_restricted.gif">
 </p>

The planet is not huge, but is sizable, with a variety of different terrain levels, features, and even a city (with an especially difficult to approach landing zone).
