# MusicShellApi

## Overview
The `MusicShellApi` is a RESTful API built using ASP.NET Core. It serves as the backend for a Unix-style shell music player, which allows users to play, pause, stop, and navigate through songs. The music source can be a local folder on the user's PC, with plans to integrate with streaming services like Spotify and Pandora in the future.

## Features
- Play, pause, stop, and navigate through songs.
- Retrieve song information.
- Modular architecture for easy extension and maintenance.
- Future-proof design to support streaming services integration.

## Folder Structure
```plaintext
MusicShellApi/
│
├── Controllers/
│   └── MusicController.cs
│
├── Mediators/
│   ├── Interfaces/
│   │   └── IMusicMediator.cs
│   └── Implementation/
│       └── MusicMediator.cs
│
├── Models/
│   └── SongInfo.cs
│
├── Services/
│   ├── Interfaces/
│   │   ├── IMusicProvider.cs
│   │   ├── IMusicProviderFactory.cs
│   │   ├── IMusicService.cs
│   │   └── IFileSystem.cs
│   │
│   ├── Providers/
│   │   └── LocalMusicProvider.cs
│   │
│   ├── Factories/
│   │   ├── PandoraMusicProviderFactory.cs
│   │   └── SpotifyMusicProviderFactory.cs
│   │
│   ├── Config/
│   │   ├── PandoraConfig.cs
│   │   └── SpotifyConfig.cs
│   │
│   ├── Implementation/
│   │   └── MusicService.cs
│   └── ...
│
├── Program.cs
├── appsettings.json
└── appsettings.Development.json
```

## Setup

1. **Clone the repository**:
   ```bash
   git clone https://github.com/yourusername/shell-music-player.git
   cd shell-music-player
   ```

2. **Navigate to the `MusicShellApi` directory**:
   ```bash
   cd MusicShellApi
   ```

3. **Restore dependencies**:
   ```bash
   dotnet restore
   ```

4. **Build the project**:
   ```bash
   dotnet build
   ```

5. **Run the project**:
   ```bash
   dotnet run
   ```

### Configuration
The API uses configuration files to manage settings.

- `appsettings.json`: General configuration settings.
- `appsettings.Development.json`: Development-specific configuration settings.

You can update these files with your settings as needed.

## Usage

### API Endpoints
- **GET /api/music/play**: Plays the current song.
- **GET /api/music/pause**: Pauses the current song.
- **GET /api/music/stop**: Stops the music.
- **GET /api/music/next**: Plays the next song.
- **GET /api/music/previous**: Plays the previous song.
- **GET /api/music/list**: Lists all available songs.
- **GET /api/music/song/{index}**: Gets information about a specific song by index.

### Example Requests
1. **Play a Song**:
   ```http
   GET /api/music/play
   ```

2. **Pause a Song**:
   ```http
   GET /api/music/pause
   ```

3. **Get Song Information**:
   ```http
   GET /api/music/song/0
   ```

## Design Patterns
### 1. Mediator Pattern
**Rationale**: Centralizes communication between different components to reduce direct dependencies and promote loose coupling.

### 2. Factory Pattern
**Rationale**: Encapsulates the creation of music providers, allowing for easy integration of new providers without modifying existing code.

### 3. Dependency Injection
**Rationale**: Facilitates the injection of dependencies, making the code more modular, testable, and easier to manage.

### 4. Singleton Pattern
**Rationale**: Ensures a single instance of services like `IMusicService` and `IMusicMediator` throughout the application's lifecycle.


## Contributing
If you would like to contribute to this project, please fork the repository and submit a pull request. We welcome all contributions!

## License
This project is licensed under the MIT License.

## Contact
For questions or support, please reach out to the project maintainer.
