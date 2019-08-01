# ASDGameplay
Another System Down game project's Fire Spread System.

![Fire Spreading](https://github.com/SammakkoFIN/ASDGameplay/blob/master/GIF/ASD%20Grid%20%26%20Fire.GIF)

# What's this project all about
This project contains some of the scripts from our school project Another System Down, which was made in Netherlands University of Applied Sciences, HAN. My role in this game project was a game programmer.

# Grid- & Fire Spread System
Grid System stores a list of nodes, which store data for ex. node's position in world space, gridX, gridY, gridZ and onFire boolean. Grid creates nodes, which are sized by public variable. So node size is 1 in world space and we can spawn things inside the node by calculating center of the node. When the flame is instantiated, it keeps growing bigger and bigger. When it has reached the max size of the flame, it will start to spread the fire by checking neighbor nodes from current node.

The fire can be put out with fire extinguisher.

# About me
In our game project, I mainly focues on adding gameplay features to the game, such as fire spreading, flames, finite state machines, fire exintiguisher, ui toolbar and so many other features. I also worked closely with the Art Team, as it was my job to add their textures, materials and animations to the game.
