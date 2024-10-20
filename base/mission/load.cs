function Mission::load()
{
	for (%i = 0; %i < ClientGroup.getCount(); %i++)
	{
		ClientGroup.getObject(%i).loadMission();
	}
}

function GameConnection::loadMission(%this)
{
	%this.downloadPhase = 0;

	commandToClient(%this, 'MissionStartPhase1', $Mission::Sequence);
}

function serverCmdMissionStartPhase1Ack(%client, %sequence)
{
	if (%sequence != $Mission::Sequence || !$Mission::Running)
	{
		return;
	}

	if (%client.downloadPhase != 0)
	{
		return;
	}

	%client.downloadPhase = 1;
	%client.sendManifest(snapshotGameAssets());
}

function serverCmdBlobDownloadFinished(%client)
{
	%client.transmitDataBlocks($Mission::Sequence);
}

function GameConnection::onDataBlocksDone(%this, %sequence)
{
	if (%sequence != $Mission::Sequence)
	{
		return;
	}

	if (%this.downloadPhase != 1)
	{
		return;
	}

	%this.downloadPhase = 1.5;

	commandToClient(%this, 'MissionStartPhase2', $Mission::Sequence, "");
}

function serverCmdMissionStartPhase2Ack(%client, %sequence)
{
	if (%sequence != $Mission::Sequence || !$Mission::Running)
	{
		return;
	}

	if (%client.downloadPhase != 1.5)
	{
		return;
	}

	%client.downloadPhase = 2;

	%client.transmitStaticBrickData();
	%client.transmitPaths();
	%client.activateGhosting();
}

function GameConnection::clientWantsGhostAlwaysRetry(%this)
{
	if ($Mission::Running)
	{
		%this.activateGhosting();
	}
}

function GameConnection::onGhostAlwaysFailed(%this)
{
	// Empty
}

function GameConnection::onGhostAlwaysObjectsReceived(%this)
{
	commandToClient(%this, 'MissionStartPhase3', $Mission::Sequence, "", $Server::LAN);
}

function serverCmdMissionStartPhase3Ack(%client, %sequence)
{
	if (%sequence != $Mission::Sequence || !$Mission::Running)
	{
		return;
	}

	if (%client.downloadPhase != 2)
	{
		return;
	}

	%client.downloadPhase = 3;

	%client.startMission();
	%client.enterGame();
}

function GameConnection::startMission(%this)
{
	commandToClient(%this, 'MissionStart', $Mission::Sequence);
}

function GameConnection::endMission(%this)
{
	commandToClient(%this, 'MissionEnd', $Mission::Sequence);
}
