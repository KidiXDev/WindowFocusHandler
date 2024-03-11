
# Window Focus Handler

A simple tools to check if process window is on focus or not

# How it works?

The `user32` library is used to call functions like GetForegroundWindow, GetWindowThreadProcessId, and GetGUIThreadInfo to interact with window and GUI related operations. These functions are used to retrieve information about the currently focused window and its associated process.

The `IsProcessWindowFocused` method takes a Process object as input and checks if its associated window is currently focused by comparing its process ID with the ID of the foreground window. If they match, it retrieves GUI thread information and checks if the active window handle matches the foreground window handle, returning true if they do.

This class essentially provides a way to determine if a specific process window is currently focused or not by utilizing functions and structures from the `user32.dll` library through platform invocation (P/Invoke) in C#.
