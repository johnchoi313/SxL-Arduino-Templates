# HTTP Smart Controller XR

A WebXR AR/VR button controller app that changes colors on interaction and communicates with Unity via HTTP.

## Features

- **WebXR AR Support** - View and interact with 3D cubes in augmented reality
- **WebXR VR Support** - Full VR controller support for immersive experiences
- **Interactive 3D Cube** - Tap/click the cube to cycle through 10 vibrant colors
- **Click Counter** - Track total interactions
- **Real-time Unity Integration** - Sends color changes to Unity HTTP server
- **Unity Connection Status** - Live connection indicator
- **Cross-Platform** - Works on AR-capable mobile devices and VR headsets
- **Responsive UI** - Info panel overlay that works in AR/VR mode

## Requirements

### For AR:
- **Mobile Device** with ARCore (Android) or ARKit (iOS)
- **Browser**: Chrome or Edge on Android, Safari on iOS
- **HTTPS**: WebXR requires secure context (use localhost for development)

### For VR:
- **VR Headset**: Meta Quest, Oculus, HTC Vive, etc.
- **Browser**: Oculus Browser, Firefox Reality, or Chrome with WebXR support

## Setup

1. Install dependencies:
```bash
npm install
```

2. Start the server:
```bash
npm start
```
or
```bash
npm run dev
```

3. Access the app:
- **Desktop**: Open `http://localhost:3000` in your browser
- **Mobile/AR**: Access `http://YOUR_LOCAL_IP:3000` from your mobile device
  - Find your local IP: `ifconfig` (Mac/Linux) or `ipconfig` (Windows)
  - Example: `http://192.168.1.100:3000`

## Unity Integration

This AR controller communicates with the Unity HTTP Server (`ReadWriteCharsHTTPServer.cs`) to synchronize colors across devices.

### How to Connect:

1. **Start Unity:**
   - Open your Unity project with `ReadWriteCharsHTTPServer.cs`
   - Set server port to `4000` (default)
   - Press Play in Unity

2. **Start Node.js Server:**
   - Run `npm start` in this directory
   - Server runs on port `3000`

3. **Enter AR/VR:**
   - On mobile: Tap "Enter AR" button
   - On VR headset: Click "Enter VR" button
   - On desktop: Click the cube directly in the 3D preview

4. **Interact with Cube:**
   - **AR Mode**: Tap on the floating cube
   - **VR Mode**: Point and click with VR controller
   - **Desktop**: Click with mouse
   - Each interaction changes the cube color and sends to Unity

### Communication Details:

- **Node.js Server**: `localhost:3000`
- **Unity Server**: `localhost:4000` (configurable in `server.js`)
- **Protocol**: HTTP GET to `/command?r=<0-255>&g=<0-255>&b=<0-255>`
- **Color Format**: Hex (#3498db) → RGB (52, 152, 219)

### Network Setup for Multiple Devices:

If you want your mobile AR device to communicate with Unity on your computer:

1. Ensure all devices are on the same WiFi network
2. Update `UNITY_HOST` in `server.js` to your computer's local IP
3. Make sure firewall allows connections on ports 3000 and 4000

## How It Works

### WebXR AR Mode:
1. User taps "Enter AR" button
2. Camera feed appears with AR cube overlay
3. User taps the 3D cube floating in their space
4. Cube changes color (cycles through 10 colors)
5. Color is sent to Node.js server
6. Server forwards RGB values to Unity
7. Unity cube updates to match AR cube color

### WebXR VR Mode:
1. User enters VR with headset
2. Cube appears in VR space
3. User points VR controller and triggers select
4. Same color change and Unity sync as AR mode

### Desktop Mode:
1. 3D preview shows rotating cube
2. Click cube with mouse to change color
3. Works without AR/VR hardware

## Technologies Used

- **Three.js** (r160) - 3D graphics and WebXR
- **WebXR Device API** - AR/VR capabilities
- **Node.js & Express** - Backend server
- **HTML5/CSS3/JavaScript** - Frontend
- **Unity C# HTTP Server** - Cross-platform communication

## Browser Compatibility

| Platform | Browser | AR Support | VR Support |
|----------|---------|------------|------------|
| Android  | Chrome  | ✅ ARCore  | ✅         |
| iOS      | Safari  | ✅ ARKit   | ❌         |
| Desktop  | Chrome  | ❌         | ✅ (with headset) |
| Meta Quest | Oculus Browser | ✅ | ✅      |

## Troubleshooting

### AR not working:
- Ensure you're using HTTPS or localhost
- Check if browser supports WebXR AR
- Verify camera permissions are granted
- Make sure device has ARCore/ARKit

### Unity not connecting:
- Verify Unity HTTP server is running on port 4000
- Check Unity console for server start message
- Ensure firewall isn't blocking connections
- Try `http://localhost:4000` in browser to test Unity server

### Can't access from mobile:
- Use your computer's local IP, not localhost
- Ensure mobile and computer are on same WiFi
- Check firewall settings on computer

## API Endpoints

- `GET /api/state` - Get current state (color, click count, Unity status)
- `POST /api/click` - Update state on interaction and sync with Unity
- `GET /api/unity/status` - Check Unity server connection

## License

MIT
