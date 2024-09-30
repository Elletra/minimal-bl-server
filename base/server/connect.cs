function GameConnection::onConnectRequest(%this, %netAddress, %lanName, %blid, %clanPrefix, %clanSuffix, %clientNonce)
{
	echo("Got connect request from ", %netAddress);

	if (%clientNonce !$= "")
	{
		cancelPendingConnection(%clientNonce);
	}

	if ($Server::LAN)
	{
		echo ("  lan name = ", %lanName);

		if (%lanName $= "")
		{
			%lanName = "Blockhead";
		}
	}
	else 
	{
		if (%blid !$= mFloor (%blid))
		{
			return "CR_BADARGS";
		}

		%this.bl_id = %blid;
		%this.setBLID("au^timoamyo7zene", %blid);
	}

	if (%blid != getMyBLID())
	{
		// TODO: Handle banned players.
	}

	%this.lanName = trim(getSubStr(stripMLControlChars(%lanName), 0, 23));

	if (ClientGroup.getCount() >= $Pref::Server::MaxPlayers && %this.getRawIP() !$= "local")
	{
		return "CR_SERVERFULL";
	}

	return "";
}

function GameConnection::onConnect(%this)
{
	%this.connected = true;

	echo(%this.getRawIP(), " successfully connected.");

	Auth::check(%this);
}

function GameConnection::onDrop(%this, %reason)
{
	if (%this.connected)
	{
		%this.leaveGame();

		if (%this.getHasAuthedOnce())
		{
			secureCommandToAllExcept("zbR4HmJcSY8hdRhr", %this, 'ClientDrop', %this.getPlayerName(), %this);
		}

		echo(%this.getHasAuthedOnce() ? %this.getPlayerName() : %this.getRawIP(), " disconnected.");
	}
}
