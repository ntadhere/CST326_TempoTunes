#Tempo Tunes Music Management Platform
## Repository Structure
```
CST-326TempoTunes/
├── Controllers/                # Contains controller classes
│   └── HomeController.cs       # Handles homepage and related views
├── Helpers/                    # Custom HTML helpers
│   └── HtmlHelpers.cs          # Defines helper methods for views
├── Models/                     # Data models for views and logic
│   ├── ErrorViewModel.cs       # Model for error handling
│   ├── HomeViewModel.cs        # Model for home page data
│   ├── PlaylistModel.cs        # Model for playlist data
│   └── TrackModel.cs           # Model for track data
├── Properties/                 # Project properties (e.g., configuration)
├── Services/                   # Business logic
│   └── Business/               # Handles playlist business logic
│       └── PlaylistCollection.cs # Manages playlist collection
├── DataAccess/                 # Data access layer
│   └── PlaylistDAO.cs          # Interacts with the playlist database
├── Views/                      # Views for rendering pages
│   ├── Home/                   # Home page views
│   │   ├── Index.cshtml        # Home page
│   │   ├── OnePlaylist.cshtml  # Displays a single playlist
│   │   └── Playlist.cshtml     # Displays playlist list
│   └── Shared/                 # Shared view components
│       ├── Error.cshtml        # Common error page
│       ├── _Layout.cshtml      # Layout template for pages
│       ├── _Layout.cshtml.css  # Layout-specific CSS
│       ├── _ValidationScriptsPartial.cshtml # Validation scripts
│       ├── _ViewImports.cshtml # Shared imports for views
│       └── _ViewStart.cshtml   # Common view start settings
├── wwwroot/                    # Static files (CSS, JS, images)
├── .DS_Store                   # MacOS system file (ignore in version control)
├── CST-326TempoTunes.csproj    # Project file with dependencies
├── Program.cs                  # Application entry point
├── appsettings.Development.json # Development-specific settings
├── appsettings.json            # General app configuration
├── global.json                 # .NET SDK version
├── .gitattributes              # Git file handling rules
├── .gitignore                  # Specifies files Git should ignore
└── CST-326TempoTunes.sln       # Visual Studio solution file

```
