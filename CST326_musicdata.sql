-- Drop tables if they exist (drop Track first because of the foreign key)
IF OBJECT_ID('dbo.Track', 'U') IS NOT NULL
    DROP TABLE dbo.Track;
IF OBJECT_ID('dbo.Playlist', 'U') IS NOT NULL
    DROP TABLE dbo.Playlist;

-- Create Playlist table
CREATE TABLE Playlist (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,
    ArtistName NVARCHAR(255),
    ImageUrl NVARCHAR(500)
);

-- Create Track table with a foreign key to Playlist
CREATE TABLE Track (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,
    Artist NVARCHAR(255),
    Duration NVARCHAR(50),
    PlaylistId INT,
    FOREIGN KEY (PlaylistId) REFERENCES Playlist(Id)
);

-------------------------
-- Insert data into Playlist
-------------------------
INSERT INTO Playlist (Name, ArtistName, ImageUrl) VALUES ('Morning Motivation', 'Various Artists', '/lib/img/playlist1.jpg');
INSERT INTO Playlist (Name, ArtistName, ImageUrl) VALUES ('Chill Vibes', 'Various Artists', '/lib/img/playlist2.jpg');
INSERT INTO Playlist (Name, ArtistName, ImageUrl) VALUES ('Workout Mix', 'Various Artists', '/lib/img/playlist3.jpg');
INSERT INTO Playlist (Name, ArtistName, ImageUrl) VALUES ('Classic Hits', 'Various Artists', '/lib/img/playlist4.jpg');
INSERT INTO Playlist (Name, ArtistName, ImageUrl) VALUES ('Jazz Essentials', 'Various Artists', '/lib/img/playlist5.jpg');
INSERT INTO Playlist (Name, ArtistName, ImageUrl) VALUES ('Rock Anthems', 'Various Artists', '/lib/img/playlist6.jpg');
INSERT INTO Playlist (Name, ArtistName, ImageUrl) VALUES ('Electronic Dance', 'Various Artists', '/lib/img/playlist7.jpg');
INSERT INTO Playlist (Name, ArtistName, ImageUrl) VALUES ('Hip Hop Beats', 'Various Artists', '/lib/img/playlist8.jpg');
INSERT INTO Playlist (Name, ArtistName, ImageUrl) VALUES ('Reggae Rhythms', 'Various Artists', '/lib/img/playlist9.jpg');
INSERT INTO Playlist (Name, ArtistName, ImageUrl) VALUES ('Acoustic Sessions', 'Various Artists', '/lib/img/playlist10.jpg');
INSERT INTO Playlist (Name, ArtistName, ImageUrl) VALUES ('Party Time', 'Various Artists', '/lib/img/playlist11.jpg');
INSERT INTO Playlist (Name, ArtistName, ImageUrl) VALUES ('Instrumental Bliss', 'Various Artists', '/lib/img/playlist12.jpg');
INSERT INTO Playlist (Name, ArtistName, ImageUrl) VALUES ('Pop Favorites', 'Various Artists', '/lib/img/playlist13.jpg');
INSERT INTO Playlist (Name, ArtistName, ImageUrl) VALUES ('Relaxing Piano', 'Various Artists', '/lib/img/playlist14.jpg');
INSERT INTO Playlist (Name, ArtistName, ImageUrl) VALUES ('Country Roads', 'Various Artists', '/lib/img/playlist15.jpg');
INSERT INTO Playlist (Name, ArtistName, ImageUrl) VALUES ('Latin Grooves', 'Various Artists', '/lib/img/playlist16.jpg');
INSERT INTO Playlist (Name, ArtistName, ImageUrl) VALUES ('R&B Smooth', 'Various Artists', '/lib/img/playlist17.jpg');
INSERT INTO Playlist (Name, ArtistName, ImageUrl) VALUES ('Indie Discoveries', 'Various Artists', '/lib/img/playlist18.jpg');
INSERT INTO Playlist (Name, ArtistName, ImageUrl) VALUES ('Soulful Sounds', 'Various Artists', '/lib/img/playlist19.jpg');
INSERT INTO Playlist (Name, ArtistName, ImageUrl) VALUES ('Evening Reflections', 'Various Artists', '/lib/img/playlist20.jpg');

-------------------------
-- Insert data into Track
-------------------------
-- Playlist 1: Morning Motivation (PlaylistId = 1)
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 1 for Morning Motivation', 'Artist 1 - Track 1', '3:00', 1);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 2 for Morning Motivation', 'Artist 1 - Track 2', '3:30', 1);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 3 for Morning Motivation', 'Artist 1 - Track 3', '4:00', 1);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 4 for Morning Motivation', 'Artist 1 - Track 4', '2:30', 1);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 5 for Morning Motivation', 'Artist 1 - Track 5', '3:45', 1);

-- Playlist 2: Chill Vibes (PlaylistId = 2)
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 1 for Chill Vibes', 'Artist 2 - Track 1', '3:00', 2);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 2 for Chill Vibes', 'Artist 2 - Track 2', '3:30', 2);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 3 for Chill Vibes', 'Artist 2 - Track 3', '4:00', 2);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 4 for Chill Vibes', 'Artist 2 - Track 4', '2:30', 2);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 5 for Chill Vibes', 'Artist 2 - Track 5', '3:45', 2);

-- Playlist 3: Workout Mix (PlaylistId = 3)
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 1 for Workout Mix', 'Artist 3 - Track 1', '3:00', 3);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 2 for Workout Mix', 'Artist 3 - Track 2', '3:30', 3);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 3 for Workout Mix', 'Artist 3 - Track 3', '4:00', 3);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 4 for Workout Mix', 'Artist 3 - Track 4', '2:30', 3);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 5 for Workout Mix', 'Artist 3 - Track 5', '3:45', 3);

-- Playlist 4: Classic Hits (PlaylistId = 4)
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 1 for Classic Hits', 'Artist 4 - Track 1', '3:00', 4);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 2 for Classic Hits', 'Artist 4 - Track 2', '3:30', 4);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 3 for Classic Hits', 'Artist 4 - Track 3', '4:00', 4);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 4 for Classic Hits', 'Artist 4 - Track 4', '2:30', 4);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 5 for Classic Hits', 'Artist 4 - Track 5', '3:45', 4);

-- Playlist 5: Jazz Essentials (PlaylistId = 5)
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 1 for Jazz Essentials', 'Artist 5 - Track 1', '3:00', 5);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 2 for Jazz Essentials', 'Artist 5 - Track 2', '3:30', 5);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 3 for Jazz Essentials', 'Artist 5 - Track 3', '4:00', 5);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 4 for Jazz Essentials', 'Artist 5 - Track 4', '2:30', 5);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 5 for Jazz Essentials', 'Artist 5 - Track 5', '3:45', 5);

-- Playlist 6: Rock Anthems (PlaylistId = 6)
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 1 for Rock Anthems', 'Artist 6 - Track 1', '3:00', 6);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 2 for Rock Anthems', 'Artist 6 - Track 2', '3:30', 6);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 3 for Rock Anthems', 'Artist 6 - Track 3', '4:00', 6);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 4 for Rock Anthems', 'Artist 6 - Track 4', '2:30', 6);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 5 for Rock Anthems', 'Artist 6 - Track 5', '3:45', 6);

-- Playlist 7: Electronic Dance (PlaylistId = 7)
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 1 for Electronic Dance', 'Artist 7 - Track 1', '3:00', 7);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 2 for Electronic Dance', 'Artist 7 - Track 2', '3:30', 7);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 3 for Electronic Dance', 'Artist 7 - Track 3', '4:00', 7);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 4 for Electronic Dance', 'Artist 7 - Track 4', '2:30', 7);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 5 for Electronic Dance', 'Artist 7 - Track 5', '3:45', 7);

-- Playlist 8: Hip Hop Beats (PlaylistId = 8)
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 1 for Hip Hop Beats', 'Artist 8 - Track 1', '3:00', 8);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 2 for Hip Hop Beats', 'Artist 8 - Track 2', '3:30', 8);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 3 for Hip Hop Beats', 'Artist 8 - Track 3', '4:00', 8);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 4 for Hip Hop Beats', 'Artist 8 - Track 4', '2:30', 8);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 5 for Hip Hop Beats', 'Artist 8 - Track 5', '3:45', 8);

-- Playlist 9: Reggae Rhythms (PlaylistId = 9)
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 1 for Reggae Rhythms', 'Artist 9 - Track 1', '3:00', 9);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 2 for Reggae Rhythms', 'Artist 9 - Track 2', '3:30', 9);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 3 for Reggae Rhythms', 'Artist 9 - Track 3', '4:00', 9);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 4 for Reggae Rhythms', 'Artist 9 - Track 4', '2:30', 9);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 5 for Reggae Rhythms', 'Artist 9 - Track 5', '3:45', 9);

-- Playlist 10: Acoustic Sessions (PlaylistId = 10)
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 1 for Acoustic Sessions', 'Artist 10 - Track 1', '3:00', 10);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 2 for Acoustic Sessions', 'Artist 10 - Track 2', '3:30', 10);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 3 for Acoustic Sessions', 'Artist 10 - Track 3', '4:00', 10);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 4 for Acoustic Sessions', 'Artist 10 - Track 4', '2:30', 10);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 5 for Acoustic Sessions', 'Artist 10 - Track 5', '3:45', 10);

-- Playlist 11: Party Time (PlaylistId = 11)
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 1 for Party Time', 'Artist 11 - Track 1', '3:00', 11);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 2 for Party Time', 'Artist 11 - Track 2', '3:30', 11);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 3 for Party Time', 'Artist 11 - Track 3', '4:00', 11);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 4 for Party Time', 'Artist 11 - Track 4', '2:30', 11);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 5 for Party Time', 'Artist 11 - Track 5', '3:45', 11);

-- Playlist 12: Instrumental Bliss (PlaylistId = 12)
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 1 for Instrumental Bliss', 'Artist 12 - Track 1', '3:00', 12);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 2 for Instrumental Bliss', 'Artist 12 - Track 2', '3:30', 12);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 3 for Instrumental Bliss', 'Artist 12 - Track 3', '4:00', 12);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 4 for Instrumental Bliss', 'Artist 12 - Track 4', '2:30', 12);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 5 for Instrumental Bliss', 'Artist 12 - Track 5', '3:45', 12);

-- Playlist 13: Pop Favorites (PlaylistId = 13)
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 1 for Pop Favorites', 'Artist 13 - Track 1', '3:00', 13);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 2 for Pop Favorites', 'Artist 13 - Track 2', '3:30', 13);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 3 for Pop Favorites', 'Artist 13 - Track 3', '4:00', 13);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 4 for Pop Favorites', 'Artist 13 - Track 4', '2:30', 13);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 5 for Pop Favorites', 'Artist 13 - Track 5', '3:45', 13);

-- Playlist 14: Relaxing Piano (PlaylistId = 14)
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 1 for Relaxing Piano', 'Artist 14 - Track 1', '3:00', 14);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 2 for Relaxing Piano', 'Artist 14 - Track 2', '3:30', 14);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 3 for Relaxing Piano', 'Artist 14 - Track 3', '4:00', 14);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 4 for Relaxing Piano', 'Artist 14 - Track 4', '2:30', 14);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 5 for Relaxing Piano', 'Artist 14 - Track 5', '3:45', 14);

-- Playlist 15: Country Roads (PlaylistId = 15)
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 1 for Country Roads', 'Artist 15 - Track 1', '3:00', 15);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 2 for Country Roads', 'Artist 15 - Track 2', '3:30', 15);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 3 for Country Roads', 'Artist 15 - Track 3', '4:00', 15);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 4 for Country Roads', 'Artist 15 - Track 4', '2:30', 15);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 5 for Country Roads', 'Artist 15 - Track 5', '3:45', 15);

-- Playlist 16: Latin Grooves (PlaylistId = 16)
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 1 for Latin Grooves', 'Artist 16 - Track 1', '3:00', 16);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 2 for Latin Grooves', 'Artist 16 - Track 2', '3:30', 16);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 3 for Latin Grooves', 'Artist 16 - Track 3', '4:00', 16);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 4 for Latin Grooves', 'Artist 16 - Track 4', '2:30', 16);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 5 for Latin Grooves', 'Artist 16 - Track 5', '3:45', 16);

-- Playlist 17: R&B Smooth (PlaylistId = 17)
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 1 for R&B Smooth', 'Artist 17 - Track 1', '3:00', 17);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 2 for R&B Smooth', 'Artist 17 - Track 2', '3:30', 17);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 3 for R&B Smooth', 'Artist 17 - Track 3', '4:00', 17);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 4 for R&B Smooth', 'Artist 17 - Track 4', '2:30', 17);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 5 for R&B Smooth', 'Artist 17 - Track 5', '3:45', 17);

-- Playlist 18: Indie Discoveries (PlaylistId = 18)
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 1 for Indie Discoveries', 'Artist 18 - Track 1', '3:00', 18);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 2 for Indie Discoveries', 'Artist 18 - Track 2', '3:30', 18);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 3 for Indie Discoveries', 'Artist 18 - Track 3', '4:00', 18);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 4 for Indie Discoveries', 'Artist 18 - Track 4', '2:30', 18);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 5 for Indie Discoveries', 'Artist 18 - Track 5', '3:45', 18);

-- Playlist 19: Soulful Sounds (PlaylistId = 19)
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 1 for Soulful Sounds', 'Artist 19 - Track 1', '3:00', 19);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 2 for Soulful Sounds', 'Artist 19 - Track 2', '3:30', 19);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 3 for Soulful Sounds', 'Artist 19 - Track 3', '4:00', 19);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 4 for Soulful Sounds', 'Artist 19 - Track 4', '2:30', 19);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 5 for Soulful Sounds', 'Artist 19 - Track 5', '3:45', 19);

-- Playlist 20: Evening Reflections (PlaylistId = 20)
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 1 for Evening Reflections', 'Artist 20 - Track 1', '3:00', 20);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 2 for Evening Reflections', 'Artist 20 - Track 2', '3:30', 20);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 3 for Evening Reflections', 'Artist 20 - Track 3', '4:00', 20);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 4 for Evening Reflections', 'Artist 20 - Track 4', '2:30', 20);
INSERT INTO Track (Name, Artist, Duration, PlaylistId) VALUES ('Track 5 for Evening Reflections', 'Artist 20 - Track 5', '3:45', 20);
