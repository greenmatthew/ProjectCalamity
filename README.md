# Project Calamity

![Project Calamity Preview](Docs/README.md/preview.png)

## Links #
- [UTA Senior Designs Blog](https://blog.uta.edu/cseseniordesign/2022/10/21/project-calamity/)
- [Matthew's Personal Git Repo](https://git.matthewgreen.gg/mgreen/ProjectCalamity.git)
- [Github Repo](https://github.com/greenmatthew/ProjectCalamity)
- [Matthew's Initial Ideas Document](https://github.com/greenmatthew/ProjectCalamity/blob/master/_Resources/Notes/Ideas.pdf)

## Content #
- [Purpose](#purpose)
- [Summary](#summary)
  - Demo Video
- [Story](#story)
- [Setting](#setting)
  - Environment Screenshots
- [Player Management Station (PMS)](#player-management-station-pms)
  - PMS Screenshots
- [Inventory System](#inventory-system)
  - Inventory Screenshots
- [Health System](#health-system)
- [Combat](#combat)
  - Enemies Screenshots
- [Conclusion](#conclusion)
- [Future Work](#future-work)

## Purpose
This is a video game project made for the 2022 Summer & Fall semester CSE Senior Design I & II courses at the University of Texas at Arlington.

## Summary
This is a sci-fi horror tactical FPS role playing game, made in Unity, where you take control of a bounty hunter who has crash landed on a mining colony planet, filled with horrific and violent alien creatures and people trying to kill you. Collect and craft items to help you survive and finish quests as you undercover the story of this planet and its humanoid inhabitants and their secret plans.

Highly inspired by other games of the same genres, most notably the Dead Space franchise and others like it such as the Resident Evil franchise and The Callisto Protocol for the setting, horror atmosphere, and some parts of the gameplay. Then the gameplay being further enhanced with small parts borrowed from other games: Dark Souls franchise, Subnautica, Chernobylite, Escape From Tarkov, and SWAT4.

*** ***Notice: Many of the originally planned features are missing or incomplete. Which is explained about in the conclusion section near the bottom. However, I will still describe these features below and state their status in the final build of the game.*** ***

https://user-images.githubusercontent.com/45467733/223000314-d6c8441f-412b-4bd8-bc7d-292791805a3f.mp4

Project Calamity Demo Video: Recorded by one of my team members for the project Will Kembel. If the video is not showing try the [original YouTube upload](https://youtu.be/olbn0plXHug).

<p align="center">
    <a href="https://youtu.be/JKdABwatEUQ">
        <img
            src="Docs/README.md/demo-video-thumbnail.png"
            alt="Project Calamity Demo"
        >
    </a>
    <br>
    Recorded by one of my team members for the project Will Kembel. If the video is not showing try the <a href="https://youtu.be/olbn0plXHug">original YouTube upload</a> by Will Kembel.
</p>


## Story
You take control of a bounty hunter who just got his biggest contract. Unfortunately, after a failed attempt to eliminate the target he crash landed on a prison mining colony. You need to gather the resources and items needed to repair his ship to finish his contract and get revenge for getting shot down.

The mining colony is home to alien creatures, robots, and humanoid lowlifes, prisoners, and employees for the evil corporation in charge of the colony. As you explore more and finish more quests for people around the planet as well as missions for the corporation itself, you begin to learn more about the corporation's sinister plans for the mining on this planet. Will you try and stop them, or do you want to join them for your own selfish benefit?

## Setting
The planet's surface is mainly dirt and rock, but there are small amounts of resource rich flora across the surface. With many dangerous creatures and robots roaming the surface and caves also worth searching for resources. There are caves littered all around the surface, all plentifully filled with raw ores waiting to be mined; copper, titanium, iron, gold, silver, and more, however, the corporation does not seem to be mining these particularly common but useful ores right outside their compound.

There is a massive compound on the equator of the planet, owned by the corporation. Inside there is many different buildings and areas. Living quarters for prisoners, guards, engineers, and all sorts of other workers. Office spaces. Laboratories. Vehicle bays for spaceships and land vehicles. One gigantically tall building, where the top executives running the planet live and manage the planet. Then finally one massive quarry in the center of the compound, so far down you can't see anything at the bottom with large elevators that take people such as prisoners and guards down to the bottom to mine.

The bounty hunter's spaceship crash lands on the outskirts of the compound. In a hot spot of caves and plenty and flora and fauna to fulfill upgrading your gear and start repairs on your ship, if you're willing to do a little exploring around.

*** ***Notice: In the current build, there is no compound nor any caves to find. Just a few buildings and a few aggressive alien creatures sprinkled around the map.*** ***

![Project Calamity Environment 0](Docs/README.md/preview.png)
![Project Calamity Environment 1](Docs/README.md/environment-1.png)
![Project Calamity Environment 2](Docs/README.md/environment-2.png)

## Player Management Station (PMS)
The Player Management Station (PMS) devices are a critical part of the gameplay loop and found all over the map. There are two different types of PMS: Alpha and Beta. They both have modules in them allowing the player to do certain things to help them survive. Beta PMS have less modules and reduced capabilities of said modules, whereas Alpha PMS have full the same exact modules with full capabilities and even more modules.

### Beta PMS
- Suit Upgrade Module - Allowing upgrading of armor and movement speed.
- Weapon Upgrade Module - Allowing upgrading of weapons.
- Crafting Module - Allowing the crafting of items from other items obtained.
- Quests Module - Where you can accept quests and turn in quest items and collect finished quests' rewards.

### Alpha PMS
- All the same modules from the Beta PMS, but these modules can push and pull items into or from the Stash Module.
- Stash Module - A place to store items for later use.
- Transportation Module - Can be used to transport to any other discovered Alpha PMS.
- Market Module - Allows the selling of items for credits and the purchase of item recipes for credits to unlock crafting recipes in the Crafting Module.
- Memory Module - Allows the saving and loading of game states. As well as exiting the game gracefully.
- Upon using this type of PMS all the enemies will respawn on the map, however you will also heal, and your shield will fully charge if you have one equipped.

These stations are inspired by the Dark Souls franchise bon fires and the Dead Space upgrade stations. They are basically a mix of the two, with the additional crafting module being directly inspired by Subnautica.

*** ***Notice: Both are inside the game with a model but completely lack basically all functionality. Only the Stash Module in the Alpha PMS is working.*** ***

![Project Calamity Alpha & Beta PMS](Docs/README.md/pms.png)
![Project Calamity Alpha PMS](Docs/README.md/alpha-pms-open.png)
![Project Calamity Alpha PMS Stash Module](Docs/README.md/alpha-pms-stash-module.png)

## Inventory System #
The player's inventory contains multiple different slots for equipment.
- Primary Weapon Slot - Can be used to equip long guns only.
- Secondary Weapon Slot - Can be used to equip long guns or pistols.
- Shield Slot - When equipped provides a pool of health that soaks all damage till the pool hits zero. Then all damage will be applied to the player's health instead.
- Armor Slot - When equipped provides damage reduction from enemies' attacks.
- Vest Slot - When equipped it has limited space with a restriction of only magazines, ammunition, grenades, and medical supplies items being allowed inside. There are different types of vests that can be equipped in this slot with pros and cons to each of them.
- Backpack Slot - When equipped it has limited space but with no restrictions on what can be contained inside. There are different types of backpacks that can be equipped in this slot with pros and cons to each of them.
- Credit Wallet - That displays how many credits you have.

Both the vests and backpacks have internal storage inside them, where you can move and rotate items to fit and organize items in the contents of these storage items. Which was inspired by Escape From Tarkov's inventory system and is a direct copy except for some removed and additional slots.

*** ***Notice: All the slots are in the game, except for the vest and backpack slots, however, there are no items other than a rifle and some example items and there is a fixed size storage container just called backpack. So, the whole inventory system provides nothing to the final build of the game. The only interaction with it is you immediately equip your rifle from an Alpha PMS's stash module.*** ***

![Project Calamity Inventory](Docs/README.md/inventory.png)
![Project Calamity Inventory Sorted](Docs/README.md/inventory-sorted.png)

## Health System #
- You have both armors and shields you can wear to stop or reduce damage take to the player.
- You can upgrade your max health in the suit module.
- Health is regenerated from health kits or resting at an Alpha PMS; however, health kits are incredibly rare to find and costly to craft.

*** ***Notice: None of these features are in the final build. There is just 100 health points and is unrecoverable unless you restart the game.*** ***

## Combat #
There is a decent selection of different firearms that you can obtain or craft throughout the progression of the game. Some require ammunition cartridges and magazines. While others use energy and require batteries and capacitors. All these weapons have different fire modes or a mixture of fire modes, those being semi-automatic, fully-automatic, and three-round burst as well as some of the energy weapons shoot out a beam continuously for a short amount of time.

Magazines must be loaded with cartridges, unlike most other games. You can do this by combining the magazine and the cartridges. It will automatically fill up the magazine to full capacity if you have enough cartridges or else it just fills the magazine as much as it can. You can also only reload the firearms using the magazines if you have a magazine in your vest. Same goes for throwing a grenade it must be in a vest to use. Both features are inspired from Escape From Tarkov.

There are different types of AI used by the different enemies in the game with different levels of intellect and tactics. Animal creatures generally are not that smart and will attack head on and will fight to the death. While humanoids are usually smarter and will use cover, flee if they feel they are losing a fight, and if in a group will leverage that against you by working together.

*** ***Notice: In the final build there is only a single firearm, found in the stash with a magazine item, but the magazine is nonfunctional, so the firearm just has infinite reloads for the purposes of the demo. Also the AI is very basic and just goes directly towards a player and attacks when in range. It has no sort of other logic now.*** ***

![Project Calamity Enemies 0](Docs/README.md/enemies-0.png)
![Project Calamity Enemies 1](Docs/README.md/enemies-1.png)

## Conclusion #
While Senior Design is two whole semesters, the first semester is nearly all documentation and planning, then the second is actually working on the project but also more documentation. While we understand the value of documentation we felt the planning and documentation was awkward and mostly a waste of time, because it had general engineering focus. The class did not feel suited for making a video game even though it was an allowed option. This cost us a lot of valuable development time.

We also faced many other problems, such as scope creep, all but one team member had experience using Unity and C#, and underestimating our final semester's workloads and other responsibilities impact on how much we could get done. These aforementioned issues lead to the final build of the game being a shell of what it was supposed to be and short of a minimum viable product. It was missing many of the planned features and the that were implemented were only partially functional.

We certainly learned the importance of keeping a project scope small and incrementally adding to the scope as features are finished, not before. As well as the importance of being truthful to ourselves on our skills and amount of work we can complete. In fact, this is the whole point of Senior Design; to learn about project pitfalls before enter industry.

## Future Work #
Work needed to bring the game to a minimum viable product as it falls short of that.

- Get enemy respawning and health regeneration upon using an Alpha PMS.
- Make it so it is possible to die, currently nothing happens after your health reaches 0 or below.
- Add in recipes for making ammunition and magazines for the only firearm, as well as the recipes for any intermediate parts needed for them.
- Add in the crafting module to the PMS system.
- Add in item drops from killing enemies.
- Add in harvestable items around the map.
- Add in the quest module to the PMS system. We have a quest system it just is not accessible currently.
- Optimize the graphics, using LODs, and other techniques.
- Add ability to save and load different game states to be able save progress.

While that seems like a whole lot just to get to an MVP, we have most of the code already done for it and the code we don't have already should be easy to implement. The hardest issue would be optimization as we have never done it before.

After reaching an MVP, what we would want to add to make it more fun.

- Add more intermediate part recipes for use in other recipes.
- A handful more weapons to find and craft.
- One or more vests, backpacks, armors, and shields to find or craft and to be able to equip them.
- Recycler crafting module upgrade, which would allow you to recycle any applicable crafted or found items for half their raw ingredient items back.
- Add humanoid enemies with firearms, that would drop ammunition, magazines, firearms, and other misc. items.
- Add pickaxe and ore nodes with a finite amount to be harvested. These being a place to get large amounts of a particular ore.
- Increase the size of the playable map.
- Make buildings possible to enter as currently they are not.
- Add caves.
- Add more alien creature enemies.
