function GameConnection::enterGame(%this)
{
	commandToClient(%this, 'setBuildingDisabled', true);
	commandToClient(%this, 'setPaintingDisabled', true);
	commandToClient(%this, 'setPlayingMiniGame', true);
	commandToClient(%this, 'setRunningMiniGame', true);
	commandToClient(%this, 'setRemoteServerData', $Server::LAN, false);
	commandToClient(%this, 'showEnergyBar', false);
	commandToClient(%this, 'PlayGui_CreateToolHud', 0);
	commandToClient(%this, 'setVignette', false, "0 0 0 0");

	// This shit is so busted, man.
	commandToClient(%this, 'setLoadingIndicator', false);

	sendTimeScaleToClient(%this);

	MissionCleanup.add(%camera = new Camera()
	{
		dataBlock = FlyingCamera;
		client = %this;
	});

	%camera.setTransform(%position);
	%camera.setControlObject(0);
	%camera.setFlyMode();
	%camera.scopeToClient(%this);

	%this.setControlObject(%camera);
	%this.camera = %camera;

	messageClient(%this, 'MsgYourSpawn');

	%this.hasSpawnedOnce = true;
}

function GameConnection::leaveGame(%this)
{
	if (isObject(%this.camera))
	{
		%this.camera.delete();
	}
}
