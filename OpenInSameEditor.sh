#!/bin/bash
# Unity wrapper: open C# files in the SAME Visual Studio / Cursor / VS Code instance on macOS.
# Without this, Unity often opens a new instance each time.
#
# Setup in Unity: Edit > Preferences (or Unity > Settings) > External Tools
#   External Script Editor: point to this script's full path
#   External Script Editor Args: "$(File)" $(Line)

FILE="$1"
LINE="${2:-1}"

# --- Choose your editor (uncomment ONE) ---

# Visual Studio for Mac (reuses same instance via open -a, no -n)
open -a "Visual Studio" "$FILE"

# Cursor (reuses window; requires "cursor" in PATH from Cursor shell command)
# cursor -r -g "$FILE:$LINE"

# Visual Studio Code (reuses window)
# code -r -g "$FILE:$LINE"
