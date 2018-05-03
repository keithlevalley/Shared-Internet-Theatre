# Shared-Internet-Theatre

This website was co-developed with Doug VanOost

Shared Internet Theatre served as our capstone project from Davenport University.

Purpose behind this site is to allow for individuals to watch media content in a synchronous way.  This is done by a user first creating a room and then inviting others to that room by sharing the room code.  Once others are in the room, the owner of the room has two additional buttons, "Play" and "Pause" that will both sync the video for all users and start and stop the video.  Additionally a chat function exists in the room for everyone to talk back and forth.

Currently only Twitch videos are working on the site, intention is to eventually add other platforms such as YouTube.

The project uses Entity Framework very heavily.  The first time the project is run, it should if configured correctly, create the database and populate some needed fields (this happens in the startup.cs file).  This startup process will automatically build an "Admin" user for the site that has additional access on the site for managing both rooms and users.

The project uses ASP.NET MVC and the chat and video syncing was done using SignalR.
