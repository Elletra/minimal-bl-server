function Mission::create()
{
	Mission::destroy();

	echo("\n### Creating mission...\n");

	while (isObject(MissionGroup))
	{
		MissionGroup.deleteAll();
		MissionGroup.delete();
	}

	new SimGroup(MissionGroup)
	{
		new Sky(Sky)
		{
			position = "336 136 0";
			rotation = "1 0 0 0";
			scale = "1 1 1";
			materialList = "base/data/skies/sunny001/sky.dml";
		};

		new Sun(Sun)
		{
			azimuth = 0;
			elevation = 45;
			color = "0.7 0.7 0.7 1.0";
			ambient = "0.4 0.4 0.3 1.0";
		};
	};

	ServerGroup.add(MissionGroup);
	$InstantGroup = ServerGroup;

	setManifestDirty();

	while (isObject(MissionCleanup))
	{
		MissionCleanup.deleteAll();
		MissionCleanup.delete();
	}

	ServerGroup.add(new SimGroup(MissionCleanup));
	$InstantGroup = MissionCleanup;

	snapshotGameAssets();
	purgeResources();

	$Mission::Sequence = 1;
	$Mission::Running = true;
}

function Mission::destroy()
{
	echo ("\n### Destroying mission...\n");

	for (%i = 0; %i < ClientGroup.getCount(); %i++)
	{
		%client = ClientGroup.getObject(%i);

		%client.endMission();
		%client.resetGhosting();
		%client.clearPaths();
	}

	setParticleDisconnectMode(true);

	while (isObject(MissionGroup))
	{
		MissionGroup.deleteAll();
		MissionGroup.delete();
	}

	while (isObject(MissionCleanup))
	{
		MissionCleanup.deleteAll();
		MissionCleanup.delete();
	}

	setParticleDisconnectMode(false);

	$Mission::Running = false;
}
