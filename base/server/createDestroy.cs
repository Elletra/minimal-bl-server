$Server::Type::Internet = 0;
$Server::Type::LAN = 1;

function Server::create(%type)
{
	Server::destroy();

	if (%type $= "")
	{
		%type = $Server::Type::Internet;
	}

	if (%type != $Server::Type::Internet && %type != $Server::Type::LAN)
	{
		error("Invalid server type '", %type, "'");
		return false;
	}

	if (!setNetPort($Pref::Server::Port))
	{
		error("Could not initialize port ", $Pref::Server::Port);
		return false;
	}

	echo("\n### Creating server...\n");

	setRandomSeed();

	$Server::Dedicated = true;
	$Server::LAN = %type == $Server::Type::LAN;

	$Con::LogBufferEnabled = false;

	$Physics::Enabled = false;
	$Physics::MaxBricks = false;

	setAllowConnections(true);

	while (isObject(ServerGroup))
	{
		ServerGroup.delete();
	}

	$ServerGroup = new SimGroup(ServerGroup);

	exec("base/game/main.cs");

	setGhostLimit($Pref::Server::GhostLimit);

	Mission::create();
	Mission::load();

	$Server::Running = true;

	echo("Server is now running.");

	return true;
}

function Server::destroy()
{
	Mission::destroy();

	setAllowConnections(false);

	for (%i = 0; %i < ClientGroup.getCount(); %i++)
	{
		ClientGroup.getObject(%i).delete("The server was shut down.");
	}

	while (isObject(ServerGroup))
	{
		ServerGroup.deleteAll();
		ServerGroup.delete();
	}

	deleteDataBlocks();
	purgeResources();

	$Server::Running = false;
}
