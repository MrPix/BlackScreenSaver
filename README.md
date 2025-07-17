# BlackScreenSaver

A lightweight Windows application that provides a simple black screen saver with system tray integration and global hotkey support.

## ?? Features

- **??? Black Screen Display**: Instantly displays a full-screen black overlay that hides all desktop content
- **?? Global Hotkey**: Quick activation using **Ctrl+F12** from anywhere in Windows
- **?? System Tray Integration**: Runs silently in the background with easy access through system tray icon
- **?? Auto-Startup Support**: Optional automatic startup with Windows
- **??? Multiple Exit Methods**: Exit using Escape key, Alt+F4, or mouse click
- **??? Cursor Management**: Automatically hides cursor during black screen and restores it on exit

## ?? System Requirements

- **Operating System**: Windows 10/11
- **Framework**: .NET 8 Runtime
- **Architecture**: x64

## ?? Installation

1. **Download**: Get the latest release from the releases page
2. **Extract**: Unzip the application to your preferred directory
3. **Run**: Execute `BlackScreenSaver.App.exe`
4. **Setup**: The application will appear in your system tray

## ?? Usage

### Quick Start
1. **Launch the application** - Look for the black square icon in your system tray
2. **Activate black screen** - Press **Ctrl+F12** or double-click the tray icon
3. **Exit black screen** - Press **Escape**, **Alt+F4**, or click anywhere on the screen

### System Tray Menu

Right-click the tray icon to access:

| Menu Item | Description |
|-----------|-------------|
| **Run Black Screen** | Manually activate the black screen |
| **Add to Startup** | Enable/disable automatic startup with Windows |
| **Exit** | Close the application completely |

### Hotkey

- **Ctrl+F12**: Activates the black screen from anywhere in Windows (global hotkey)

## ?? Configuration

### Auto-Startup
- Use the "Add to Startup" menu option to automatically start with Windows
- The menu item dynamically shows "Add to Startup" or "Remove from Startup" based on current status
- Settings are stored in Windows Registry under `HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Run`

## ??? Technical Details

### Architecture
- **Framework**: .NET 8 with Windows Forms
- **Language**: C# 12.0
- **Pattern**: Single-instance tray application

### Key Components
- **TrayApplication**: Main application class managing system tray and hotkeys
- **BlackForm**: Full-screen black overlay form
- **Program**: Application entry point and hotkey management

### Hotkey Implementation
- Uses Windows API (`user32.dll`) for global hotkey registration
- Handles `WM_HOTKEY` messages for hotkey detection
- Automatic cleanup on application exit

## ??? Building from Source

### Prerequisites
- Visual Studio 2022 or later
- .NET 8 SDK

### Build Steps# Clone the repository
git clone [repository-url]

# Navigate to project directory
cd BlackScreenSaver

# Build the application
dotnet build BlackScreenSaver.App/BlackScreenSaver.App.csproj --configuration Release

# Run the application
dotnet run --project BlackScreenSaver.App/BlackScreenSaver.App.csproj
## ?? Use Cases

- **Presentations**: Quickly hide desktop content during breaks or transitions
- **Privacy**: Instantly obscure screen content when needed
- **Screen Protection**: Prevent screen burn-in during extended idle periods
- **Focus Mode**: Eliminate visual distractions for concentration
- **Meeting Privacy**: Hide sensitive information during video calls

## ? FAQ

**Q: How do I exit the black screen?**
A: Press Escape, Alt+F4, or click anywhere on the screen.

**Q: Can I change the hotkey?**
A: Currently, the hotkey is fixed to Ctrl+F12. Future versions may include customization.

**Q: Will this work on multiple monitors?**
A: Yes, the black screen covers all connected monitors.

**Q: Does this affect system performance?**
A: No, the application has minimal resource usage and only activates when needed.

## ?? Troubleshooting

### Hotkey Not Working
- Ensure no other application is using Ctrl+F12
- Try running as administrator if hotkey registration fails
- Restart the application

### Tray Icon Missing
- Check if the application is running in Task Manager
- Verify system tray settings in Windows
- Restart the application

### Startup Issues
- Verify .NET 8 Runtime is installed
- Check Windows event logs for error details
- Ensure application has necessary permissions

## ?? License

This project is open source. See the LICENSE file for details.

## ?? Version History

### Current Version
- System tray integration
- Global hotkey support (Ctrl+F12)
- Auto-startup functionality
- Improved cursor management
- Enhanced error handling

## ?? Contributing

Contributions are welcome! Please feel free to submit issues, feature requests, or pull requests.

---

**Made with ?? for Windows users who need quick screen privacy**