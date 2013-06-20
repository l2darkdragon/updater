Updater
=======

This application is a Lineage II game client auto-update tool written especially for the DarkDragon Lineage II server's needs.

It communicates with an update service server via REST API in order to get information about game clients maintained by the update service and their newest available versions as well as newest available version of the tool itself.

When new version of the game client is released, updater will download the patch from the update service server and apply it to the local game client files in order to keep everything up-to-date.

Dependencies
------------

Updater uses and is distributed with following open source libraries:

 * [NLog](http://nlog-project.org/)
 * [vcdiff-dotnet](https://github.com/winkel/vcdiff-dotnet)

Requirements
------------

Updater requires .NET Framework 4.5 or later and must be configured to point at running update service server instance to ensure its primary function.
