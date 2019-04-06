# PlinkWrapper
Adds possibility to use multiple putty .ppk keys with one git host site (like github.com) using one machine. It wraps and coordinates the functionality of TortoiseGitPlink and Pageant.

### Who will benefit?
This program is for Windows git users who use command prompt (powershell prompt) and Tortoisegit interchangeably and has two (or more) github.com accounts with its own ssh keys. The other possible situation is two people use the same computer and that is why different github.com ssh keys.

### The program is not for
- non-Windows user
- non-Tortoisegit and pageant user
- non-putty with Tortoisegit user
- user that uses putty a lot and do not want to abuse it only for git interactions

### The problems solved
- load ssh keys to pageant on first need but not at the system startup or manually.
- load proper ssh keys to pageant depending on what repository is interacted with. Multiple keys for different accounts are supported properly.
- no matter what are used command prompt or Tortoisegit, the behavior is the same.

### Set up
0. git for windows, Tortoisegit and putty are installed.
1. Clone repository and build the solution.
2. Copy built bin files *PlinkWrapper.exe* and *PlinkWrapper.exe.config* into any folder (`c:\Program Files\TortoiseGit\bin\` for eaxample).
3. Set up putty path and Tortoisegit path in *PlinkWrapper.exe.config* according to your system valid paths.
4. Set up putty
- Open putty and go to `Session` category. Add session with the name corresponding to your github account name. If your github account name is `kodicat` than the putty session should be `kodicat`. Click save button. Than go to `Connection -> SSH -> Auth` and select your private key for authentication. After selecting it go back to `Session` category select your session and click save button one more time.
- Do the same steps for your other github accounts
- Do the same steps for `Default Settings` in case you need the default ssh key for interaction.
For example `kodicat` key and `Default Settings` key may be the same as `kodicat` is the main git user on the machine. Cloning the repositories like `git@github.com:Microsoft/dotnet.git` will need the session `Microsoft` in your putty or `Default Settings` session to be set in case you want to use it with ssh key, or it will work without ssh key at all for cloning (as it is a public repository). Pushing to `git@github.com:Microsoft/dotnet.git` will require `Microsoft` session in putty or `Default Settings` to be set if your account `kodicat` is a part of `Microsoft` organization on github.
5. Set `GIT_SSH` environment variable to point to your PlinkWrapper.exe, for example `c:\Program Files\TortoiseGit\bin\PlinkWrapper.exe`
6. Set up Tortoisegit network settings to use PlinkWrapper.exe as well (see [Tortoisegit documentation](https://tortoisegit.org/docs/tortoisegit/tgit-dug-settings.html#tgit-dug-settings-network)). For example to `c:\Program Files\TortoiseGit\bin\PlinkWrapper.exe`

### To consider
This console app was created just for my personal convenience.
It is full of abuses, hacks ad assumptions. It looks the registry, uses explorer.exe to de-elevate from admin rights.
It is not ideal solution that does its work.
