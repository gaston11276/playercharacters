[![License](https://img.shields.io/github/license/gaston11276/playercharacters.svg)](LICENSE)
[![Build Status](https://img.shields.io/appveyor/ci/gaston11276/playercharacters/master.svg)](https://ci.appveyor.com/project/gaston11276/playercharacters)
[![Release Version](https://img.shields.io/github/release/gaston11276/playercharacters/all.svg)](https://github.com/gaston11276/playercharacters/releases)

NFive Plugin

## Installation
Install the plugin into your server from the [NFive Hub](https://hub.nfive.io/gaston11276/playercharacters): `nfpm install gaston11276/playercharacters`
# playercharacters
This plugin is in an early state. Igicore's plugin Characters has been used as a foundation.

F1 - Character creator.
F2 - Appearance menu.
F5 - Spawn location.

- Playercharacters includes character creation supporting code first database as implemented by NFive/IgiCore.
- The migration file assumes that your database already has tables for Users and Sessions as per default.
- Inventory (also by Igicore) is included but not yet used in ui implementation. Inventory might be moved to a separate plugin.
- Playercharacters includes a spawn-location menu that most likely will be moved to a separate plugin.
- More values will be added to th configuration file.
- Date of Birth is not implemented by ui at this point.
- Menues for Model and Animations have been temporaly disabled.
- Most tattoos does not belong to its respective zone but is found in the 'Unknown' zone, temporarly applied at the face tattoo index.
- Props applying/removing is not yet handled properly, thus it might be tricky to have no prop at a certain index (like removing the watch).
- Tattoo camera is not fine-tuned but can be rotated.
- Male/Female selectiono will be improved.
- A button press indication will be added.
- Due to some maximum setting for number of text draw calls made sometime the menues seems to be missing text. This can be seen when opening the 'HeadOverlays' menu. Some solution to this will be found I'm sure.
- Input events from the nui/overlay interface is sent to the client service by nfive 'send' command. This is probably totally off the intended way of doing things. I will look into it at some time. Meanwhile, please suggest a better solution.

Please let me know what you think. I am grateful for any suggestion of improvement or just ideas of any kind.


