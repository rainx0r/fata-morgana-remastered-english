# fata-morgana-remastered-english
English patch for The House in Fata Morgana Remastered. Based on the PS4 version of "The House in Fata Morgana Dreams of the Revenants Edition", re-enabling disabled english files that already exist in the game, as well a tiny bit of original work. This is still just merely an English patch for The House in Fata Morgana Remastered only with the help of assets from that PS4 game, it does not include requiem, reincarnation, or the short stories.

Everything in the game including scripts, images and menus is translated based on the official translation of the game by MangaGamer. You can think of this patch as a port of that translation to the Remastered version if the game if you will.

The game can be obtained [here](https://www.animategames.jp/home/detail/30082), they seem to accept western cards (or at least they did mine) so long as you have a Japanese IP (you can use a VPN). Please buy it to support Novectacle. Likewise please buy the official HD localized version (PS4 or PS Vita).

## Download & Install
1. Download the latest [Release](https://github.com/rainx0r/fata-morgana-remastered-english/releases).
2. Unzip its contents into the root of the install directory for The House in Fata Morgana Remastered and accept to overwrite existing files when asked.

The game should now be fully in English. You can change the language between Japanese and English in the Config from the main menu.

## Build

This is only if you're curious as to how you could build the patch yourself if you have the PS4 files.

0. You'll need a dump of the PS4 version of "The House in Fata Morgana Dreams of the Revenants Edition". You'll also need to unpack it with a tool of your choice then unpack the `bgimage` AssetBundle. I used [AssetRipper](https://github.com/AssetRipper/AssetRipper). You can put all the PNGs in `Fata Morgana Remastered English Patch/Assets/fata_unity/AssetBundleResources/data/bgimage_en` or just the ones referenced in [bgimage.manifest](https://github.com/rainx0r/fata-morgana-remastered-english/blob/main/Fata%20Morgana%20Remastered%20English%20Patch/Assets/Editor/bgimage.manifest). You'll also need to copy copy `MOV_intro.mp4` and `MOV_staffroll_main.mp4` from `Image0\Media\StreamingAssets\movie_fullHD` into `build/FataMorgana_Data/StreamingAssets/movie_fullHD`. Finally, you need to extract all the assets (using AssetRipper, Open Folder and Extract All Files) and copy the lyric files from `Assets/Resources/data/lyrics/en` to `Fata Morgana Remastered English Patch/Assets/fata_unity/AssetBundleResources/data/lyrics_en/`.
1. Grab Unity 2017.4.33f1
2. Open `Fata Morgana Remastered English Patch` as a Unity project.
3. Build the AssetBundles with `Project > Build AssetBundles`.
4. The `build` directory should now have the full patch build!

Code changes to `Assembly-CSharp.dll` were rather minimal and were done with [dnSpyEx](https://github.com/dnSpyEx/dnSpy).

## Credits

- **Translation** (of a single image and 2 menu items...): @RainAnnen
- **Image Editing**: @vimiani
- **Hacking**: @RainAnnen, @TheZombieKiller
- **Special thanks**: @alyinghood, @MrComputerRevo

## Disclaimer

I do not own any of the assets that appear anywhere.
