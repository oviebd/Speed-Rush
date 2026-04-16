# Open C# Files in the Same Editor Instance (macOS)

Unity on macOS can open a **new** Visual Studio (or Cursor/VS Code) window every time you double‑click a script. Use one of the options below so files open in the **same** instance.

---

## Option 1: Use the wrapper script (recommended)

1. **Choose your editor**  
   Open `OpenInSameEditor.sh` and uncomment the line for your editor (Visual Studio, Cursor, or VS Code). Comment out or remove the others.

2. **In Unity**
   - **Unity > Settings** (or **Edit > Preferences** on Windows).
   - Go to **External Tools**.
   - **External Script Editor:** Click **Browse** and select **`OpenInSameEditor.sh`** (use the full path, e.g.  
     `/Users/habibur/Documents/Projects/Unity/SpeedRush/OpenInSameEditor.sh`).
   - **External Script Editor Args:**  
     `"$(File)" $(Line)`

3. **Regenerate project files (optional)**  
   In the same **External Tools** section, click **Regenerate project files** so the solution is up to date.

After this, opening a C# script from Unity should use the same editor window.

---

## Option 2: Try External Script Editor Args only (no script)

If you use **Cursor** or **VS Code** and the `cursor` or `code` command is in your PATH:

1. **Unity > Settings > External Tools**
2. **External Script Editor:** Choose **Cursor** or **Visual Studio Code** from the list (or browse to the app).
3. **External Script Editor Args:**  
   - Cursor: `-r -g "$(File):$(Line)"`  
   - VS Code: `-r -g "$(File):$(Line)"`  

`-r` / `--reuse-window` tells the editor to use an existing window.

If Unity still opens new windows, use **Option 1** with the script.

---

## If you use Visual Studio for Mac

Unity has a known issue on macOS where Visual Studio can open multiple instances ([Unity Issue VS-68](https://issuetracker.unity3d.com/issues/macos-visual-studio-opens-multiple-instances-when-selecting-multiple-scripts-and-opening-them)); it’s marked “Won’t Fix.”

Using **Option 1** and setting the script to `open -a "Visual Studio" "$FILE"` avoids `-n` (new instance) and usually makes macOS reuse the same Visual Studio window.

---

## Quick reference: script editor names

| Editor              | App name for `open -a` |
|---------------------|------------------------|
| Visual Studio (Mac) | `Visual Studio`        |
| Cursor              | `Cursor`               |
| VS Code             | `Visual Studio Code`   |
