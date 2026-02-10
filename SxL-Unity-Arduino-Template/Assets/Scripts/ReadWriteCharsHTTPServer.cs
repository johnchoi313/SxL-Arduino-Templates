using System.Net;
using UnityEngine;

/// <summary>
/// Unity as HTTP Server - Arduino as HTTP Client
/// Arduino polls Unity for 'a'/'b' data, sends 'c'/'d' commands
/// </summary>
public class ReadWriteCharsHTTPServer : MonoBehaviour
{
    [Header("Server Settings")]
    public int serverPort = 8080;
    
    [Header("Unity Objects")]
    public GameObject cube;
    
    [Header("LED Control")]
    private int ledPin = 4;
    
    private HTTPServer server;
    private char currentChar = 'a';
    private int charsSent = 0;
    private int sendTimer = 0;
    private int sendTime = 100;  // Switch char every 1 second
    
    void Start() {
        server = new HTTPServer(serverPort);
        
        // Register routes
        server.AddRoute("/", HandleRoot);
        server.AddRoute("/data", HandleData);
        server.AddRoute("/command", HandleCommand);
        
        server.Start();
        
        Debug.Log($"Unity HTTP Server started on port {serverPort}");
        Debug.Log("Configure Arduino to connect to this Unity IP");
    }
    
    void Update() {
        // Alternate between 'a' and 'b'
        if (sendTimer > 0) {
            sendTimer -= 1;
        } else {
            sendTimer = sendTime;
            charsSent += 1;
            currentChar = (charsSent % 2 == 0) ? 'a' : 'b';
        }
    }
    
    void OnDestroy() {
        server?.Stop();
    }
    
    // Root endpoint
    string HandleRoot(HttpListenerContext context) {
        return "<h1>Unity HTTP Server</h1>" +
               "<p>GET /data - Get current character (a or b)</p>" +
               "<p>GET /command?cmd=c - Change cube to RED</p>" +
               "<p>GET /command?cmd=d - Change cube to BLUE</p>";
    }
    
    // Data endpoint - Arduino polls this
    string HandleData(HttpListenerContext context) {
        Debug.Log($"[Server] Sending char: {currentChar}");
        return currentChar.ToString();
    }
    
    // Command endpoint - Arduino sends commands
    string HandleCommand(HttpListenerContext context) {
        string cmd = context.Request.QueryString["cmd"];
        
        if (cmd == "c") {
            // Change cube to red
            if (cube != null) {
                cube.GetComponent<Renderer>().material.color = Color.red;
            }
            Debug.Log("[Server] Command: c - Cube set to RED");
            return "Cube set to RED";
        } 
        else if (cmd == "d") {
            // Change cube to blue
            if (cube != null) {
                cube.GetComponent<Renderer>().material.color = Color.blue;
            }
            Debug.Log("[Server] Command: d - Cube set to BLUE");
            return "Cube set to BLUE";
        }
        else {
            return "Invalid command";
        }
    }
}
