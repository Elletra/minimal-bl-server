# Minimal Blockland r2033 Server

This is a minimal Blockland r2033 server. It contains just the minimum for a server to start up and clients to join.

It includes a special `Blockland.exe` that has been patched to make the code work.

To be specific, it edits the [main.cs script](https://gist.github.com/Elletra/01c1d0d0dfddb3beb4f3c98fa99ae275) that's embedded into every executable. Here's what it changes that script to:

```cs
setLogMode(6); enableWinConsole(true); setModPaths("base;config"); exec("base/main.cs");
```

This enables logging, enables the console window, allows scripts to be executed in the `base/` and `config/` folders, and then executes `base/main.cs`.
