# GarminLaps
This is a project aimed at fixing the problem with Garmin not allowing definitions of laps in activities to be changed after the fact.

As it is right now Garmin Connect won't let a user manipulate when/where a new lap was started in an already uploaded activity (regardless of how it was uploaded).

This might be a problem if you engage in a sport where it is not convenient to push the lap button on the watch at the exact right time (e.g. when riding an enduro motorcycle). Therefore this project aims at giving users the ability to redefine when/where laps have occurred during the activity.

My ambition is to implement this so that users can use it in the following manner:
1) An activity is ended and synced to Garmin Connect.
2) Said activity is then downloaded as a .tcx-file.
3) That .tcx-file is the uploaded to a website (to be built by this project).
4) The user can then indicate where in the activity new laps have occurred.
5) The website will allow the user to download a new .tcx-file.
6) The user removes the old activity (the one with incorrect laps) from Garmin Connect.
7) The user uploads the new .tcx-file to Garmin Connect as a new activity.
