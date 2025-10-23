15-Days-15-Games-Day-12-Hexa-Havoc
This is the twelfth game from my "15 Days 15 Games" challenge. It is a 3D top-down arena survival game featuring physics-based combat, a multi-state power-up system, progressive enemy waves, and boss encounters.

🚀 About the Game
The player controls a sphere on a hexagonal platform and must push incoming enemies off the edge to survive. Enemies spawn in waves of increasing numbers. Every few waves, a boss enemy spawns. The player can collect temporary power-ups that grant special abilities: enhanced pushback, homing rockets, or a ground smash attack. The game ends if the player falls off the platform.

💡 Technical Highlights
Engine: Unity (3D Physics)

Advanced Power-Up System: Implemented using an enum (PowerUpType) to manage player states. Features include:

Pushback: Applies amplified ForceMode.Impulse to enemies on collision.

Homing Rockets: Instantiates projectiles (RocketBehaviour.cs) that use vector math ((target.transform.position - transform.position).normalized) and transform.LookAt() to seek enemies, applying force on impact.

Ground Smash: Utilizes a Coroutine (IEnumerator Smash()) to manage a multi-stage ability (vertical ascent, hover, rapid descent). Uses Physics.OverlapSphere and AddExplosionForce to affect nearby enemies upon landing.

Sophisticated Wave & Boss Spawning: The SpawnManager tracks remaining enemies (FindObjectsByType<Enemy>) to trigger new waves. It uses the modulo operator (% bossRound) to determine when to spawn a boss instead of regular enemies. Bosses (Enemy.cs with isBoss = true) have unique behavior, spawning mini-enemies at intervals.

Enemy AI: Enemies implement basic "seek" behavior by calculating the direction vector towards the player ((player.transform.position - transform.position).normalized) and applying force via Rigidbody.AddForce.

Modular Design: Clear separation of concerns between PlayerController, Enemy, SpawnManager, GameManager, and individual power-up/projectile scripts (PowerUp.cs, RocketBehaviour.cs).

▶️ Play the Game!
You can play the game in your browser on its itch.io page: [Link to your new itch.io page for this game]
