function onDataBlockLimitExceeded()
{
	$DataBlocks::ExceededCount++;
}

function onDataBlocksDeleted()
{
	$DataBlocks::ExceededCount = 0;
}

function onStart()
{
	exec("./init.cs");

	exec("./server/createDestroy.cs");
	exec("./server/connect.cs");
	exec("./server/auth.cs");
	exec("./server/load.cs");

	exec("./mission/createDestroy.cs");
	exec("./mission/load.cs");

	if (!Server::create($Server::Type::Internet))
	{
		error("Failed to create server!");
	}
}

function onExit()
{
	if ($Server::Running)
	{
		Server::destroy();
	}

	export("$Pref::*", "config/prefs.cs", false);
}

onStart();
